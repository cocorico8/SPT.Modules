﻿using SPT.Reflection.Patching;
using Comfort.Common;
using EFT;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace SPT.SinglePlayer.Patches.Progression
{
    /// <summary>
    /// After picking up a quest item, trigger CheckForStatusChange() from the questController to fully update a quest sub-tasks to show (e.g. `survive and extract item from raid` task)
    /// </summary>
    public class MidRaidQuestChangePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Profile), nameof(Profile.AddToCarriedQuestItems));
        }

        [PatchPostfix]
        private static void PatchPostfix()
        {
            var gameWorld = Singleton<GameWorld>.Instance;
            if (gameWorld == null)
            {
                Logger.LogDebug("[MidRaidQuestChangePatch] gameWorld instance was null");

                return;
            }
                
            var player = gameWorld.MainPlayer;
            var questController = Traverse.Create(player).Field<AbstractQuestControllerClass>("_questController").Value;
            if (questController != null)
            {
                foreach (var quest in questController.Quests.ToList())
                {
                    quest.CheckForStatusChange(true, true);
                }
            }
        }
    }
}