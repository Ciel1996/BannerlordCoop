namespace Coop.Core.Server.States
{
    public abstract class ServerState : IServerState
    {
        protected IServerLogic logic;
        public ServerState(IServerLogic logic)
        {
            this.logic = logic;
        }
        public abstract void StartServer();
        public abstract void StopServer();
    }
}
