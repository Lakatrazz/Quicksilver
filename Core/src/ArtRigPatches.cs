using SLZ.Rig;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

namespace Quicksilver.Patching
{
    [HarmonyPatch(typeof(ArtRig))]
    public static class ArtRigPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(ArtRig.OnUpdate))]
        public static void OnUpdatePrefix(ArtRig __instance)
        {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance))
            {
                TimePatches.ReturnScaled = true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ArtRig.OnUpdate))]
        public static void OnUpdatePostfix()
        {
            TimePatches.ReturnScaled = false;
        }
    }
}
