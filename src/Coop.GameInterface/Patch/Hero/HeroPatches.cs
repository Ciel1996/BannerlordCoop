﻿using HarmonyLib;
using TaleWorlds.CampaignSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Coop.Mod.Patch
{
    [HarmonyPatch(typeof(Hero))]
    internal class HeroPatches
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor)]
        static void Postfix(Hero __instance)
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
