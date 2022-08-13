using System;

namespace Coop.Core.Server.States
{
    public class ServerRunningState : ServerState
    {
        public ServerRunningState(IServerLogic logic) : base(logic) { }

        public override void StartServer()
        {
            throw new NotImplementedException();
        }

        public override void StopServer()
        {
            throw new NotImplementedException();
        }
    }
}
