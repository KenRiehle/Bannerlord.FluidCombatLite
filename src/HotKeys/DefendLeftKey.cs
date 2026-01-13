using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext
{
    public class DefendLeftKey : HotKeyBase
    {
        public static bool IsKeyDown { get; set; } = false;

        public DefendLeftKey() : base(nameof(DefendLeftKey),
            displayName: "Defend Left",
            description: "Allows you to defend from your left side.",
            defaultKey: InputKey.Numpad5,
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
