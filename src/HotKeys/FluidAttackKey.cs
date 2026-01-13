using System;
using Bannerlord.ButterLib.HotKeys;
using Bannerlord.FluidCombatNext.Helper;
using Bannerlord.FluidCombatNext.Patches;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.FluidCombatNext
{
    class FluidAttackKey : HotKeyBase
	{
		public static bool IsKeyDown { get; set; } = false;

		public FluidAttackKey() : base(nameof(FluidAttackKey),
            displayName: "Fluid Attack",
            description: "Allows you to attack from different directions while holding the key.",
			defaultKey: InputKey.RightControl,
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
				if (Mission.Current.MainAgent.MountAgent != null)
				{
					FluidAttackingPatch.RestrictAttackDirectionVert = FluidCombatSettings.Instance!.AttackRestrictionHorseback.SelectedIndex == 1;
					FluidAttackingPatch.RestrictAttackDirectionHoriz = FluidCombatSettings.Instance!.AttackRestrictionHorseback.SelectedIndex == 2;
					FluidAttackingPatch.IsChangeActive = FluidCombatSettings.Instance!.AttackActiveOnHorseback;
					FluidAttackingPatch.Threshold = FluidCombatSettings.Instance!.AttackHorsebackInputThreshold;
					FluidAttackingPatch.XSensitivity = FluidCombatSettings.Instance!.AttackHorsebackXSensitivity;
					FluidAttackingPatch.YSensitivity = FluidCombatSettings.Instance!.AttackHorsebackYSensitivity;
				}
				else
				{
					FluidAttackingPatch.RestrictAttackDirectionVert = FluidCombatSettings.Instance!.AttackRestriction.SelectedIndex == 1;
					FluidAttackingPatch.RestrictAttackDirectionHoriz = FluidCombatSettings.Instance!.AttackRestriction.SelectedIndex == 2;
					FluidAttackingPatch.IsChangeActive = true;
					FluidAttackingPatch.Threshold = FluidCombatSettings.Instance!.AttackInputThreshold;
					FluidAttackingPatch.XSensitivity = FluidCombatSettings.Instance!.AttackXSensitivity;
					FluidAttackingPatch.YSensitivity = FluidCombatSettings.Instance!.AttackYSensitivity;
				}
			}
			catch (Exception ex)
			{
                MessageHelper.HandleError("FluidAttack Settings Failed", ex.ToString());
            }
		}

		protected override void OnPressed()
		{
			FluidAttackingPatch.PrevAttackType = 0U;
			UpdateSettings();
			IsKeyDown = true;
		}

		protected override void OnReleased()
		{
			IsKeyDown = false;
		}
	}
}
