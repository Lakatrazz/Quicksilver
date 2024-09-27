using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using SLZ;
using SLZ.Rig;

using UnityEngine;

namespace Quicksilver.Patching {
    [HarmonyPatch(typeof(OpenController))]
    public static class OpenControllerPatches {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(OpenController.GetThumbStickAxis))]
        public static void GetThumbStickAxis(OpenController __instance, ref Vector2 __result) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance.manager)) {
                float mult = 1f / QuicksilverMod.TargetTimeScale;

                var manager = __instance.manager;
                if (manager.isRightHanded) {
                    if (__instance.handedness != Handedness.RIGHT)
                        __result.x *= mult;
                }
                else {
                    if (__instance.handedness != Handedness.LEFT)
                        __result.x *= mult;
                }

                __result.y *= mult;
            }
        }
    }
}
