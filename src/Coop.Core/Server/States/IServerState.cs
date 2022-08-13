using Common.LogicStates;

namespace Coop.Core.Server.States
{
    public interface IServerState : IState
    {
        void StartServer();
        void StopServer();
    }
}
