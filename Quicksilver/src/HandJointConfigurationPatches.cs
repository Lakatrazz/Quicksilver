using HarmonyLib;

using Il2CppSLZ.Marrow;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(HandJointConfiguration))]
public static class HandJointConfigurationPatches
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandJointConfiguration.LockConfiguration))]
    public static void LockConfiguration(ConfigurableJoint joint)
    {
        float timeScale = QuicksilverMod.TargetTimeScale;
        if (QuicksilverMod.IsEnabled && timeScale > 0f)
        {
            float sqr = timeScale * timeScale;
            joint.breakForce /= sqr;
            joint.breakTorque /= sqr;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandJointConfiguration.ApplyConfiguration), typeof(ConfigurableJoint))]
    public static void ApplyConfiguration(ConfigurableJoint joint)
    {
        if (QuicksilverMod.IsEnabled && QuicksilverMod.TargetTimeScale > 0f)
        {
            joint.breakForce /= QuicksilverMod.TargetTimeScale * QuicksilverMod.TargetTimeScale;
            joint.breakTorque /= QuicksilverMod.TargetTimeScale * QuicksilverMod.TargetTimeScale;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandJointConfiguration.ApplyConfiguration), typeof(Quaternion), typeof(ConfigurableJoint))]
    public static void ApplyConfiguration(Quaternion localRotation, ConfigurableJoint joint)
    {
        if (QuicksilverMod.IsEnabled && QuicksilverMod.TargetTimeScale > 0f)
        {
            joint.breakForce /= QuicksilverMod.TargetTimeScale * QuicksilverMod.TargetTimeScale;
            joint.breakTorque /= QuicksilverMod.TargetTimeScale * QuicksilverMod.TargetTimeScale;
        }
    }
}