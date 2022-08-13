using Common.LogicStates;

namespace Coop.Core.Client.States
{
    public interface IClientState : IState
    {
        void Connect();
    }
}
