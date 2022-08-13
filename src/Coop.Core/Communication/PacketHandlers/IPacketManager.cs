using System;
using Common.Components;
using Coop.Core.Communication.MessageBroker;
using Coop.Core.Communication.Messages;

namespace Coop.Core.Communication.PacketHandlers
{
    public interface IPacketManager
    {
        void HandleBroadcast(MessagePayload<BroadcastPacket> payload);
        void HandleForward(MessagePayload<ForwardPacket> payload);
        void HandleSend(MessagePayload<SendPacket> payload);
        void HandleReceive(MessagePayload<ReceivePacket> payload);
    }
}