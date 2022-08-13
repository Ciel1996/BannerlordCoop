using HarmonyLib;
using TaleWorlds.ObjectSystem;

namespace Coop.GameInterface.Patch
{
    [HarmonyPatch(typeof(MBObjectManager), "RemoveTemporaryTypes")]
    class TemporaryTypesPatch
    {
        static bool Prefix()
        {
            return false;
        }        
    }
}
