using Coop.Core.Communication.MessageBroker;
using Coop.GameInterface;

namespace Coop.Core.EventHandlers
{
    public abstract class EventHandlerBase
    {
        protected readonly IMessageBroker MessageBroker;
        protected readonly IGameInterface GameInterface;

        protected EventHandlerBase(IMessageBroker messageBroker, IGameInterface gameInterface)
        {
            MessageBroker = messageBroker;
            GameInterface = gameInterface;
        }
    }
}