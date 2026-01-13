using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.Patches;
using TaleWorlds.InputSystem;

namespace Bannerlord.FluidCombatNext.HotKeys
{
    public class OverheadAttackKey : HotKeyBase
    {
        public static bool IsKeyDown { get; set; } = false;

        public OverheadAttackKey() : base(nameof(OverheadAttackKey),
            displayName: "Overhead Attack",
            description: "Allows you to perform an overhead attack.",
            defaultKey: InputKey.X1MouseButton,
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
            StabAttackKey.IsKeyDown = false;

            LeftAttackPatch.CachedKeyDown = false;
            RightAttackPatch.CachedKeyDown = false;
            StabAttackPatch.CachedKeyDown = false;

            IsKeyDown = true;
        }

        protected override void OnReleased()
        {
            IsKeyDown = false;
            OverheadAttackPatch.CachedKeyDown = false;
        }
    }
}
