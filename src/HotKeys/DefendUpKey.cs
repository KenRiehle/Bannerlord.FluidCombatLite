using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext
{
    public class DefendUpKey : HotKeyBase
	{
		public static bool IsKeyDown { get; set; } = false;

		public DefendUpKey() : base(nameof(DefendUpKey),
            displayName: "Defend Up",
            description: "Allows you to defend in an upward direction.",
            defaultKey: InputKey.Numpad3,
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
