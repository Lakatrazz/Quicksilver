using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using SLZ.SFX;
using UnityEngine;

namespace Quicksilver.Patching {
    [HarmonyPatch(typeof(WindBuffetSFX))]
    public static class WindBuffetPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(WindBuffetSFX.Update))]
        public static void UpdatePrefix(WindBuffetSFX __instance) {
            if (QuicksilverMod.IsEnabled) {
                if (QuicksilverMod.TargetTimeScale > 0f) {
                    __instance.minSpeed = 5f / QuicksilverMod.TargetTimeScale;
                    __instance.maxSpeed = 40f / QuicksilverMod.TargetTimeScale;
                }

                TimePatches.ForceDefaultTimescale = true;
            }
            else {
                __instance.minSpeed = 5f;
                __instance.maxSpeed = 40f;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WindBuffetSFX.Update))]
        public static void UpdatePostfix(WindBuffetSFX __instance) {
            TimePatches.ForceDefaultTimescale = false;
        }
    }
}
