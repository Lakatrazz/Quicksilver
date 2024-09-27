using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.Rig;
using SLZ.VRMK;
using UnityEngine;

namespace Quicksilver.Patching
{
    [HarmonyPatch(typeof(SLZ_Body.Footstep))]
    public static class FootstepPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SLZ_Body.Footstep.UpdateStepping))]
        public static void UpdateStepping(Vector3 p, Vector3 comVelGrndVector, Vector3 accel, float angularVel, Quaternion rootRotation, ref float stepSpeedUpdate, float stepLZ, Vector3 rootUp, float deltaTime)
        {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f) {
                stepSpeedUpdate /= Mathf.Lerp(1f, QuicksilverMod.TargetTimeScale, 0.95f);
            }
        }
    }
}
