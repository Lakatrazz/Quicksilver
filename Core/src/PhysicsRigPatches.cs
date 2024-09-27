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
    [HarmonyPatch(typeof(PhysicsRig))]
    public static class PhysicsRigPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PhysicsRig.UpdateKnee))]
        public static void UpdateKneePrefix(PhysicsRig __instance, float feetTar, out float __state) {
            __state = __instance.manager.avatar._massTotal;

            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance)) {
                var drive = __instance.kneePelvisJoint.yDrive;
                float mlp = 1f / QuicksilverMod.TargetTimeScale;

                drive.positionSpring = 7500f * mlp;
                drive.positionDamper = 1200f * mlp;
                drive.maximumForce = 6750f * mlp;

                __instance.kneePelvisJoint.yDrive = drive;

                __instance.manager.avatar._massTotal *= mlp;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(PhysicsRig.UpdateKnee))]
        public static void UpdateKneePostfix(PhysicsRig __instance, float feetTar, float __state)
        {
            __instance.manager.avatar._massTotal = __state;
        }
    }
}
