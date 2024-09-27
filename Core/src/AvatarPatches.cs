using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using UnityEngine;

using Avatar = SLZ.VRMK.Avatar;

namespace Quicksilver.Patching
{
    [HarmonyPatch(typeof(Avatar))]
    public static class AvatarPatches {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Avatar.ComputeMass))]
        public static void ComputeMass(Avatar __instance, float normalizeTo82) {
            if (QuicksilverMod.TargetTimeScale <= 0f || !QuicksilverMod.IsMainRig(__instance) || !QuicksilverMod.IsEnabled)
                return;

            // Mass
            float mlp = 1f / QuicksilverMod.TargetTimeScale;
            float lowerMlp = 1f / Mathf.Lerp(1f, QuicksilverMod.TargetTimeScale, 0.9f);

            __instance._massChest *= mlp;
            __instance._massHead *= mlp;
            __instance._massPelvis *= mlp;
            __instance._massArm *= mlp;
            __instance._massLeg *= mlp;
            __instance._massTotal *= lowerMlp;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Avatar.ComputeBaseStats))]
        public static void ComputeBaseStats(Avatar __instance) {
            if (QuicksilverMod.TargetTimeScale <= 0f || !QuicksilverMod.IsMainRig(__instance) || !QuicksilverMod.IsEnabled)
                return;

            // Stats
            float mlp = 1f / QuicksilverMod.TargetTimeScale;
            float pow = mlp * mlp;

            __instance._strengthUpper *= pow;
            __instance._strengthLower *= pow * pow;
            __instance._strengthGrip *= mlp;
            __instance._speed *= mlp;
            __instance._agility *= pow;
        }
    }
}
