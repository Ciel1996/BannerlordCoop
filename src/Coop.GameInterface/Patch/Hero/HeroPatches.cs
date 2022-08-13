using HarmonyLib;
using NLog;

namespace Coop.GameInterface.Patch.Hero
{
    [HarmonyPatch(typeof(TaleWorlds.CampaignSystem.Hero))]
    internal class HeroPatches
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor)]
        static void Postfix(TaleWorlds.CampaignSystem.Hero __instance)
        {
            
            //if(Coop.Core.IsServer)
            //{
            //    string stacktrace = Environment.StackTrace;
            //    Logger.Info($"Creating new hero, {__instance.Name}");
            //}
            //else if(CoopClient.Instance.ClientPlaying)
            //{
            //    string stacktrace = Environment.StackTrace;
            //    Logger.Info($"Creating new hero, {__instance.Name}");
            //}
            
        }
    }
}
