using Bannerlord.FluidCombatNext.Helper;
using HarmonyLib;
using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace Bannerlord.FluidCombatNext.Patches
{
    //Patch the polling thread that handles input
    [HarmonyPatch(typeof(MissionMainAgentController), "ControlTick")]
    static class FluidAttackingPatch
    {
        private static bool _cachedAttack = false;

        
        public static Agent.MovementControlFlag PrevAttackType = GameHelper.NoAttack;
        public static Agent.MovementControlFlag AttackType = Agent.MovementControlFlag.AttackUp;

        public static bool RestrictAttackDirectionVert = false;
        public static bool RestrictAttackDirectionHoriz = false;
        public static bool IsChangeActive = true;
        public static float Threshold = 11f;
        public static float XSensitivity = 1f;
        public static float YSensitivity = 1f;

        private static Agent.MovementControlFlag GetAttackDirection(Agent agent, float inputX, float inputY)
        {
            if (RestrictAttackDirectionVert)
            {
                if (inputY < 0)
                    return Agent.MovementControlFlag.AttackUp;
                else if (inputY > 0)
                    return Agent.MovementControlFlag.AttackDown;

                return Agent.MovementControlFlag.AttackUp;
            }
            else if (RestrictAttackDirectionHoriz)
            {
                EquipmentIndex wieldedItemIndex = agent.GetPrimaryWieldedItemIndex();

                //If we're weilding a weapon
                if (wieldedItemIndex != EquipmentIndex.None)
                {
                    //If we're a polearm go back to default directions (they can only stab and overhead)
                    bool isPolearm = agent.Equipment[wieldedItemIndex].CurrentUsageItem.IsPolearm;
                    if (isPolearm)
                        return agent.AttackDirectionToMovementFlag(agent.PlayerAttackDirection());
                }

                if (inputX < 0)
                    return Agent.MovementControlFlag.AttackLeft;
                else if (inputX > 0)
                    return Agent.MovementControlFlag.AttackRight;

                return Agent.MovementControlFlag.AttackRight;
            }

            return agent.AttackDirectionToMovementFlag(agent.PlayerAttackDirection());
        }

        public static void Postfix(MissionMainAgentController __instance)
        {
            try
            {
                Agent mainAgent = __instance.Mission.MainAgent;

                float inputX = Input.MouseMoveX;
                float inputY = Input.MouseMoveY;
                float inputWeight = (MathF.Abs(inputX) * XSensitivity + MathF.Abs(inputY) * YSensitivity) / 2f;
                
                if (FluidAttackKey.IsKeyDown || _cachedAttack)
                {
                    _cachedAttack = false;

                    if (PrevAttackType == GameHelper.NoAttack || inputWeight > Threshold)
                        AttackType = GetAttackDirection(mainAgent, inputX, inputY);

                    if (AttackType != PrevAttackType && IsChangeActive)
                    {
                        var wieldedItemIndex = mainAgent.GetPrimaryWieldedItemIndex();

                        //If we're weilding a weapon
                        if (wieldedItemIndex != EquipmentIndex.None)
                        {
                            //If we're not a ranged weapon
                            if (!mainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.IsRangedWeapon)
                            {
                                var cachedType = PrevAttackType;
                                PrevAttackType = AttackType;

                                //If cached attack type was not nothing then cancel with a block
                                if (cachedType != GameHelper.NoAttack)
                                {
                                    mainAgent.MovementFlags |= Agent.MovementControlFlag.DefendUp;
                                    _cachedAttack = true;
                                    return;
                                }
                            }
                        }
                    }

                    mainAgent.MovementFlags |= AttackType;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.HandleError("FluidAttack Failed", ex.ToString());
            }
        }
    }
}
