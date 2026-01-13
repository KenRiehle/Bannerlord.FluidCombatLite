using Bannerlord.FluidCombatNext.Helper;
using HarmonyLib;
using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace Bannerlord.FluidCombatNext.Patches
{
    //Patch the polling thread that handles input
    [HarmonyPatch(typeof(MissionMainAgentController), "ControlTick")]
    public static class DefendLeftPatch
    {
        public static void Postfix(MissionMainAgentController __instance)
        {
            try
            {
                if (DefendLeftKey.IsKeyDown)
                    __instance.Mission.MainAgent.MovementFlags |= Agent.MovementControlFlag.DefendLeft;
            }
            catch (Exception ex)
            {
                MessageHelper.HandleError("DefendLeft Failed", ex.ToString());
            }
        }
    }
}
