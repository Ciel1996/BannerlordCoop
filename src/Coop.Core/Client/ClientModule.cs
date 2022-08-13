using Autofac;
using Common.LogicStates;
using Coop.Core.Client.States;
using LiteNetLib;

namespace Coop.Core.Client
{
    internal class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClientLogic>().As<IClientLogic>();
            builder.RegisterType<CoopClient>().As<ICoopClient>().As<ICoopNetwork>().As<INetEventListener>().SingleInstance();
            builder.RegisterType<InitialClientState>().As<IState>();
            base.Load(builder);
        }
    }
}
