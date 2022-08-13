using Coop.GameInterface.Helpers;

namespace Coop.GameInterface
{



    public interface IGameInterface
    {
        IExampleGameHelper ExampleGameHelper { get; }
        ISaveLoadHelper SaveLoadHelper { get; }
    }
}
