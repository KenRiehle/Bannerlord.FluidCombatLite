using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext.Helper
{
    public static class GameHelper
    {
        public const uint NoAttack = 0U;

        public static bool IsAliveAndInBattle()
        {
            if (Campaign.Current != null)
                return Mission.Current != null && Mission.Current.MainAgent != null && !Campaign.Current.ConversationManager.IsConversationInProgress;
            else
                return Mission.Current != null && Mission.Current.MainAgent != null;
        }

        public static Agent.MovementControlFlag GetAttackDirectionIfNotPolearm(Agent agent, Agent.MovementControlFlag desiredAttackDirection)
        {
            var wieldedItemIndex = agent.GetPrimaryWieldedItemIndex();

            //If we're weilding a weapon
            if (wieldedItemIndex != EquipmentIndex.None)
            {
                //If we're a polearm go back to default directions
                bool isPolearm = agent.Equipment[wieldedItemIndex].CurrentUsageItem.IsPolearm;
                if (isPolearm)
                    return agent.AttackDirectionToMovementFlag(agent.PlayerAttackDirection());
            }

            //Return Desired attack direction
            return desiredAttackDirection;
        }
    }
}
