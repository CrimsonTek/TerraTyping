using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Common.Configs;
using TerraTyping.Core.Abilities.Buffs;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;

namespace TerraTyping.Core.Abilities
{
    public static class AbilityLookup
    {
        public static ServerConfig.AbilityConfig AbilityConfig => ServerConfig.Instance.AbilityConfigInstance;

        static AbilityLookup()
        {
            InitializeAbilityLookupTable();
        }

        #region Delegates
        public delegate ModifyDamageReturn ModifyDamage(ModifyDamageParameters modifyDamageParameters);
        public static ModifyDamage ModifyDamageDefault => (parameters) =>
        {
            return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
        };
        public struct ModifyDamageReturn
        {
            public float newDamage;
            public float newKnockback;
            public bool heal;

            [Obsolete]
            public ModifyDamageReturn(float newDamage, bool heal, CombatTextInfo textInfo, float newKnockback)
            {
                this.newDamage = newDamage;
                this.newKnockback = newKnockback;
                this.heal = heal;
            }

            public ModifyDamageReturn(float newDamage, bool heal, float newKnockback)
            {
                this.newDamage = newDamage;
                this.newKnockback = newKnockback;
                this.heal = heal;
            }
        }
        public struct ModifyDamageParameters
        {
            public ElementArray incomingElements;
            public float damage;
            public ITarget targetInterface;
            public float knockback;
            public IDamageClass damageClass;
            public Wrapper attackerWrapper;

            public ModifyDamageParameters(ElementArray incoming, float damage, ITarget targetInterface, float knockback, IDamageClass damageClass, Wrapper attackerWrapper)
            {
                incomingElements = incoming;
                this.damage = damage;
                this.targetInterface = targetInterface;
                this.knockback = knockback;
                this.damageClass = damageClass;
                this.attackerWrapper = attackerWrapper;
            }
        }

        public delegate ForceStabWithItemReturn ForceStabWithItem(ForceStabWithItemParameters parameters);
        public static ForceStabWithItem ForceStabWithItemDefault => (paraemeters) => ForceStabWithItemReturn.DoNothing();
        public struct ForceStabWithItemParameters
        {
            public readonly WeaponWrapper weaponWrapper;

            public ForceStabWithItemParameters(WeaponWrapper weaponWrapper)
            {
                this.weaponWrapper = weaponWrapper;
            }
        }
        public struct ForceStabWithItemReturn
        {
            public int AddCount { get; }
            public bool ReplaceStab { get; }
            public int ReplaceCount { get; }

            /// <summary>
            /// Use a static builder.
            /// </summary>
            [Obsolete("Use a static builder.")]
            public ForceStabWithItemReturn()
            {
                AddCount = default;
                ReplaceStab = default;
                ReplaceCount = default;
            }

            private ForceStabWithItemReturn(int addCount, bool replaceStab, int replaceCount)
            {
                AddCount = addCount;
                ReplaceStab = replaceStab;
                ReplaceCount = replaceCount;
            }

            public static ForceStabWithItemReturn AddStabCount(int stabCount)
            {
                return new ForceStabWithItemReturn(stabCount, false, 0);
            }

            public static ForceStabWithItemReturn ReplaceStabCount(int stabCount)
            {
                return new ForceStabWithItemReturn(0, true, stabCount);
            }

            public static ForceStabWithItemReturn DoNothing()
            {
                return new ForceStabWithItemReturn(0, false, 0);
            }
        }

        public delegate ElementArray ModifyAttackType(ElementArray @default);
        public static ModifyAttackType ModifyAttackTypeDefault => (@default) =>
        {
            return @default;
        };

        public delegate float ModifyEffectivenessOutgoing(ModifyEffectivenessOutgoingParameters parameters);
        public static ModifyEffectivenessOutgoing ModifyEffectivenessOutgoingDefault => (parameters) =>
        {
            return parameters.normalEffectiveness;
        };
        public struct ModifyEffectivenessOutgoingParameters
        {
            public Element outgoingType;
            public Element defendingType;
            public float normalEffectiveness;

            public ModifyEffectivenessOutgoingParameters(Element outgoingType, Element defendingType, float normalEffectiveness)
            {
                this.outgoingType = outgoingType;
                this.defendingType = defendingType;
                this.normalEffectiveness = normalEffectiveness;
            }
        }

        public delegate float ModifyEffectivenessIncoming(ModifyEffectivenessIncomingParameters parameters);
        public static ModifyEffectivenessIncoming ModifyEffectivenessIncomingDefault = (parameters) =>
        {
            return parameters.normalEffectiveness;
        };
        public struct ModifyEffectivenessIncomingParameters
        {
            public Element incomingElement;
            public Element defendingElement;
            public float normalEffectiveness;

            public ModifyEffectivenessIncomingParameters(Element incomingElement, Element defendingElement, float normalEffectiveness)
            {
                this.incomingElement = incomingElement;
                this.defendingElement = defendingElement;
                this.normalEffectiveness = normalEffectiveness;
            }
        }

        public delegate float ModifyStabAmount(float defaultStab, bool stab);
        public static ModifyStabAmount ModifyStabAmountDefault => (defaultStab, stab) =>
        {
            return defaultStab;
        };

        public delegate PowerupTypeReturn PowerupType(PowerupTypeParameters parameters);
        public static PowerupType PowerupTypeDefault => (type) =>
        {
            return new PowerupTypeReturn(1);
        };
        public struct PowerupTypeParameters
        {
            public Element element;
            public Wrapper user;
            public Wrapper target;

            public PowerupTypeParameters(Element type, Wrapper user, Wrapper target)
            {
                element = type;
                this.user = user;
                this.target = target;
            }
        }
        public struct PowerupTypeReturn
        {
            public float powerupMultiplier;

            public PowerupTypeReturn(float powerupMultiplier)
            {
                this.powerupMultiplier = powerupMultiplier;
            }
        }

        public delegate void OnHitByType(Element type, ITarget target);
        public static OnHitByType OnHitByTypeDefault => (type, target) => { };

        public delegate void UpdateLifeRegen(ITarget target, TargetType targetType);
        public static UpdateLifeRegen UpdateLifeRegenDefault => (target, type) => { };

        public delegate void BuffOnHit(BuffOnHitParameters buffOnHitParameters);
        public static BuffOnHit BuffOnHitDefault => (parameters) => { };
        public struct BuffOnHitParameters
        {
            public ElementArray incoming;
            public ITarget target;
            public Wrapper attacker;

            public BuffOnHitParameters(ElementArray incoming, ITarget target, Wrapper attacker)
            {
                this.incoming = incoming;
                this.target = target;
                this.attacker = attacker;
            }
        }

        /// <summary>
        /// Used when text should appear when hit by a specific type, eg Dry Skin.
        /// </summary>
        public delegate MessageOnHitReturn MessageOnHit(MessageOnHitParameters messageOnHitParameters);
        public static MessageOnHit MessageOnHitDefault => (parameters) => default;
        public struct MessageOnHitParameters
        {
            public ITarget target;
            public ElementArray incoming;
            public IDamageClass damageClass;
            public Wrapper attacker;

            public MessageOnHitParameters(ElementArray incoming, ITarget target, IDamageClass damageClass, Wrapper attacker)
            {
                this.target = target;
                this.damageClass = damageClass;
                this.attacker = attacker;
                this.incoming = incoming;
            }
        }
        public struct MessageOnHitReturn
        {
            public Message message;

            public static MessageOnHitReturn None => new MessageOnHitReturn(Message.None);

            public MessageOnHitReturn(Message message)
            {
                this.message = message;
            }

            public MessageOnHitReturn(string text, Element elementColor)
            {
                message = new Message(text, elementColor);
            }
        }

        public delegate MessageHitEnemyReturn MessageHitEnemy(MessageHitEnemyParameters messageHitParameters);
        public static MessageHitEnemy MessageHitEnemyDefault => (parameters) => { return default; };
        public struct MessageHitEnemyParameters
        {
            public ElementArray outgoing;
            public IDefensiveElements primaryType;

            public MessageHitEnemyParameters(ElementArray outgoing, IDefensiveElements defender)
            {
                this.outgoing = outgoing;
                primaryType = defender;
            }
        }
        public struct MessageHitEnemyReturn
        {
            public Message message;

            public MessageHitEnemyReturn(Message message)
            {
                this.message = message;
            }
        }

        public delegate bool AttractProjectile(AttractProjectileParameters attractProjectileParameters);
        public static AttractProjectile AttractProjectileDefault => (parameters) => false;
        public struct AttractProjectileParameters
        {
            public ProjectileWrapper projectile;
            public Wrapper target;

            public AttractProjectileParameters(ProjectileWrapper projectileWrapper, Wrapper target)
            {
                projectile = projectileWrapper;
                this.target = target;
            }
        }
        public interface IAttractProjectileTarget : ITarget, ITeam { }

        public delegate Ability ModifyOpponentsAbility(Ability defaultAbility);
        public static ModifyOpponentsAbility ModifyOpponentsAbilityDefault => (defaultAbility) => { return defaultAbility; };
        #endregion

        #region Factories
        public struct Message
        {
            public bool hasMessage;
            public string text;
            public bool setColor;
            public Color textColor;
            public ElementArray elements;

            public static Message None => new Message()
            {
                hasMessage = false,
                text = string.Empty,
                elements = ElementArray.Default
            };

            public Message(string text)
            {
                hasMessage = true;
                this.text = text;
                setColor = false;
                textColor = default;
                elements = ElementArray.Default;
            }

            public Message(string text, Color color)
            {
                hasMessage = true;
                this.text = text;
                setColor = true;
                textColor = color;
                elements = ElementArray.Default;
            }

            public Message(string text, Element type)
            {
                hasMessage = true;
                this.text = text;
                setColor = true;
                textColor = TerraTypingColors.GetColor(type);
                elements = ElementArray.Default;
            }

            public Message(string text, ElementArray elements)
            {
                hasMessage = true;
                this.text = text;
                setColor = true;
                textColor = default;
                this.elements = elements;
            }
        }
        struct BuffOnHitData
        {
            public bool addBuff;
            public int modBuff;
            public bool quiet;
            private readonly Func<float> timePlayer;
            private readonly Func<float> timeNPC;

            public static BuffOnHitData DoNothing => new BuffOnHitData()
            {
                addBuff = false,
                modBuff = -1,
                quiet = false,
            };

            public int TimePlayer { get => (int)(timePlayer.Invoke() * 60); }
            public int TimeNPC { get => (int)(timeNPC.Invoke() * 60); }

            public BuffOnHitData(int modBuff, bool quiet, Func<float> timePlayer, Func<float> timeNPC)
            {
                addBuff = true;
                this.modBuff = modBuff;
                this.quiet = quiet;
                this.timePlayer = timePlayer ?? throw new ArgumentNullException(nameof(timePlayer));
                this.timeNPC = timeNPC ?? throw new ArgumentNullException(nameof(timeNPC));
            }
        }

        static ModifyDamage AbsorbFactory(Element typeToAbsorb, bool healFromAbsorb)
        {
            return (parameters) =>
            {
                if (parameters.incomingElements.HasElement(typeToAbsorb))
                {
                    return new ModifyDamageReturn(0, healFromAbsorb, 0);
                }
                return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
            };
        }
        static BuffOnHit BuffOnHitFactory(BuffOnHitData buffOnHitData, Element typeThatBuffs) => BuffOnHitFactory(buffOnHitData, new Element[] { typeThatBuffs });
        static BuffOnHit BuffOnHitFactory(BuffOnHitData buffOnHitData, Element[] typesThatBuff)
        {
            return (parameters) =>
            {
                // typesThatBuff.Contains(parameters.incoming)
                if (buffOnHitData.addBuff && parameters.incoming.HasAnyElement(typesThatBuff))
                {
                    int time = buffOnHitData.TimePlayer;
                    if (parameters.target.EntityType == EntityType.NPC)
                    {
                        time = buffOnHitData.TimeNPC;
                    }
                    parameters.target.AddBuff(buffOnHitData.modBuff, time, buffOnHitData.quiet);
                }
            };
        }

        static MessageOnHit MessageOnHitFactory(Element[] types, string text)
        {
            return (parameters) =>
            {
                for (int i = 0; i < types.Length; i++)
                {
                    for (int j = 0; j < parameters.incoming.Length; j++)
                    {
                        if (parameters.incoming[j] == types[j])
                        {
                            return new MessageOnHitReturn(text, types[j]);
                        }
                    }
                }

                return MessageOnHitReturn.None;
            };
        }
        static MessageOnHit MessageOnHitFactory(Element type, string text)
        {
            return (parameters) =>
            {
                for (int i = 0; i < parameters.incoming.Length; i++)
                {
                    if (parameters.incoming[i] == type)
                    {
                        return new MessageOnHitReturn(text, type);
                    }
                }

                return MessageOnHitReturn.None;
            };
        }
        #endregion

        public static AbilityFlyweight GetAbility(Ability abilityID)
        {
            if (abilityLookupTable.TryGetValue(abilityID, out AbilityFlyweight ability))
            {
                return ability;
            }
            else
            {
                return abilityLookupTable[Ability.None];
            }
        }
        public static AbilityFlyweight GetAbility(IAbility abilityEntity)
        {
            Ability ability = abilityEntity?.GetAbility ?? Ability.None;
            if (abilityLookupTable.ContainsKey(ability))
            {
                return abilityLookupTable[ability];
            }
            else
            {
                return abilityLookupTable[Ability.None];
            }
        }

        static readonly Dictionary<Ability, AbilityFlyweight> abilityLookupTable = new Dictionary<Ability, AbilityFlyweight>() { };
        static void InitializeAbilityLookupTable()
        {
            AddAbility(Ability.None, new AbilityFlyweight(Ability.None));
            AddAbility(Ability.Levitate, new AbilityFlyweight(Ability.Levitate)
            {
                modifyDamageIncoming = AbsorbFactory(Element.ground, false)
            });
            AddAbility(Ability.VoltAbsorb, new AbilityFlyweight(Ability.VoltAbsorb)
            {
                modifyDamageIncoming = AbsorbFactory(Element.electric, true),
                messageOnHit = MessageOnHitFactory(Element.electric, "Volt Absorb!")
            });
            AddAbility(Ability.LightningRod, new AbilityFlyweight(Ability.LightningRod)
            {
                modifyDamageIncoming = AbsorbFactory(Element.electric, false),
                buffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<LightningRod>(), false,
                () => AbilityConfig.LightningRodDurationPlayer,
                () => AbilityConfig.LightningRodDurationNPC), Element.electric),
                messageOnHit = MessageOnHitFactory(Element.electric, "Lightning Rod!"),
                attractProjectile = (parameters) =>
                {
                    if (parameters.target is ITeam targetTeam &&
                        parameters.target is ITarget targetTarget)
                    {
                        if (targetTeam.GetTeam() != parameters.projectile.GetTeam() &&
                            !targetTarget.Immortal && targetTarget.Active &&
                            parameters.projectile.OffensiveElements.HasElement(Element.electric))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            });
            AddAbility(Ability.StormDrain, new AbilityFlyweight(Ability.StormDrain)
            {
                modifyDamageIncoming = AbsorbFactory(Element.water, false),
                buffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<StormDrain>(), false,
                () => AbilityConfig.StormDrainDurationPlayer,
                () => AbilityConfig.StormDrainDurationNPC), Element.water),
                messageOnHit = MessageOnHitFactory(Element.water, "Storm Drain!"),
                attractProjectile = (parameters) =>
                {
                    if (parameters.target is ITeam targetTeam &&
                        parameters.target is ITarget targetTarget)
                    {
                        if (targetTeam.GetTeam() != parameters.projectile.GetTeam() &&
                            !targetTarget.Immortal && targetTarget.Active &&
                            parameters.projectile.OffensiveElements.HasElement(Element.water))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            });
            AddAbility(Ability.MotorDrive, new AbilityFlyweight(Ability.MotorDrive)
            {
                modifyDamageIncoming = AbsorbFactory(Element.electric, false),
                buffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<MotorDrive>(), false,
                () => AbilityConfig.MotorDriveDurationPlayer,
                () => AbilityConfig.MotorDriveDurationNPC), Element.electric),
                messageOnHit = MessageOnHitFactory(Element.electric, "Motor Drive!")
            });
            AddAbility(Ability.WaterAbsorb, new AbilityFlyweight(Ability.WaterAbsorb)
            {
                modifyDamageIncoming = AbsorbFactory(Element.water, true),
                messageOnHit = MessageOnHitFactory(Element.water, "Water Absorb!")
            });
            AddAbility(Ability.FlashFire, new AbilityFlyweight(Ability.FlashFire)
            {
                modifyDamageIncoming = AbsorbFactory(Element.fire, false),
                buffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<FlashFire>(), false,
                () => AbilityConfig.FlashFireDurationPlayer,
                () => AbilityConfig.FlashFireDurationNPC), Element.fire),
                messageOnHit = MessageOnHitFactory(Element.fire, "Flash Fire!")
            });
            AddAbility(Ability.SapSipper, new AbilityFlyweight(Ability.SapSipper)
            {
                modifyDamageIncoming = AbsorbFactory(Element.grass, false),
                messageOnHit = MessageOnHitFactory(Element.grass, "Sap Sipper!")
            });
            AddAbility(Ability.ThickFat, new AbilityFlyweight(Ability.ThickFat)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;

                    if (parameters.incomingElements.HasAnyElement(Element.ice, Element.fire))
                    {
                        damage *= Table.Divisor;
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                messageOnHit = (parameters) =>
                {
                    if (parameters.incoming.HasAnyElement(Element.ice, Element.fire, out Element firstFound))
                    {
                        return new MessageOnHitReturn("Thick Fat!", firstFound);
                    }
                    return MessageOnHitReturn.None;
                }
            });
            AddAbility(Ability.Heatproof, new AbilityFlyweight(Ability.Heatproof)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        return new ModifyDamageReturn(parameters.damage * Table.Divisor, false, parameters.knockback);
                    }
                    return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                },
                messageOnHit = MessageOnHitFactory(Element.fire, "Heatproof!")
            });
            AddAbility(Ability.WaterBubble, new AbilityFlyweight(Ability.WaterBubble)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        return new ModifyDamageReturn(parameters.damage * Table.Divisor, false, parameters.knockback);
                    }
                    return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                },
                powerupType = (parameters) =>
                {
                    if (parameters.element == Element.water)
                    {
                        return new PowerupTypeReturn(Table.Multiplier);
                    }
                    else
                    {
                        return new PowerupTypeReturn(1);
                    }
                },
                messageOnHit = MessageOnHitFactory(Element.fire, "Water Bubble!")
            });
            AddAbility(Ability.Fluffy, new AbilityFlyweight(Ability.Fluffy)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;
                    if (parameters.damageClass.Melee)
                    {
                        damage *= Table.Divisor;
                    }

                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        damage *= Table.Multiplier;
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                messageOnHit = (parameters) =>
                {
                    if (parameters.incoming.HasElement(Element.fire))
                    {
                        return new MessageOnHitReturn("Fluffy!", Element.fire);
                    }

                    if (parameters.damageClass.Melee)
                    {
                        return new MessageOnHitReturn("Fluffy!", Element.ice);
                    }

                    return MessageOnHitReturn.None;
                }
            });
            AddAbility(Ability.Justified, new AbilityFlyweight(Ability.Justified)
            {
                buffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<Justified>(), false,
                () => AbilityConfig.JustifiedDurationPlayer,
                () => AbilityConfig.JustifiedDurationNPC), Element.dark),
                messageOnHit = MessageOnHitFactory(Element.dark, "Justified!")
            });
            AddAbility(Ability.WaterCompaction, new AbilityFlyweight(Ability.WaterCompaction)
            {
                buffOnHit = (parameters) =>
                {
                    if (parameters.incoming.HasElement(Element.water))
                    {
                        int time;
                        if (parameters.target is NPCWrapper npcWrapper)
                        {
                            time = (int)(AbilityConfig.WaterCompactionDurationNPC * 180);
                        }
                        else
                        {
                            time = (int)(AbilityConfig.WaterCompactionDurationPlayer * 180);
                        }
                        parameters.target.AddBuff(ModContent.BuffType<WaterCompaction>(), time, false);
                    }
                },
                messageOnHit = MessageOnHitFactory(Element.water, "Water Compaction!")
            });
            AddAbility(Ability.SteamEngine, new AbilityFlyweight(Ability.SteamEngine)
            {
                buffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<SteamEngine>(), false,
                () => AbilityConfig.SteamEngineDurationPlayer,
                () => AbilityConfig.SteamEngineDurationNPC), new Element[] { Element.water, Element.fire }),
                messageOnHit = (parameters) =>
                {
                    if (parameters.incoming.HasAnyElement(Element.water, Element.fire, out Element selected))
                    {
                        return new MessageOnHitReturn("Steam Engine!", selected);
                    }
                    else
                    {
                        return MessageOnHitReturn.None;
                    }
                }
            });
            AddAbility(Ability.DrySkin, new AbilityFlyweight(Ability.DrySkin)
            {
                updateLifeRegen = (target, targetType) =>
                {
                    bool raining = Main.raining && !target.ZoneDesert && !target.ZoneSnow;
                    bool snowing = Main.raining && target.ZoneSnow;
                    bool surface = target.GetRect().Center.Y >= Main.worldSurface;
                    bool hell = target.GetRect().Center.Y <= Main.maxTilesY - 200;
                    bool aroundNoon = false;
                    if (Main.dayTime)
                    {
                        Time lowerTimeBound = MyUtils.TimeConverter(8, 0);
                        Time upperTimeBound = MyUtils.TimeConverter(16, 0);
                        if (Main.time >= lowerTimeBound.time && Main.time <= upperTimeBound.time)
                        {
                            aroundNoon = true;
                        }
                    }

                    bool heal = raining && !hell;
                    bool damage = aroundNoon && !raining && surface && !snowing && !target.ZoneBeach || hell;

                    if (heal)
                    {
                        if (target.LifeRegen <= 0)
                        {
                            target.LifeRegen += 4;
                        }
                    }
                    if (damage)
                    {
                        if (target.LifeRegen > 0)
                        {
                            target.LifeRegen = 0;
                        }
                        target.LifeRegenTime = 0;
                        target.LifeRegen -= 4;
                    }
                },
                modifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;
                    bool heal = false;
                    for (int i = 0; i < parameters.incomingElements.Length; i++)
                    {
                        Element element = parameters.incomingElements[i];
                        if (element == Element.fire)
                        {
                            damage *= 1.25f;
                        }
                        else if (element == Element.water)
                        {
                            damage = 0;
                            heal = true;
                        }
                    }
                    return new ModifyDamageReturn(damage, heal, parameters.knockback);
                },
                messageOnHit = MessageOnHitFactory(new Element[] { Element.fire, Element.water }, "Dry Skin!")
            });
            AddAbility(Ability.Mummy, new AbilityFlyweight(Ability.Mummy)
            {
                buffOnHit = (parameters) =>
                {
                    if (parameters.attacker is WeaponWrapper itemWrapper)
                    {
                        Player player = Main.player[itemWrapper.Player];
                        player.AddBuff(ModContent.BuffType<Mummy>(), (int)(AbilityConfig.MummyDurationPlayer * 180));
                    }
                    else if (parameters.attacker is ProjectileWrapper projWrapper)
                    {
                        if (projWrapper.OwnerType == Owner.Player)
                        {
                            Player player = Main.player[projWrapper.Projectile.owner];
                            player.AddBuff(ModContent.BuffType<Mummy>(), (int)(AbilityConfig.MummyDurationPlayer * 180));
                        }
                    }
                    else if (parameters.attacker is NPCWrapper npcWrapper)
                    {
                        NPC npc = npcWrapper.NPC;
                        npc.AddBuff(ModContent.BuffType<Mummy>(), (int)(AbilityConfig.MummyDurationNPC * 180));
                    }
                },
                messageOnHit = (parameters) =>
                {
                    if (parameters.attacker is WeaponWrapper or ProjectileWrapper or NPCWrapper)
                    {
                        return new MessageOnHitReturn("Mummy!", Element.ghost);
                    }

                    return MessageOnHitReturn.None;
                }
            });
            AddAbility(Ability.Corrosion, new AbilityFlyweight(Ability.Corrosion)
            {
                modifyEffectivenessOutgoing = (parameters) =>
                {
                    if (parameters.outgoingType == Element.poison)
                    {
                        if (parameters.defendingType is Element.poison or Element.steel)
                        {
                            if (parameters.normalEffectiveness < 1)
                            {
                                return 1;
                            }
                        }
                    }

                    return parameters.normalEffectiveness;
                },
                messageHitEnemy = (parameters) =>
                {
                    if (parameters.outgoing.HasElement(Element.poison))
                    {
                        if (parameters.primaryType.DefensiveElements.HasAnyElement(Element.poison, Element.steel))
                        {
                            return new MessageHitEnemyReturn(new Message("Corrosion!", Element.poison));
                        }
                    }
                    return new MessageHitEnemyReturn(Message.None);
                }
            });
            AddAbility(new AbilityFlyweight(Ability.ColorChange)
            {
                buffOnHit = (parameters) =>
                {
                    int duration;
                    if (parameters.target is PlayerWrapper)
                    {
                        duration = (int)(AbilityConfig.ColorChangeDurationPlayer * 180);
                    }
                    else
                    {
                        duration = (int)(AbilityConfig.ColorChangeDurationNPC * 180);
                    }

                    int colorChangeBuffID = ModContent.BuffType<ColorChange>();
                    parameters.target.ModifiedElements = parameters.incoming;
                    parameters.target.AddBuff(colorChangeBuffID, duration);
                },
                messageOnHit = (parameters) => new MessageOnHitReturn(new Message("Color Change!", parameters.incoming))
            });
            AddAbility(Ability.MoldBreaker, new AbilityFlyweight(Ability.MoldBreaker)
            {
                modifyOpponentsAbility = (defaultAbility) =>
                {
                    return Ability.None;
                }
            });
            AddAbility(Ability.SandForce, new AbilityFlyweight(Ability.SandForce)
            {
                powerupType = (parameters) =>
                {
                    Element type = parameters.element;
                    bool appropriateType = false;

                    switch (type)
                    {
                        case Element.rock:
                        case Element.ground:
                        case Element.steel:
                            appropriateType = true;
                            break;
                        default:
                            appropriateType = false;
                            break;
                    }

                    float damageBoost = 1;
                    if (appropriateType && parameters.user is ITarget target && target.ZoneSandstorm)
                    {
                        if (target.EntityType is EntityType.NPC)
                        {
                            damageBoost = AbilityConfig.SandForceDamageBoostNPC;
                        }
                        else
                        {
                            damageBoost = AbilityConfig.SandForceDamageBoostPlayer;
                        }
                    }
                    return new PowerupTypeReturn(damageBoost);
                }
            });
            AddAbility(Ability.Scrappy, new AbilityFlyweight(Ability.Scrappy)
            {
                modifyEffectivenessOutgoing = (parameters) =>
                {
                    if (parameters.defendingType == Element.ghost)
                    {
                        if (parameters.outgoingType is Element.normal or Element.fighting)
                        {
                            if (parameters.normalEffectiveness < 1)
                            {
                                return 1f;
                            }
                        }
                    }
                    return parameters.normalEffectiveness;
                }
            });
            AddAbility(Ability.Flammable, new AbilityFlyweight(Ability.Flammable)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;
                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        damage *= Table.Multiplier;
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                }
            });
            AddAbility(Ability.Grounded, new AbilityFlyweight(Ability.Grounded)
            {
                modifyEffectivenessIncoming = (parameters) =>
                {
                    if (parameters.defendingElement == Element.flying && parameters.incomingElement == Element.ground && parameters.normalEffectiveness < 1)
                    {
                        return 1;
                    }

                    return parameters.normalEffectiveness;
                }
            });
            AddAbility(Ability.PrismArmor, new AbilityFlyweight(Ability.PrismArmor)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;
                    if (parameters.damage > 1)
                    {
                        damage *= 0.75f;
                        if (parameters.damage < 1)
                        {
                            damage = 1;
                        }

                        return new ModifyDamageReturn(damage, false, parameters.knockback);
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                }
            });
            AddAbility(Ability.DD2Stab, new AbilityFlyweight(Ability.DD2Stab)
            {
                modifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                forceStabWithItem = (parameters) =>
                {
                    if (parameters.weaponWrapper is not WeaponWrapper weaponWrapper)
                    {
                        return ForceStabWithItemReturn.DoNothing();
                    }

                    Player player = Main.player[weaponWrapper.Player];
                    if (player is null)
                    {
                        return ForceStabWithItemReturn.DoNothing();
                    }

                    Item[] armor = player.armor;
                    int helm = armor[0]?.type ?? -1;
                    int chest = armor[1]?.type ?? -1;
                    int legs = armor[2]?.type ?? -1;

                    bool itemMatchesArmor;
                    switch (weaponWrapper.item?.type)
                    {
                        case ItemID.DD2FlameburstTowerT1Popper:
                        case ItemID.DD2FlameburstTowerT2Popper:
                        case ItemID.DD2FlameburstTowerT3Popper:
                            itemMatchesArmor = helm == ItemID.ApprenticeHat && chest == ItemID.ApprenticeRobe && legs == ItemID.ApprenticeTrousers || helm == ItemID.ApprenticeAltHead && chest == ItemID.ApprenticeAltShirt && legs == ItemID.ApprenticeAltPants;
                            break;

                        case ItemID.DD2ExplosiveTrapT1Popper:
                        case ItemID.DD2ExplosiveTrapT2Popper:
                        case ItemID.DD2ExplosiveTrapT3Popper:
                            itemMatchesArmor = helm == ItemID.HuntressWig && chest == ItemID.HuntressJerkin && legs == ItemID.HuntressPants || helm == ItemID.HuntressAltHead && chest == ItemID.HuntressAltShirt && legs == ItemID.HuntressAltPants;
                            break;

                        case ItemID.DD2LightningAuraT1Popper:
                        case ItemID.DD2LightningAuraT2Popper:
                        case ItemID.DD2LightningAuraT3Popper:
                            itemMatchesArmor = helm == ItemID.MonkBrows && chest == ItemID.MonkShirt && legs == ItemID.MonkPants || helm == ItemID.MonkAltHead && chest == ItemID.MonkAltShirt && legs == ItemID.MonkAltPants;
                            break;

                        case ItemID.DD2BallistraTowerT1Popper:
                        case ItemID.DD2BallistraTowerT2Popper:
                        case ItemID.DD2BallistraTowerT3Popper:
                            itemMatchesArmor = helm == ItemID.SquireGreatHelm && chest == ItemID.SquirePlating && legs == ItemID.SquireGreaves || helm == ItemID.SquireAltHead && chest == ItemID.SquireAltShirt && legs == ItemID.SquireAltPants;
                            break;

                        default:
                            itemMatchesArmor = false;
                            break;
                    }

                    if (itemMatchesArmor)
                    {
                        return ForceStabWithItemReturn.ReplaceStabCount(1);
                    }

                    return ForceStabWithItemReturn.DoNothing();
                }
            });
        }

        private static void AddAbility(Ability none, AbilityFlyweight ability)
        {
            abilityLookupTable.Add(ability.id, ability);
        }

        private static void AddAbility(AbilityFlyweight ability)
        {
            abilityLookupTable.Add(ability.id, ability);
        }
    }
}
