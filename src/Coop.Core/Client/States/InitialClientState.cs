using System;
using Coop.Core.Communication.MessageBroker;

namespace Coop.Core.Client.States
{
    internal class InitialClientState : ClientState
    {
        public InitialClientState(IMessageBroker messageBroker, IClientLogic logic) : base(messageBroker, logic) { }

        public override void Connect()
        {
            throw new NotImplementedException();
        }
    }
}
