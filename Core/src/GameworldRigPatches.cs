using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.Rig;

using UnityEngine;

namespace Quicksilver.Patching
{
    [HarmonyPatch(typeof(RealtimeSkeletonRig))]
    public static class RealtimeSkeletonRigPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(RealtimeSkeletonRig.UpdateHeptaBody))]
        public static void UpdateHeptaBody(RealtimeSkeletonRig __instance, ControllerRig inRig, ref float deltaTime) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= QuicksilverMod.TargetTimeScale;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RealtimeSkeletonRig.OnFixedUpdate))]
        public static void OnFixedUpdate(RealtimeSkeletonRig __instance, ref float deltaTime)
        {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance))
            {
                deltaTime /= QuicksilverMod.TargetTimeScale;
            }
        }
    }

    [HarmonyPatch(typeof(GameWorldSkeletonRig))]
    public static class GameworldRigPatches {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameWorldSkeletonRig.OnFixedUpdate))]
        public static void OnFixedUpdatePrefix(GameWorldSkeletonRig __instance, float deltaTime) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                TimePatches.ReturnScaled = true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameWorldSkeletonRig.OnFixedUpdate))]
        public static void OnFixedUpdatePostfix(float deltaTime)
        {
            TimePatches.ReturnScaled = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameWorldSkeletonRig.UpdateHeptaBody2))]
        public static void UpdateHeptaBody2(GameWorldSkeletonRig __instance, Rig inRig, ref float deltaTime, ref Vector2 velocity, ref Vector2 accel)
        {
            if (!QuicksilverMod.IsEnabled)
                return;

            float timeScale = QuicksilverMod.TargetTimeScale;

            if (timeScale > 0f && QuicksilverMod.IsMainRig(__instance))
            {
                float sqr = timeScale * timeScale;
                float pow = sqr * sqr;
                deltaTime /= sqr;
                velocity *= pow;
                accel *= pow * pow;
            }
        }
    }
}
