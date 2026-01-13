using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext
{
    public class DefendRightKey : HotKeyBase
	{
		public static bool IsKeyDown { get; set; } = false;

		public DefendRightKey() : base(nameof(DefendRightKey),
            displayName: "Defend Down",
            description: "Allows you to defend from your right side.",
            defaultKey: InputKey.Numpad6,
            category: SubModule.HotkeyCategory)
        {		
			Predicate = IsKeyActive;
		}

		private bool IsKeyActive()
		{
            var isAliveAndInBattle = GameHelper.IsAliveAndInBattle();

            if (!isAliveAndInBattle)
                OnReleased();

            return isAliveAndInBattle;
        }

		protected override void OnPressed()
		{
			IsKeyDown = true;
		}

		protected override void OnReleased()
		{
			IsKeyDown = false;
		}
	}
}
