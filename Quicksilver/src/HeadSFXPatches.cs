using HarmonyLib;

using UnityEngine;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(HeadSFX))]
public static class HeadSFXPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.SmallDamageVocal))]
    public static void SmallDamageVocalPrefix(float damage)
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.SmallDamageVocal))]
    public static void SmallDamageVocalPostfix(float damage)
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.BigDamageVocal))]
    public static void BigDamageVocalPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.BigDamageVocal))]
    public static void BigDamageVocalPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.DyingVocal))]
    public static void DyingVocalPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.DyingVocal))]
    public static void DyingVocalPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.DeathVocal))]
    public static void DeathVocalPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.DeathVocal))]
    public static void DeathVocalPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.RecoveryVocal))]
    public static void RecoveryVocalPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.RecoveryVocal))]
    public static void RecoveryVocalPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.JumpEffort))]
    public static void JumpEffortPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.JumpEffort))]
    public static void JumpEffortPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.DoubleJump))]
    public static void DoubleJumpPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.DoubleJump))]
    public static void DoubleJumpPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HeadSFX.OnSignificantCollisionEnter))]
    public static void OnSignificantCollisionEnterPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HeadSFX.OnSignificantCollisionEnter))]
    public static void OnSignificantCollisionEnterPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }
}