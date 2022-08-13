using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coop.Core.Communication.MessageBroker;
using Coop.Core.Communication.Messages;
using Coop.GameInterface.Serialization;
using LiteNetLib;
using LiteNetLib.Utils;
using ProtoBuf;

namespace Coop.Core.Communication.PacketHandlers
{
    public enum NetworkDistributionType
    {
        Invalid,

    }

    public class PacketManager : IPacketManager
    {
        private readonly NetManager _netManager;
        private readonly ISerializer _serializer;
        private readonly IMessageBroker _messageBroker;

        private static readonly Dictionary<PacketType, List<IPacketHandler>> PacketHandlers = new Dictionary<PacketType, List<IPacketHandler>>();

        public static void RegisterPacketHandler(IPacketHandler handler)
        {
            if(PacketHandlers.TryGetValue(handler.PacketType, out var handlers))
            {
                if (handlers.Contains(handler)) throw new InvalidOperationException($"{handler.GetType()} is already registered.");
                handlers.Add(handler);
            }
            else
            {
                PacketHandlers.Add(handler.PacketType, new List<IPacketHandler> { handler });
            }
        }

        public PacketManager(NetManager netManager, ISerializer serializer, IMessageBroker messageBroker)
        {
            _netManager = netManager;
            _serializer = serializer;
            _messageBroker = messageBroker;

            messageBroker.Subscribe<BroadcastPacket>(HandleBroadcast);
            messageBroker.Subscribe<ForwardPacket>(HandleForward);
            messageBroker.Subscribe<SendPacket>(HandleSend);
        }

        public void HandleBroadcast(MessagePayload<BroadcastPacket> payload)
        {
            IPacket packet = payload.What.Packet;
            SendAll(packet);
        }

        public void HandleForward(MessagePayload<ForwardPacket> payload)
        {
            NetPeer sendingClient = payload.What.SendingClient;
            IPacket packet = payload.What.Packet;
            SendAllBut(sendingClient, packet);
        }

        public void HandleSend(MessagePayload<SendPacket> payload)
        {
            NetPeer receivingClient = payload.What.ClientToSend;
            IPacket packet = payload.What.Packet;
            Send(receivingClient, packet);
        }

        private void SendAllBut(NetPeer netPeer, IPacket packet)
        {
            foreach (NetPeer peer in _netManager.ConnectedPeerList.Where(peer => peer != netPeer))
            {
                Send(peer, packet);
            }
        }

        private void SendAll(IPacket packet)
        {
            foreach(NetPeer peer in _netManager.ConnectedPeerList)
            {
                Send(peer, packet);
            }
        }

        private void Send(NetPeer netPeer, IPacket packet)
        {
            PacketWrapper wrapper = new PacketWrapper(packet);

            // Put type using writer
            NetDataWriter writer = new NetDataWriter();
            writer.Put((int)wrapper.Type);

            // Serialize and put data in writer (with length is important on receive end)
            byte[] data = _serializer.Serialize(packet);
            writer.PutBytesWithLength(data);

            // Send data
            netPeer.Send(writer.Data, wrapper.DeliveryMethod);
        }

        public void HandleReceive(MessagePayload<ReceivePacket> payload)
        {
            NetPeer peer = payload.What.Peer;
            NetPacketReader reader = payload.What.Writer;

            PacketWrapper wrapper = _serializer.Deserialize<PacketWrapper>(reader.GetBytesWithLength());

            PacketType packetType = wrapper.Packet.Type;
            IPacket packet = wrapper.Packet;

            if(PacketHandlers.TryGetValue(packetType, out var handlers))
            {
                foreach(var handler in handlers)
                {
                    Task.Factory.StartNew(() => { handler.HandlePacket(peer, packet); });
                }
            }
        }
    }

    [ProtoContract(SkipConstructor = true)]
    internal readonly struct PacketWrapper : IPacket
    {
        public PacketType Type => PacketType.PacketWrapper;
        public DeliveryMethod DeliveryMethod { get; }

        [ProtoMember(1)]
        public IPacket Packet { get; }

        public PacketWrapper(IPacket packet)
        {
            DeliveryMethod = packet.DeliveryMethod;
            Packet = packet;
        }
    }
}
