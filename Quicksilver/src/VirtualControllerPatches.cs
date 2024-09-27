using HarmonyLib;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(HandgunVirtualController))]
public static class HandgunVirtualControllerPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(HandgunVirtualController.OnVirtualControllerSolve))]
    public static void OnVirtualControllerSolvePrefix(VirtualControlerPayload p)
    {
        TimePatches.ReturnScaled = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandgunVirtualController.OnVirtualControllerSolve))]
    public static void OnVirtualControllerSolvePostfix(VirtualControlerPayload p)
    {
        TimePatches.ReturnScaled = false;
    }
}

[HarmonyPatch(typeof(RifleVirtualController))]
public static class RifleVirtualControllerPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(RifleVirtualController.OnVirtualControllerSolve))]
    public static void OnVirtualControllerSolvePrefix(VirtualControlerPayload payload)
    {
        TimePatches.ReturnScaled = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(RifleVirtualController.OnVirtualControllerSolve))]
    public static void OnVirtualControllerSolvePostfix(VirtualControlerPayload payload)
    {
        TimePatches.ReturnScaled = false;
    }
}