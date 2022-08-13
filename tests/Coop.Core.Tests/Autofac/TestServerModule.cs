using Autofac;
using Coop.Core.Communication.MessageBroker;
using Coop.Core.Server;
using Coop.Core.Tests.Stubs;

namespace Coop.Core.Tests.Autofac
{
    internal class TestServerModule : TestModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CoopServerStub>().As<ICoopServer>().As<ICoopNetwork>().SingleInstance();
            builder.RegisterType<MessageBrokerStub>().As<IMessageBroker>().SingleInstance();
            
            base.Load(builder);
        }
    }
}
