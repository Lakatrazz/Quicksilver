using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.Interaction;

using UnityEngine;

namespace Quicksilver.Patching {
    [HarmonyPatch(typeof(ForcePullGrip), nameof(ForcePullGrip.OnFarHandHoverUpdate))]
    public static class ForcePullPatches {
        public static void Prefix(ForcePullGrip __instance, Hand hand) {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (QuicksilverMod.TargetTimeScale > 0f) {
                __instance.maxForce = 250f / QuicksilverMod.TargetTimeScale;
            }
        }
    }
}
