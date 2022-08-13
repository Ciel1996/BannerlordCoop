using Coop.Core.Communication.MessageBroker;

namespace Coop.Core.Client.States
{
    public abstract class ClientState : IClientState
    {
        protected IClientLogic Logic;
        protected IMessageBroker MessageBroker;

        public ClientState(IMessageBroker messageBroker, IClientLogic logic)
        {
            MessageBroker = messageBroker;
            Logic = logic;
        }

        public abstract void Connect();
    }
}
