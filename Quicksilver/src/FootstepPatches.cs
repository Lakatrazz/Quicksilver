using HarmonyLib;

using Il2CppSLZ.Marrow;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(SLZ_Body.Footstep))]
public static class FootstepPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SLZ_Body.Footstep.UpdateStepping))]
    public static void UpdateStepping(Vector3 rootPosition, Quaternion rootRotation, Quaternion footRotation, Vector3 rootUp, Vector3 velocitySanGrav, Vector3 accel, float angularVel, float velocitySanGravNorm, float stepLZ, float velDot, float sacrumHeightWeight, ref float deltaTime)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.TargetTimeScale > 0f)
        {
            deltaTime /= Mathf.Lerp(1f, QuicksilverMod.TargetTimeScale, 0.95f);
        }
    }
}