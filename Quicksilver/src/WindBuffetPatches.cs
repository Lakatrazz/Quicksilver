using HarmonyLib;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(WindBuffetSFX))]
public static class WindBuffetPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(WindBuffetSFX.UpdateBuffet))]
    public static void UpdateBuffetPrefix(WindBuffetSFX __instance)
    {
        if (QuicksilverMod.IsEnabled)
        {
            if (QuicksilverMod.TargetTimeScale > 0f)
            {
                __instance.minSpeed = 5f / QuicksilverMod.TargetTimeScale;
                __instance.maxSpeed = 40f / QuicksilverMod.TargetTimeScale;
            }

            TimePatches.ForceDefaultTimescale = true;
        }
        else
        {
            __instance.minSpeed = 5f;
            __instance.maxSpeed = 40f;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(WindBuffetSFX.UpdateBuffet))]
    public static void UpdateBuffetPostfix(WindBuffetSFX __instance)
    {
        TimePatches.ForceDefaultTimescale = false;
    }
}