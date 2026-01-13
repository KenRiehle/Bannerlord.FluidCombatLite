using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.Patches;
using TaleWorlds.InputSystem;

namespace Bannerlord.FluidCombatNext.HotKeys
{
    public class RightAttackKey : HotKeyBase
    {
        public static bool IsKeyDown { get; set; } = false;

        public RightAttackKey() : base(nameof(RightAttackKey),
            displayName: "Right Attack",
            description: "Allows you to perform an attack from the right side.",
            defaultKey: InputKey.Numpad2,
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
            LeftAttackKey.IsKeyDown = false;
            OverheadAttackKey.IsKeyDown = false;
            StabAttackKey.IsKeyDown = false;

            LeftAttackPatch.CachedKeyDown = false;
            OverheadAttackPatch.CachedKeyDown = false;
            StabAttackPatch.CachedKeyDown = false;

            IsKeyDown = true;
        }

        protected override void OnReleased()
        {
            IsKeyDown = false;
            RightAttackPatch.CachedKeyDown = false;
        }
    }
}
