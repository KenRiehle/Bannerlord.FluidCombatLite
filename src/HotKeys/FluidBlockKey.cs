using System;
using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.Patches;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext.HotKeys
{
    public class FluidBlockKey : HotKeyBase
    {
        public static bool IsKeyDown { get; set; } = false;

        public FluidBlockKey() : base(nameof(FluidBlockKey),
            displayName: "Fluid Defend",
            description: "Allows you to defend from different directions while holding the key.",
            defaultKey: InputKey.RightAlt,
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

        private void UpdateSettings()
        {
            try
            {
                //If on a mount
                if (Mission.Current.MainAgent.MountAgent != null)
                {
                    FluidBlockingPatch.RestrictBlockDirectionVert = FluidCombatSettings.Instance!.DefenseRestrictionHorseback.SelectedIndex == 1;
                    FluidBlockingPatch.RestrictBlockDirectionHoriz = FluidCombatSettings.Instance!.DefenseRestrictionHorseback.SelectedIndex == 2;
                    FluidBlockingPatch.IsChangeActive = FluidCombatSettings.Instance!.BlockActiveOnHorseback;
                    FluidBlockingPatch.Threshold = FluidCombatSettings.Instance!.BlockHorsebackInputThreshold;
                    FluidBlockingPatch.XSensitivity = FluidCombatSettings.Instance!.BlockHorsebackXSensitivity;
                    FluidBlockingPatch.YSensitivity = FluidCombatSettings.Instance!.BlockHorsebackYSensitivity;
                }
                else
                {
                    FluidBlockingPatch.RestrictBlockDirectionVert = FluidCombatSettings.Instance!.DefenseRestriction.SelectedIndex == 1;
                    FluidBlockingPatch.RestrictBlockDirectionHoriz = FluidCombatSettings.Instance!.DefenseRestriction.SelectedIndex == 2;
                    FluidBlockingPatch.IsChangeActive = true;
                    FluidBlockingPatch.Threshold = FluidCombatSettings.Instance!.BlockInputThreshold;
                    FluidBlockingPatch.XSensitivity = FluidCombatSettings.Instance!.BlockXSensitivity;
                    FluidBlockingPatch.YSensitivity = FluidCombatSettings.Instance!.BlockYSensitivity;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.HandleError("FluidBlock Settings Failed", ex.ToString());
            }
        }

        protected override void OnPressed()
        {
            FluidBlockingPatch.PrevDefenseType = 0U;
            UpdateSettings();
            IsKeyDown = true;
        }

        protected override void OnReleased()
        {
            IsKeyDown = false;
        }
    }
}
