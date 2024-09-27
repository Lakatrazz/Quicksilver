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
    [HarmonyPatch(typeof(FootstepSFX))]
    public static class FootstepSFXPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(FootstepSFX.PlayStep))]
        public static void PlayStepPrefix(float velocitySqr) {
            TimePatches.ForceDefaultTimescale = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(FootstepSFX.PlayStep))]
        public static void PlayStepPostfix(float velocitySqr) {
            TimePatches.ForceDefaultTimescale = false;
        }
    }
}
