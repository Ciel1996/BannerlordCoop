using HarmonyLib;
using TaleWorlds.CampaignSystem.CampaignBehaviors;

namespace Coop.GameInterface.Patch.CampaignPatches
{
    [HarmonyPatch(typeof(CompanionsCampaignBehavior))]
    internal class CompanionsCampaignBehaviorPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch("CreateCompanionAndAddToSettlement")]
        private static bool Prefix()
        {
            return false;
        }
    }
}
