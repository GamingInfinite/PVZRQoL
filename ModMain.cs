using Il2CppReloaded.Gameplay;
using MelonLoader;
using PVZREasyAPI;
using UnityEngine;

[assembly: MelonInfo(typeof(PVZRQoL.ModMain), "PVZ: Replanted QoL", "1.0.0", "SamanthaUSR", null)]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

namespace PVZRQoL
{
    public class ModMain : MelonMod
    {
        public static int CursorSeedPacketIndex = -1;
        public static SeedBank currentSeeds;
        public static SeedPacket currentPacket
        {
            get
            {
                if (currentSeeds == null || CursorSeedPacketIndex == -1)
                {
                    return null;
                }
                return currentSeeds.mSeedPackets[CursorSeedPacketIndex];
            }
        }

        public static KeyCode[] keybinds =
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0
        };

        public override void OnInitializeMelon()
        {
            Events.OnBoardUpdate += (Board board) =>
            {
                currentSeeds = board.mSeedBank;
            };
            Events.OnAddCoin += (Coin coin) =>
            {
                if (coin.IsSun() || coin.IsMoney())
                {
                    coin.Collect(0, false);
                    coin.PlayCollectSound();
                }
            };
            HarmonyInstance.PatchAll();
        }

        public override void OnUpdate()
        {
            ManageHotkeys();
        }

        public void ManageHotkeys()
        {
            if (currentSeeds != null)
            {
                for (int i = 0; i < currentSeeds.GetPacketCount(); i++)
                {
                    SeedPacket packet = currentSeeds.mSeedPackets[i];
                    if (Input.GetKeyDown(keybinds[i]))
                    {
                        if (packet.CanPickUp())
                        {
                            if (CursorSeedPacketIndex != i)
                            {
                                if (currentPacket != null)
                                {
                                    currentPacket.Deselected(0);
                                    currentPacket.Activate();
                                }
                                packet.Selected(0, false);
                                packet.Activated(0, false);
                                return;
                            }
                            packet.Deselected(0);
                            packet.Activate();
                            return;
                        }
                        else
                        {
                            packet.Activated(0, false);
                        }
                    }
                }
            }
        }
    }
}