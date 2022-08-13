using Autofac;
using Coop.Core.Configuration;
using Coop.Core.Server.Config;
using Coop.Core.Server.States;
using LiteNetLib;

namespace Coop.Core.Server
{
    internal class ServerModule : CoopModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServerLogic>().As<IServerLogic>().SingleInstance();
            builder.RegisterType<CoopServer>().As<ICoopServer>().As<ICoopNetwork>().As<INetEventListener>().SingleInstance();
            builder.RegisterType<InitialServerState>().As<IServerState>();
            
            builder.RegisterType<ServerConfig>().As<INetworkConfiguration>().OwnedByLifetimeScope();

            
            base.Load(builder);
        }
    }
}