using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.HotKeys;
using HarmonyLib;
using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace Bannerlord.FluidCombatNext.Patches
{
    //Patch the polling thread that handles input
    [HarmonyPatch(typeof(MissionMainAgentController), "ControlTick")]
    static class FluidBlockingPatch
    {
        private static bool _cachedDefend = false;

        public static Agent.MovementControlFlag PrevDefenseType = GameHelper.NoAttack;
        public static Agent.MovementControlFlag DefenseType = Agent.MovementControlFlag.DefendUp;

        public static bool RestrictBlockDirectionVert = false;
        public static bool RestrictBlockDirectionHoriz = false;
        public static bool IsChangeActive = true;
        public static float Threshold = 6f;
        public static float XSensitivity = 1f;
        public static float YSensitivity = 1f;

        private static Agent.MovementControlFlag GetDefenseType(Agent agent, float inputX, float inputY)
        {
            if (RestrictBlockDirectionVert)
            {
                if (inputY < 0)
                    return Agent.MovementControlFlag.DefendUp;
                else if (inputY > 0)
                    return Agent.MovementControlFlag.DefendDown;

                return Agent.MovementControlFlag.DefendUp;
            }
            else if (RestrictBlockDirectionHoriz)
            {
                if (inputX < 0)
                    return Agent.MovementControlFlag.DefendLeft;
                else if (inputX > 0)
                    return Agent.MovementControlFlag.DefendRight;

                return Agent.MovementControlFlag.DefendLeft;
            }

            return agent.GetDefendMovementFlag();
        }

        public static void Postfix(MissionMainAgentController __instance)
        {
            try
            {
                Agent mainAgent = __instance.Mission.MainAgent;

                float inputX = Input.MouseMoveX;
                float inputY = Input.MouseMoveY;
                float inputWeight = (MathF.Abs(inputX) * XSensitivity + MathF.Abs(inputY) * YSensitivity) / 2f;

                if (FluidBlockKey.IsKeyDown || _cachedDefend)
                {
                    _cachedDefend = false;

                    if (PrevDefenseType == GameHelper.NoAttack || inputWeight > Threshold)
                        DefenseType = GetDefenseType(mainAgent, inputX, inputY);

                    if (DefenseType != PrevDefenseType && IsChangeActive)
                    {
                        Agent.MovementControlFlag cachedType = PrevDefenseType;
                        PrevDefenseType = DefenseType;
                        
                        if (cachedType != GameHelper.NoAttack)
                        {
                            _cachedDefend = true;
                            return;
                        }
                    }

                    mainAgent.MovementFlags |= DefenseType;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.HandleError("FluidBlock Failed", ex.ToString());
            }
        }
    }
}
