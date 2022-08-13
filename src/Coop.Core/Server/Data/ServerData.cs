using System.Collections.Generic;
using Coop.Core.Data;
using LiteNetLib;

namespace Coop.Core.Server.Data
{
    internal class ServerData
    {
        public IReadOnlyList<ICoopPlayer> Players => _players;
        private readonly List<ICoopPlayer> _players = new List<ICoopPlayer>();
        void AddClient(NetPeer peer)
        {

        }
        void RemoveClient(NetPeer peer)
        {

        }
        void AddP2PClient(NetPeer peer)
        {

        }
        void RemoveP2PClient(NetPeer peer)
        {

        }
    }
}
