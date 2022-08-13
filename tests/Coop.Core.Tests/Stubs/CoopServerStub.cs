using System;
using System.Net;
using System.Net.Sockets;
using Coop.Core.Server;
using LiteNetLib;

namespace Coop.Core.Tests.Stubs
{
    public class CoopServerStub : ICoopServer
    {
        public void Update(TimeSpan frameTime)
        {
            
        }

        public int Priority { get; }
        
        public void OnPeerConnected(NetPeer peer)
        {
            
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        public void OnNatIntroductionRequest(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, string token)
        {
            
        }

        public void OnNatIntroductionSuccess(IPEndPoint targetEndPoint, NatAddressType type, string token)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}