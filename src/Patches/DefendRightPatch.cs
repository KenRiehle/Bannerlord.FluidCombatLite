using Bannerlord.FluidCombatNext.Helper;
using HarmonyLib;
using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace Bannerlord.FluidCombatNext.Patches
{
    //Patch the polling thread that handles input
    [HarmonyPatch(typeof(MissionMainAgentController), "ControlTick")]
    public static class DefendRightPatch
    {
        public static void Postfix(MissionMainAgentController __instance)
        {
            try
            {
                if (DefendRightKey.IsKeyDown)
                    __instance.Mission.MainAgent.MovementFlags |= Agent.MovementControlFlag.DefendRight;
            }
            catch (Exception ex)
            {
                MessageHelper.HandleError("DefendRight Failed", ex.ToString());
            }
        }
    }
}
