using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext
{
    public class DefendDownKey : HotKeyBase
	{
		public static bool IsKeyDown { get; set; } = false;

		public DefendDownKey() : base(nameof(DefendDownKey),
            displayName: "Defend Down",
            description: "Allows you to defend in a downward direction.",
            defaultKey: InputKey.Numpad4,
            category: SubModule.HotkeyCategory)
        {
			Predicate = IsKeyDownValid;
		}

		private bool IsKeyDownValid()
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
