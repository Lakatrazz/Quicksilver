using HarmonyLib;

using Il2CppSLZ.Marrow;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(Rigidbody))]
public static class RigidbodyPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Rigidbody.AddForceAtPosition), typeof(Vector3), typeof(Vector3), typeof(ForceMode))]
    public static void AddForceAtPosition(Rigidbody __instance, ref Vector3 force, Vector3 position, ForceMode mode)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        var gun = Gun.Cache.Get(__instance.gameObject);

        if (QuicksilverMod.TargetTimeScale <= 0f || !gun)
            return;

        if (!(gun.triggerGrip && gun.triggerGrip.GetHand() && gun.triggerGrip.GetHand().manager == QuicksilverMod.Instance.rigManager))
            return;

        force /= QuicksilverMod.TargetTimeScale;
    }
}