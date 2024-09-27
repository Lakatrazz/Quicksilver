using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.Rig;
using SLZ.VRMK;
using SLZ.Marrow.Utilities;

using UnityEngine;
using SLZ;
using SLZ.SFX;

namespace Quicksilver.Patching
{
    [HarmonyPatch(typeof(HeadSFX))]
    public static class HeadSFXPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(HeadSFX.SmallDamageVocal))]
        public static void SmallDamageVocalPrefix(float damage) {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HeadSFX.SmallDamageVocal))]
        public static void SmallDamageVocalPostfix(float damage) {
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
        [HarmonyPatch(nameof(HeadSFX.OnCollisionEnter))]
        public static void OnCollisionEnterPrefix(Collision c)
        {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HeadSFX.OnCollisionEnter))]
        public static void OnCollisionEnterPostfix(Collision c)
        {
            TimePatches.ForceDefaultTimescale = false;
        }
    }
}
