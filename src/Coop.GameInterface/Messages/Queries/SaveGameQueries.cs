using Common;
using Coop.GameInterface.Data;

namespace Coop.GameInterface.Messages.Queries
{
    public readonly struct GameSaveDataQuery { }

    /// <summary>
    /// Reply to GameSaveDataQuery
    /// </summary>
    public readonly struct GameSaveDataResponse : IResponse
    {
        public IGameSaveData GameSaveData { get; }


        // Always successful
        public bool Success => true;

        /// <summary>
        /// GameSaveData will only be created internally as it requires game access
        /// </summary>
        /// <param name="gameSaveData">Game Save Data</param>
        internal GameSaveDataResponse(IGameSaveData gameSaveData) 
        { 
            GameSaveData = gameSaveData;
        }
    }
}
