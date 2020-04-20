﻿using System;
using System.IO;

namespace Coop.Network
{
    public enum EConnectionState
    {
        ClientJoinRequesting,   /** [client side] Client is trying to establish a connection to a server.
                                 *
                                 * Possible transitions to:
                                 * - ClientJoining: The server accepted the request.
                                 * - Disconnecting: Request timeout or the server rejected the request.
                                 */
        ClientAwaitingWorldData,/** [client side] Client is joining a server, e.g. downloading data.
                                 *
                                 * Possible transitions to
                                 * - ClientConnected:   Client successfully connected to the server.
                                 * - Disconnecting:     Timeout.
                                 */
        ClientConnected,        /** [client side] Client is connected to a server.
                                 *
                                 * Possible transitions to:
                                 * - Disconnecting:  Timeout or disconnect request (either server or client side).
                                 */
        ServerAwaitingClient,   /** [server side] Server is awaiting a join request from a client.
                                 *
                                 * Possible transitions to:
                                 * - ServerJoining: Join request from client received & approved.
                                 * - Disconnecting:  Timeout or request denied.
                                 */
        ServerSendingWorldData, /** [server side] Client is joining the server.
                                 *
                                 * Possible transitions to:
                                 * - ServerConnected: Join request from client received & approved.
                                 * - Disconnecting:    Timeout or request denied.
                                 */
        ServerConnected,        /** [server side] Client is connected to the server.
                                 *
                                 * Possible transitions to:
                                 * - Disconnecting:  Timeout or disconnect request (either server or client side).
                                 */
        Disconnecting,          /** Connection is being closed.
                                 *
                                 * Possible transitions to:
                                 * - Disconnected:  Connection was closed.
                                 */
        Disconnected            /** Connection is inactive.
                                 *
                                 * Possible transitions to:
                                 * - ClientJoinRequesting:  [client side] Client wants to establish a connection to a server.
                                 * - ServerAwaitingClient:  [server side] Client wants to establish a connection to a server.
                                 */
    }
    /// <summary>
    /// A connection represents state and logic for data exchange between a client and a server.
    /// - State management & transitions are to be implemented by inherting classes.
    /// - Physical data exchange is implemented in a <see cref="INetworkConnection"/>.
    /// </summary>
    public abstract class ConnectionBase
    {
        public readonly PacketDispatcher Dispatcher;
        /// <summary>
        /// </summary>
        /// <param name="network">Networking implementation.</param>
        public ConnectionBase(INetworkConnection network)
        {
            Dispatcher = new PacketDispatcher();
            m_Network = network;
        }

        #region Send and Receive
        public void Send(Packet packet)
        {
            if (State != EConnectionState.Disconnected)
            {
                if (packet.Length > m_Network.MaxPackageLength)
                {
                    throw new PacketSendException($"Payload for package {packet.Type} too large ({packet.Length}>{m_Network.MaxPackageLength}).");
                }
                else
                {
                    new PacketWriter(packet).Write(new BinaryWriter(m_Stream));
                    m_Network.SendRaw(m_Stream.ToArray());
                    m_Stream.SetLength(0);
                }
            }
        }
        public void SendFragmented(Packet packet)
        {
            if (State != EConnectionState.Disconnected)
            {
                if (packet.Length > m_Network.MaxPackageLength)
                {
                    throw new PacketSendException($"Payload for package {packet.Type} too large ({packet.Length}>{m_Network.MaxPackageLength}).");
                }
                else
                {
                    var writer = new BinaryWriter(m_Stream);
                    var packetWriter = new PacketWriter(packet);
                    while (!packetWriter.Done)
                    {
                        packetWriter.Write(writer, m_Network.FragmentLength);
                        m_Network.SendRaw(m_Stream.ToArray());
                        m_Stream.SetLength(0);
                    }
                }
            }
        }
        public void Receive(ByteReader reader)
        {
            if (State != EConnectionState.Disconnected)
            {
                if (m_PackageReader == null)
                {
                    m_PackageReader = new PacketReader();
                }
                Packet packet = m_PackageReader.Read(reader);
                if (packet != null)
                {
                    m_PackageReader = null;
                    Dispatcher.Dispatch(State, packet);
                }
            }
        }
        #endregion
        #region State & transitions
        protected readonly INetworkConnection m_Network;
        public abstract EConnectionState State { get; }
        public int Latency = 0;
        /// <summary>
        /// Closes the connection.
        /// 
        /// Postcondition: <see cref="State"/> is equal to <see cref="EConnectionState.Disconnected"/>.
        /// </summary>
        /// <param name="eReason">Reason that caused the disconnect.</param>
        public abstract void Disconnect(EDisconnectReason eReason);
        public override string ToString()
        {
            return $"{base.ToString()} - State: {State}, Ping: {Latency}, Netinfo: {m_Network}";
        }
        #endregion
        #region Internals
        private MemoryStream m_Stream = new MemoryStream();
        private PacketReader m_PackageReader = null;
        #endregion
    }
}
