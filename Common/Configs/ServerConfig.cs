using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using TerraTyping.Core;

namespace TerraTyping.Common.Configs;

public class ServerConfig : ModConfig
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
    public static ServerConfig Instance;

    public override ConfigScope Mode => ConfigScope.ServerSide;

    public override void OnChanged()
    {
        Table.NewMultiplierAndDivisorValues(this);
    }

    private float multiplier = 2;
    [Range(1f, 5f)]
    [Increment(0.1f)]
    [DefaultValue(2)]
    public float Multiplier
    {
        get => multiplier;
        [Obsolete("Only the user should use this.")]
        set
        {
            if (multiplier != value)
            {
                multiplier = value;
                if (DivisorIsInverseOfMultiplier && value != 0)
                {
                    divisor = 1 / value;
                }
            }
        }
    }

    private float divisor = 0.5f;
    [Range(0f, 1f)]
    [Increment(0.1f)]
    [DefaultValue(0.5)]
    public float Divisor
    {
        get => divisor;
        [Obsolete("Only the user should use this.")]
        set
        {
            if (divisor != value)
            {
                divisor = value;
                divisorIsInverseOfMultiplier = false;
            }
        }
    }

    private bool divisorIsInverseOfMultiplier = true;
    [DefaultValue(true)]
    public bool DivisorIsInverseOfMultiplier
    {
        get => divisorIsInverseOfMultiplier;
        [Obsolete("Only the user should use this.")]
        set
        {
            if (divisorIsInverseOfMultiplier != value)
            {
                divisorIsInverseOfMultiplier = value;
                if (value)
                {
                    divisor = 1 / Multiplier;
                }
            }
        }
    }

    [Range(1f, 5f)]
    [Increment(0.1f)]
    [DefaultValue(1.5)]
    public float STAB { get; set; } = 1.5f;

    [Range(1f, 5f)]
    [Increment(0.1f)]
    [DefaultValue(1.5)]
    public float RangedSTAB { get; set; } = 1.5f;

    [Range(1f, 5f)]
    [Increment(0.1f)]
    [DefaultValue(1.0)]
    public float AmmoSTAB { get; set; } = 1.0f;

    [Range(1f, 5f)]
    [Increment(0.05f)]
    [DefaultValue(1.25)]
    public float WeatherMultiplier { get; set; } = 1.25f;

    [DefaultValue(false)]
    public bool WeatherMultOnlyExpert { get; set; } = false;

    [DefaultValue(true)]
    public bool WeatherMultForEnemies { get; set; } = true;

    [Range(0f, 100f)]
    [DefaultValue(0.5f)]
    public float HiddenAbilityChancePercent { get; set; } = 0.5f;

    [Range(0f, 1f)]
    [DefaultValue(0.75f)]
    public float STABDiminishingReturnScalar { get; set; } = 0.75f;

    [Range(0f, 1f)]
    [DefaultValue(0.5f)]
    public float KnockbackScaling { get; set; } = 0.5f;

    [SeparatePage]
    public AbilityConfig AbilityConfigInstance { get; set; } = new AbilityConfig();

    public class AbilityConfig
    {
        [Header("$Config.Header.LightningRod")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float LightningRodDamageBoostPlayer { get; set; } = 1.2f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float LightningRodDamageBoostNPC { get; set; } = 1.5f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float LightningRodDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float LightningRodDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.StormDrain")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float StormDrainDamageBoostPlayer { get; set; } = 1.2f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float StormDrainDamageBoostNPC { get; set; } = 1.5f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float StormDrainDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float StormDrainDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.FlashFire")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float FlashFireDamageBoostPlayer { get; set; } = 1.2f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float FlashFireDamageBoostNPC { get; set; } = 1.5f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float FlashFireDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float FlashFireDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.MotorDrive")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float MotorDriveSpeedBoostPlayer { get; set; } = 1.15f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float MotorDriveSpeedBoostNPC { get; set; } = 1.15f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float MotorDriveDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float MotorDriveDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.Justified")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float JustifiedDamageBoostPlayer { get; set; } = 1.15f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float JustifiedDamageBoostNPC { get; set; } = 1.15f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float JustifiedDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float JustifiedDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.WaterCompaction")]

        [ReloadRequired]
        [Range(1, 40)]
        [Increment(1)]
        [DefaultValue(20)]
        [Slider]
        public int WaterCompactionDefenseBoostPlayer { get; set; } = 20;

        [ReloadRequired]
        [Range(1, 40)]
        [Increment(1)]
        [DefaultValue(20)]
        [Slider]
        public int WaterCompactionDefenseBoostNPC { get; set; } = 20;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float WaterCompactionDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float WaterCompactionDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.SteamEngine")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.4f)]
        public float SteamEngineSpeedBoostPlayer { get; set; } = 1.4f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.4f)]
        public float SteamEngineSpeedBoostNPC { get; set; } = 1.4f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float SteamEngineDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float SteamEngineDurationNPC { get; set; } = 6f;

        [Header("$Config.Header.Mummy")]

        [ReloadRequired]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(10f)]
        public float MummyDurationPlayer { get; set; } = 10f;

        [ReloadRequired]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(15f)]
        public float MummyDurationNPC { get; set; } = 15f;

        [Header("$Config.Header.ColorChange")]

        [ReloadRequired]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(8)]
        public float ColorChangeDurationPlayer { get; set; } = 8;

        [ReloadRequired]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(10)]
        public float ColorChangeDurationNPC { get; set; } = 10;

        [Header("$Config.Header.SandForce")]

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.3f)]
        public float SandForceDamageBoostPlayer { get; set; } = 1.3f;

        [ReloadRequired]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.3f)]
        public float SandForceDamageBoostNPC { get; set; } = 1.3f;
    }
}