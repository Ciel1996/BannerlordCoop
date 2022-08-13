using Common.LogicStates;
using Coop.Core.Client.States;
using Coop.Core.Communication.MessageBroker;
using NLog;

namespace Coop.Core.Client
{
    public class ClientLogic : IClientLogic
    {
        public ILogger Logger { get; }
        public IMessageBroker MessageBroker { get; }
        public IState State { get => _state; set => _state = (IClientState)value; }
        private IClientState _state;
        
        public ClientLogic(ILogger logger, IMessageBroker messageBroker, IState initialState)
        {
            Logger = logger;
            MessageBroker = messageBroker;
            State = initialState;
        }
    }
}
