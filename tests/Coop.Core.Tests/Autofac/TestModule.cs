using Autofac;
using Coop.Core.Communication.MessageBroker;
using Coop.Core.Debugging.Logger;
using Coop.Core.Tests.Stubs;
using LiteNetLib;

namespace Coop.Core.Tests.Autofac
{
    internal abstract class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageBrokerStub>().As<IMessageBroker>().SingleInstance();
            builder.RegisterType<NLogLogger>().As<ILogger>().SingleInstance();
            
            builder.RegisterType<NetManager>().AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}
