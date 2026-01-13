using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;
using System.Reflection;

namespace Bannerlord.FluidCombatNext
{
    public class FluidCombatSettings : AttributeGlobalSettings<FluidCombatSettings>
    {
        public override string Id { get; } = SubModule.ModuleName;
        public override string DisplayName => SubModule.DisplayName;
        public override string FolderName => GetType().Assembly.GetName().Name;
        public override string FormatType => "json";

        // Attacking
        [SettingPropertyGroup("Attacking Options/Horseback Options")]
        [SettingPropertyBool("Active on Horseback", Order = 0, RequireRestart = false, HintText = "Whether fluid attacking is active while on horseback.")]
        public bool AttackActiveOnHorseback { get; set; } = true;

        [SettingPropertyGroup("Attacking Options/Horseback Options")]
        [SettingPropertyDropdown("Attack Restriction", HintText = "Restricts the attack direction to up and down or left and right, allowing you to use the dedicated keybinds for them instead. Horizontal restriction doesn't apply to polearms.", Order = 1, RequireRestart = false)]
        public Dropdown<string> AttackRestrictionHorseback { get; } = new Dropdown<string>(new string[]
        {
            "None",
            "Vertical Only",
            "Horizontal Only"
        }, 0);

        [SettingPropertyGroup("Attacking Options/Horseback Options")]
        [SettingPropertyFloatingInteger("Input Threshold", 0, 20, "0.0 Units", Order = 2, RequireRestart = false, HintText = "The input threshold above which attack direction changes will be registered, based on the combined X and Y input. Threshold for the first attack is always zero.")]
        public float AttackHorsebackInputThreshold { get; set; } = 11f;

        [SettingPropertyGroup("Attacking Options/Horseback Options")]
        [SettingPropertyFloatingInteger("X Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 3, RequireRestart = false, HintText = "The multiplier for the X mouse axis input value. Used in checking if above the input threshold.")]
        public float AttackHorsebackXSensitivity { get; set; } = 1f;

        [SettingPropertyGroup("Attacking Options/Horseback Options")]
        [SettingPropertyFloatingInteger("Y Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 4, RequireRestart = false, HintText = "The multiplier for the Y mouse axis input value. Used in checking if above the input threshold.")]
        public float AttackHorsebackYSensitivity { get; set; } = 0.7f;


        [SettingPropertyGroup("Attacking Options/On-Foot Options")]
        [SettingPropertyDropdown("Attack Restriction", HintText = "Restricts the attack direction to up and down or left and right, allowing you to use the dedicated keybinds for them instead. Horizontal restriction doesn't apply to polearms.", Order = 0, RequireRestart = false)]
        public Dropdown<string> AttackRestriction { get; } = new Dropdown<string>(new string[]
        {
            "None",
            "Vertical Only",
            "Horizontal Only"
        }, 0);

        [SettingPropertyGroup("Attacking Options/On-Foot Options")]
        [SettingPropertyFloatingInteger("Input Threshold", 0, 20, "0.0 Units", Order = 1, RequireRestart = false, HintText = "The input threshold above which attack direction changes will be registered, based on the combined X and Y input. Threshold for the first attack is always zero.")]
        public float AttackInputThreshold { get; set; } = 11f;

        [SettingPropertyGroup("Attacking Options/On-Foot Options")]
        [SettingPropertyFloatingInteger("X Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 2, RequireRestart = false, HintText = "The multiplier for the X mouse axis input value. Used in checking if above the input threshold.")]
        public float AttackXSensitivity { get; set; } = 1f;

        [SettingPropertyGroup("Attacking Options/On-Foot Options")]
        [SettingPropertyFloatingInteger("Y Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 3, RequireRestart = false, HintText = "The multiplier for the Y mouse axis input value. Used in checking if above the input threshold.")]
        public float AttackYSensitivity { get; set; } = 1.2f;


        // Blocking
        [SettingPropertyGroup("Blocking Options/Horseback Options")]
        [SettingPropertyBool("Active on Horseback", Order = 0, RequireRestart = false, HintText = "Whether fluid blocking is active while on horseback.")]
        public bool BlockActiveOnHorseback { get; set; } = true;

        [SettingPropertyGroup("Blocking Options/Horseback Options")]
        [SettingPropertyDropdown("Defense Restriction", HintText = "Restricts the defense direction to up and down or left and right, allowing you to use the dedicated keybinds for them instead.", Order = 1, RequireRestart = false)]
        public Dropdown<string> DefenseRestrictionHorseback { get; } = new Dropdown<string>(new string[]
        {
            "None",
            "Vertical Only",
            "Horizontal Only"
        }, 0);

        [SettingPropertyGroup("Blocking Options/Horseback Options")]
        [SettingPropertyFloatingInteger("Input Threshold", 0, 20, "0.0 Units", Order = 2, RequireRestart = false, HintText = "The input threshold above which blocking direction changes will be registered, based on the combined X and Y input. Threshold for the first block is always zero.")]
        public float BlockHorsebackInputThreshold { get; set; } = 6f;

        [SettingPropertyGroup("Blocking Options/Horseback Options")]
        [SettingPropertyFloatingInteger("X Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 3, RequireRestart = false, HintText = "The multiplier for the X mouse axis input value. Used in checking if above the input threshold.")]
        public float BlockHorsebackXSensitivity { get; set; } = 1f;

        [SettingPropertyGroup("Blocking Options/Horseback Options")]
        [SettingPropertyFloatingInteger("Y Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 4, RequireRestart = false, HintText = "The multiplier for the Y mouse axis input value. Used in checking if above the input threshold.")]
        public float BlockHorsebackYSensitivity { get; set; } = 1f;


        [SettingPropertyGroup("Blocking Options/On-Foot Options")]
        [SettingPropertyDropdown("Defense Restriction", HintText = "Restricts the defense direction to up and down or left and right, allowing you to use the dedicated keybinds for them instead.", Order = 0, RequireRestart = false)]
        public Dropdown<string> DefenseRestriction { get; } = new Dropdown<string>(new string[]
        {
            "None",
            "Vertical Only",
            "Horizontal Only"
        }, 0);

        [SettingPropertyGroup("Blocking Options/On-Foot Options")]
        [SettingPropertyFloatingInteger("Input Threshold", 0, 20, "0.0 Units", Order = 1, RequireRestart = false, HintText = "The input threshold above which blocking direction changes will be registered, based on the combined X and Y input. Threshold for the first block is always zero.")]
        public float BlockInputThreshold { get; set; } = 6f;

        [SettingPropertyGroup("Blocking Options/On-Foot Options")]
        [SettingPropertyFloatingInteger("X Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 2, RequireRestart = false, HintText = "The multiplier for the X mouse axis input value. Used in checking if above the input threshold.")]
        public float BlockXSensitivity { get; set; } = 1f;

        [SettingPropertyGroup("Blocking Options/On-Foot Options")]
        [SettingPropertyFloatingInteger("Y Axis Sensitivity Multiplier", 0, 5, "0.00x", Order = 3, RequireRestart = false, HintText = "The multiplier for the Y mouse axis input value. Used in checking if above the input threshold.")]
        public float BlockYSensitivity { get; set; } = 1f;
    }
}
