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
    [HarmonyPatch(typeof(HandSFX))]
    public static class HandSFXPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(HandSFX.Grab))]
        public static void GrabPrefix() {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HandSFX.Grab))]
        public static void GrabPostfix() {
            TimePatches.ForceDefaultTimescale = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(HandSFX.BodySlot))]
        public static void BodySlotPrefix() {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HandSFX.BodySlot))]
        public static void BodySlotPostfix() {
            TimePatches.ForceDefaultTimescale = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(HandSFX.Drop))]
        public static void DropPrefix()
        {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HandSFX.Drop))]
        public static void DropPostfix()
        {
            TimePatches.ForceDefaultTimescale = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(HandSFX.ForcePull))]
        public static void ForcePullPrefix(float massDistance)
        {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HandSFX.ForcePull))]
        public static void ForcePullPostfix(float massDistance)
        {
            TimePatches.ForceDefaultTimescale = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(HandSFX.OnCollisionEnter))]
        public static void OnCollisionEnterPrefix(Collision c)
        {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HandSFX.OnCollisionEnter))]
        public static void OnCollisionEnterPostfix(Collision c)
        {
            TimePatches.ForceDefaultTimescale = false;
        }
    }
}
