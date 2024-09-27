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

namespace Quicksilver.Patching
{
    [HarmonyPatch(typeof(Time))]
    public static class TimePatches {
        public static bool ReturnScaled = false;

        public static bool ForceDefaultTimescale = false;

        public static bool ReturnInversed = false;

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Time.deltaTime), MethodType.Getter)]
        public static void GetDeltaTime(ref float __result) {
            if (!QuicksilverMod.IsEnabled || !(Time.timeScale > 0f))
                return;

            if (ReturnScaled)
                __result /= Time.timeScale;
            else if (ReturnInversed)
                __result *= Time.timeScale;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Time.fixedDeltaTime), MethodType.Getter)]
        public static void GetFixedDeltaTime(ref float __result) {
            if (!QuicksilverMod.IsEnabled || !(Time.timeScale > 0f))
                return;

            if (ReturnScaled)
                __result /= Time.timeScale;
            else if (ReturnInversed)
                __result *= Time.timeScale;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Time.timeScale), MethodType.Getter)]
        public static void GetTimeScale(ref float __result)
        {
            if (!QuicksilverMod.IsEnabled)
                return;

            if (ForceDefaultTimescale)
                __result = 1f;
        }
    }
}
