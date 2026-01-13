using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.Patches;
using TaleWorlds.InputSystem;

namespace Bannerlord.FluidCombatNext.HotKeys
{
    public class StabAttackKey : HotKeyBase
    {
        public static bool IsKeyDown { get; set; } = false;

        public StabAttackKey() : base(nameof(StabAttackKey),
            displayName: "Stab Attack",
            description: "Allows you to perform a stab attack.",
            defaultKey: InputKey.X2MouseButton,
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
            RightAttackKey.IsKeyDown = false;
            OverheadAttackKey.IsKeyDown = false;

            LeftAttackPatch.CachedKeyDown = false;
            RightAttackPatch.CachedKeyDown = false;
            OverheadAttackPatch.CachedKeyDown = false;

            IsKeyDown = true;
        }

        protected override void OnReleased()
        {
            IsKeyDown = false;
            StabAttackPatch.CachedKeyDown = false;
        }
    }
}
