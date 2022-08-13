using TaleWorlds.SaveSystem.Load;

namespace Coop.GameInterface.Data
{
    internal class GameSaveData : IGameSaveData
    {
        public byte[] Data { get; }

        public GameSaveData(byte[] data)
        {
            Data = data;
        }

        public LoadResult LoadData()
        {
            return null;
        }
    }
}
