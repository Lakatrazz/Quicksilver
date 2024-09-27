using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.Rig;

using UnityEngine;

namespace Quicksilver.Patching {
    [HarmonyPatch(typeof(RemapRig))]
    public static class RemapRigPatches
    {
        private const float CrouchPow = 0.01f;
        private const float JumpPow = 0.5f;
        private const float RotationPow = 0.4f;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RemapRig.ApplyRotation))]
        public static void ApplyRotation(RemapRig __instance, ref float deltaTime, float smoothTwist) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= Mathf.Pow(QuicksilverMod.TargetTimeScale, RotationPow);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RemapRig.JumpCharge))]
        public static void JumpCharge(RemapRig __instance, ref float deltaTime, bool chargeInput = true) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= Mathf.Pow(QuicksilverMod.TargetTimeScale, JumpPow);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RemapRig.Jumping))]
        public static void Jumping(RemapRig __instance, ref float deltaTime)
        {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= Mathf.Pow(QuicksilverMod.TargetTimeScale, JumpPow);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RemapRig.CrouchHold))]
        public static void CrouchHold(RemapRig __instance, ref float deltaTime, float crouchRate = -1f, bool crouchInput = true) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= Mathf.Pow(QuicksilverMod.TargetTimeScale, CrouchPow);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RemapRig.MoveSpineCrouchOffTowards))]
        public static void MoveSpineCrouchOffTowards(RemapRig __instance, float target, ref float deltaTime, float rate = 6.5f) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= Mathf.Pow(QuicksilverMod.TargetTimeScale, CrouchPow);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(RemapRig.ApplyMovement))]
        public static void ApplyMovementPrefix(RemapRig __instance, Vector2 axis, bool inputPressed, ref float deltaTime) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                deltaTime /= Mathf.Lerp(1f, QuicksilverMod.TargetTimeScale, 0.95f);

                TimePatches.ForceDefaultTimescale = true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(RemapRig.ApplyMovement))]
        public static void ApplyMovementPostfix(Vector2 axis, bool inputPressed, float deltaTime) {
            TimePatches.ForceDefaultTimescale = false;
        }
    }
}
