using Autofac;
using Coop.Core.Communication;
using Coop.Core.Communication.MessageBroker;
using Coop.Core.Communication.PacketHandlers;
using Coop.Core.Configuration;
using Coop.Core.Debugging.Logger;
using Coop.Core.Server.Connections;
using Coop.GameInterface.Serialization;
using LiteNetLib;

namespace Coop.Core
{
    internal abstract class CoopModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Logging
            builder.RegisterType<NLogLogger>().As<ILogger>().SingleInstance();
            #endregion

            #region Network
            builder.RegisterType<NetworkConfiguration>().As<INetworkConfiguration>().OwnedByLifetimeScope();
            builder.RegisterType<NetManager>().AsSelf().SingleInstance();
            #endregion

            #region Communication
            builder.RegisterType<ProtobufSerializer>().As<ISerializer>().SingleInstance();
            builder.RegisterType<PacketManager>().As<IPacketManager>().SingleInstance();
            builder.RegisterType<NetworkMessageBroker>().As<IMessageBroker>().SingleInstance();
            builder.RegisterType<Connection>().As<IConnection>();
            #endregion

            #region Coop.Core.GameInterface
            builder.RegisterType<GameInterface.GameInterface>().AsSelf().SingleInstance();
            #endregion

            base.Load(builder);
        }
    }
}
