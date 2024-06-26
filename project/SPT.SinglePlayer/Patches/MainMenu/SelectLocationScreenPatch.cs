using SPT.Reflection.Patching;
using EFT.UI;
using EFT.UI.Matchmaker;
using System.Reflection;
using HarmonyLib;

namespace SPT.SinglePlayer.Patches.MainMenu
{
    /// <summary>
    /// Remove the ready button from select location screen
    /// </summary>
    public class SelectLocationScreenPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MatchMakerSelectionLocationScreen), nameof(MatchMakerSelectionLocationScreen.Awake));
        }

        [PatchPostfix]
        private static void PatchPostfix(DefaultUIButton ____readyButton)
        {
            ____readyButton.Interactable = false;
        }
    }
}
