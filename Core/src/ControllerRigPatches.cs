using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.Rig;

using UnityEngine;

namespace Quicksilver.Patching {
    [HarmonyPatch(typeof(ControllerRig))]
    public static class ControllerRigPatches
    {
        private const float RotationPow = 0.4f;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ControllerRig.OnFixedUpdate))]
        public static void OnFixedUpdatePrefix(ControllerRig __instance, float deltaTime, out float __state)
        {
            __state = __instance.degreesPerSnap;

            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                __instance.degreesPerSnap /= Mathf.Pow(QuicksilverMod.TargetTimeScale, RotationPow);
                TimePatches.ReturnScaled = true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ControllerRig.OnFixedUpdate))]
        public static void OnFixedUpdatePostfix(ControllerRig __instance, float deltaTime, float __state) {
            __instance.degreesPerSnap = __state;
            TimePatches.ReturnScaled = false;
        }
    }
}
