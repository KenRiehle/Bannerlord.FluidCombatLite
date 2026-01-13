using Bannerlord.FluidCombatNext.Helper;
using HarmonyLib;
using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace Bannerlord.FluidCombatNext.Patches
{
    //Patch the polling thread that handles input
    [HarmonyPatch(typeof(MissionMainAgentController), "ControlTick")]
    static class DefendUpPatch
    {
        public static void Postfix(MissionMainAgentController __instance)
        {
            try
            {
                if (DefendUpKey.IsKeyDown)
                    __instance.Mission.MainAgent.MovementFlags |= Agent.MovementControlFlag.DefendUp;
            }
            catch (Exception ex)
            {
                MessageHelper.HandleError("DefendUp Failed", ex.ToString());
            }
        }
    }
}
