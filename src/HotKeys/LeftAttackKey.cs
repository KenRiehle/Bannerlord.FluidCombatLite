using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.Patches;
using TaleWorlds.InputSystem;

namespace Bannerlord.FluidCombatNext.HotKeys
{
    public class LeftAttackKey : HotKeyBase
    {
        public static bool IsKeyDown { get; set; } = false;

        public LeftAttackKey() : base(nameof(LeftAttackKey),
            displayName: "Left Attack",
            description: "Allows you to perform an attack from the left side.",
            defaultKey: InputKey.Numpad1,
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
            //Clear other key statuses
            RightAttackKey.IsKeyDown = false;
            OverheadAttackKey.IsKeyDown = false;
            StabAttackKey.IsKeyDown = false;

            RightAttackPatch.CachedKeyDown = false;
            OverheadAttackPatch.CachedKeyDown = false;
            StabAttackPatch.CachedKeyDown = false;

            IsKeyDown = true;
        }

        protected override void OnReleased()
        {
            IsKeyDown = false;
            LeftAttackPatch.CachedKeyDown = false;
        }
    }
}
