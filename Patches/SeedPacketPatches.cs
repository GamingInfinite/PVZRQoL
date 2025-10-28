using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace PVZRQoL.Patches
{
    [HarmonyPatch(typeof(SeedPacket))]
    internal class SeedPacketPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SeedPacket.Activate))]
        [HarmonyPatch(nameof(SeedPacket.WasPlanted))]
        public static void InvalidateCursor()
        {
            ModMain.CursorSeedPacketIndex = -1;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SeedPacket.Selected))]
        public static void GetIndex(SeedPacket __instance)
        {
            ModMain.CursorSeedPacketIndex = __instance.Index;
        }
    }
}
