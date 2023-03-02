using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Abilities.Buffs;
using TerraTyping.Data;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;

namespace TerraTyping.Abilities
{
    public static class AbilityLookup
    {
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

            [Obsolete] public ModifyDamageReturn(float newDamage, bool heal, CombatTextInfo textInfo, float newKnockback)
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
                this.incomingElements = incoming;
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
            public Element type;
            public Wrapper user;
            public Wrapper target;

            public PowerupTypeParameters(Element type, Wrapper user, Wrapper target)
            {
                this.type = type;
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
        public static MessageOnHit MessageOnHitDefault => (parameters) => { return default; };
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
                this.primaryType = defender;
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

        public delegate AbilityID ModifyOpponentsAbility(AbilityID defaultAbility);
        public static ModifyOpponentsAbility ModifyOpponentsAbilityDefault => (defaultAbility) => { return defaultAbility; };
        #endregion

        #region Factories
        public struct Message
        {
            public bool hasMessage;
            public string text;
            public bool setColor;
            public Color textColor;

            public static Message None => new Message()
            {
                hasMessage = false,
                text = string.Empty,
            };

            public Message(string text)
            {
                hasMessage = true;
                this.text = text;
                setColor = false;
                textColor = default;
            }

            public Message(string text, Color color)
            {
                hasMessage = true;
                this.text = text;
                setColor = true;
                textColor = color;
            }
            
            public Message(string text, Element type)
            {
                hasMessage = true;
                this.text = text;
                setColor = true;
                textColor = ElementColors.GetColor(type);
            }
        }
        struct BuffOnHitData
        {
            public bool addBuff;
            public int modBuff;
            public int time;
            public int timeNPC;
            public bool quiet;

            public static BuffOnHitData DoNothing => new BuffOnHitData()
            {
                addBuff = false,
                modBuff = -1,
                time = 0,
                quiet = false,
            };

            public BuffOnHitData(int modBuff, int timePlayer, int timeNPC, bool quiet)
            {
                addBuff = true;
                this.modBuff = modBuff;
                this.time = timePlayer;
                this.timeNPC = timeNPC;
                this.quiet = quiet;
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
                    int time = buffOnHitData.time;
                    if (parameters.target.EntityType == EntityType.NPC)
                    {
                        time = buffOnHitData.timeNPC;
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

        public static Ability GetAbility(AbilityID abilityID)
        {
            if (abilityLookupTable.TryGetValue(abilityID, out Ability ability))
            {
                return ability;
            }
            else
            {
                return abilityLookupTable[AbilityID.None];
            }
        }
        public static Ability GetAbility(IAbility abilityEntity)
        {
            AbilityID ability = abilityEntity.GetAbility;
            if (abilityLookupTable.ContainsKey(ability))
            {
                return abilityLookupTable[ability];
            }
            else
            {
                return abilityLookupTable[AbilityID.None];
            }
        }

        static readonly Dictionary<AbilityID, Ability> abilityLookupTable = new Dictionary<AbilityID, Ability>() { };
        static void InitializeAbilityLookupTable()
        {
            AddAbility(AbilityID.None, new Ability(AbilityID.None));
            AddAbility(AbilityID.Levitate, new Ability(AbilityID.Levitate)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.ground, false)
            });
            AddAbility(AbilityID.VoltAbsorb, new Ability(AbilityID.VoltAbsorb)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.electric, true),
                MessageOnHit = MessageOnHitFactory(Element.electric, "Volt Absorb!")
            });
            AddAbility(AbilityID.LightningRod, new Ability(AbilityID.LightningRod)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.electric, false),
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<LightningRod>(),
                AbilityData.lightningRodDurationPlayer,
                AbilityData.lightningRodDurationNPC, false), Element.electric),
                MessageOnHit = MessageOnHitFactory(Element.electric, "Lightning Rod!"),
                AttractProjectile = (parameters) =>
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
            AddAbility(AbilityID.StormDrain, new Ability(AbilityID.StormDrain)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.water, false),
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<StormDrain>(),
                AbilityData.stormDrainDurationPlayer,
                AbilityData.stormDrainDurationNPC, false), Element.water),
                MessageOnHit = MessageOnHitFactory(Element.water, "Storm Drain!"),
                AttractProjectile = (parameters) =>
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
            AddAbility(AbilityID.MotorDrive, new Ability(AbilityID.MotorDrive)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.electric, false),
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<MotorDrive>(),
                AbilityData.motorDriveDurationPlayer,
                AbilityData.motorDriveDurationNPC, false), Element.electric),
                MessageOnHit = MessageOnHitFactory(Element.electric, "Motor Drive!")
            });
            AddAbility(AbilityID.WaterAbsorb, new Ability(AbilityID.WaterAbsorb)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.water, true),
                MessageOnHit = MessageOnHitFactory(Element.water, "Water Absorb!")
            });
            AddAbility(AbilityID.FlashFire, new Ability(AbilityID.FlashFire)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.fire, false),
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<FlashFire>(),
                AbilityData.flashFireDurationPlayer,
                AbilityData.flashFireDurationNPC, false), Element.fire),
                MessageOnHit = MessageOnHitFactory(Element.fire, "Flash Fire!")
            });
            AddAbility(AbilityID.SapSipper, new Ability(AbilityID.SapSipper)
            {
                ModifyDamageIncoming = AbsorbFactory(Element.grass, false),
                MessageOnHit = MessageOnHitFactory(Element.grass, "Sap Sipper!")
            });
            AddAbility(AbilityID.ThickFat, new Ability(AbilityID.ThickFat)
            {
                ModifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;

                    if (parameters.incomingElements.HasAnyElement(Element.ice, Element.fire))
                    {
                        damage *= AbilityData.thickFatFireIceDamageTaken;
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                MessageOnHit = (parameters) =>
                {
                    if (parameters.incoming.HasAnyElement(Element.ice, Element.fire, out Element firstFound))
                    {
                        return new MessageOnHitReturn("Thick Fat!", firstFound);
                    }
                    return MessageOnHitReturn.None;
                }
            });
            AddAbility(AbilityID.Heatproof, new Ability(AbilityID.Heatproof)
            {
                ModifyDamageIncoming = (parameters) =>
                {
                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        return new ModifyDamageReturn(parameters.damage * AbilityData.heatproofFireDamageTaken, false, parameters.knockback);
                    }
                    return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                },
                MessageOnHit = MessageOnHitFactory(Element.fire, "Heatproof!")
            });
            AddAbility(AbilityID.WaterBubble, new Ability(AbilityID.WaterBubble)
            {
                ModifyDamageIncoming = (parameters) =>
                {
                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        return new ModifyDamageReturn(parameters.damage * AbilityData.waterBubbleFireDamageTaken, false, parameters.knockback);
                    }
                    return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                },
                PowerupType = (parameters) =>
                {
                    if (parameters.type == Element.water)
                    {
                        return new PowerupTypeReturn(AbilityData.waterBubbleWaterDamageBoost);
                    }
                    else
                    {
                        return new PowerupTypeReturn(1);
                    }
                },
                MessageOnHit = MessageOnHitFactory(Element.fire, "Water Bubble!")
            });
            AddAbility(AbilityID.Fluffy, new Ability(AbilityID.Fluffy)
            {
                ModifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;
                    if (parameters.damageClass.Melee)
                    {
                        damage *= AbilityData.fluffyMeleeDamageTaken;
                    }

                    if (parameters.incomingElements.HasElement(Element.fire))
                    {
                        damage *= AbilityData.fluffyFireDamageTaken;
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                MessageOnHit = (parameters) =>
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
            AddAbility(AbilityID.Justified, new Ability(AbilityID.Justified)
            {
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<Justified>(),
                AbilityData.justifiedDurationPlayer,
                AbilityData.justifiedDurationNPC, false), Element.dark),
                MessageOnHit = MessageOnHitFactory(Element.dark, "Justified!")
            });
            AddAbility(AbilityID.WaterCompaction, new Ability(AbilityID.WaterCompaction)
            {
                BuffOnHit = (parameters) =>
                {
                    if (parameters.incoming.HasElement(Element.water))
                    {
                        if (parameters.target is NPCWrapper npcWrapper)
                        {
                            npcWrapper.NPC.defense += AbilityData.waterCompactionDefenseBoostNPC;
                        }
                        else
                        {
                            int time = AbilityData.waterCompactionDurationPlayer;
                            parameters.target.AddBuff(ModContent.BuffType<WaterCompaction>(), time, false);
                        }
                    }
                },
                MessageOnHit = MessageOnHitFactory(Element.water, "Water Compaction!")
            });
            AddAbility(AbilityID.SteamEngine, new Ability(AbilityID.SteamEngine)
            {
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<SteamEngine>(),
                AbilityData.steamEngineDurationPlayer,
                AbilityData.steamEngineDurationNPC, false), new Element[] { Element.water, Element.fire }),
                MessageOnHit = (parameters) =>
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
            AddAbility(AbilityID.DrySkin, new Ability(AbilityID.DrySkin)
            {
                UpdateLifeRegen = (target, targetType) =>
                {
                    bool raining = Main.raining && !target.ZoneDesert && !target.ZoneSnow;
                    bool snowing = Main.raining && target.ZoneSnow;
                    bool surface = target.GetRect().Center.Y >= Main.worldSurface;
                    bool hell = target.GetRect().Center.Y <= (Main.maxTilesY - 200);
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
                    bool damage = (aroundNoon && !raining && surface && !snowing && !target.ZoneBeach) || hell;

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
                ModifyDamageIncoming = (parameters) =>
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
                MessageOnHit = MessageOnHitFactory(new Element[] { Element.fire, Element.water }, "Dry Skin!")
            });
            AddAbility(AbilityID.Mummy, new Ability(AbilityID.Mummy)
            {
                BuffOnHit = (parameters) =>
                {
                    if (parameters.attacker is WeaponWrapper itemWrapper)
                    {
                        Player player = Main.player[itemWrapper.Player];
                        player.AddBuff(ModContent.BuffType<Mummy>(), AbilityData.mummyDurationPlayer);
                    }
                    else if (parameters.attacker is ProjectileWrapper projWrapper)
                    {
                        if (projWrapper.OwnerType == Owner.Player)
                        {
                            Player player = Main.player[projWrapper.Projectile.owner];
                            player.AddBuff(ModContent.BuffType<Mummy>(), AbilityData.mummyDurationPlayer);
                        }
                    }
                    else if (parameters.attacker is NPCWrapper npcWrapper)
                    {
                        NPC npc = npcWrapper.NPC;
                        npc.AddBuff(ModContent.BuffType<Mummy>(), AbilityData.mummyDurationNPC);
                    }
                },
                MessageOnHit = (parameters) =>
                {
                    if (parameters.attacker is WeaponWrapper ||
                        parameters.attacker is ProjectileWrapper ||
                        parameters.attacker is NPCWrapper)
                    {
                        return new MessageOnHitReturn("Mummy!", Element.ghost);
                    }

                    return MessageOnHitReturn.None;
                }
            });
            AddAbility(AbilityID.Corrosion, new Ability(AbilityID.Corrosion)
            {
                ModifyEffectivenessOutgoing = (parameters) =>
                {
                    if (parameters.outgoingType == Element.poison)
                    {
                        if (parameters.defendingType == Element.poison || parameters.defendingType == Element.steel)
                        {
                            if (parameters.normalEffectiveness < 1)
                            {
                                return 1;
                            }
                        }
                    }

                    return parameters.normalEffectiveness;
                },
                MessageHitEnemy = (parameters) => // todo: why isn't this working :(
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
            AddAbility(new Ability(AbilityID.ColorChange)
            {
                BuffOnHit = (parameters) =>
                {
                    int duration;
                    if (parameters.target is PlayerWrapper)
                    {
                        duration = AbilityData.colorChangeDurationPlayer;
                    }
                    else
                    {
                        duration = AbilityData.colorChangeDurationNPC;
                    }

                    int colorChangeBuffID = ModContent.BuffType<ColorChange>();
                    parameters.target.ModifiedElements = parameters.incoming;
                    parameters.target.AddBuff(colorChangeBuffID, duration);
                },
                MessageOnHit = (parameters) =>
                {
                    Element elementForColor = parameters.incoming.FirstOrDefault(); // todo: do something cooler
                    return new MessageOnHitReturn("Color Change!", elementForColor);
                }
            });
            AddAbility(AbilityID.MoldBreaker, new Ability(AbilityID.MoldBreaker)
            {
                ModifyOpponentsAbility = (defaultAbility) =>
                {
                    return AbilityID.None;
                }
            });
            AddAbility(AbilityID.SandForce, new Ability(AbilityID.SandForce)
            {
                PowerupType = (parameters) =>
                {
                    Element type = parameters.type;
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

                    if (appropriateType && parameters.user is ITarget target)
                    {
                        if (target.ZoneSandstorm)
                        {
                            return new PowerupTypeReturn(AbilityData.sandStormDamageBoost);
                        }
                    }
                    return new PowerupTypeReturn(1);
                }
            });
            AddAbility(AbilityID.Scrappy, new Ability(AbilityID.Scrappy)
            {
                ModifyEffectivenessOutgoing = (parameters) =>
                {
                    if (parameters.defendingType == Element.ghost)
                    {
                        Element attacking = parameters.outgoingType;
                        if (attacking == Element.normal || attacking == Element.fighting)
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
            AddAbility(AbilityID.Flammable, new Ability(AbilityID.Flammable)
            {
                ModifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;
                    for (int i = 0; i < parameters.incomingElements.Length; i++)
                    {
                        Element element = parameters.incomingElements[i];
                        if (element == Element.fire)
                        {
                            damage *= AbilityData.flammableDamageMultiplierFire;
                        }
                        else if (element == Element.water)
                        {
                            damage *= AbilityData.flammableDamageMultiplierWater;
                        }
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                }
            });
            AddAbility(AbilityID.Grounded, new Ability(AbilityID.Grounded)
            {
                ModifyEffectivenessIncoming = (parameters) =>
                {
                    if (parameters.defendingElement == Element.flying && parameters.incomingElement == Element.ground && parameters.normalEffectiveness < 1)
                    {
                        return 1;
                    }

                    return parameters.normalEffectiveness;
                }
            });
            AddAbility(AbilityID.PrismArmor, new Ability(AbilityID.PrismArmor)
            {
                ModifyDamageIncoming = (parameters) =>
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
            AddAbility(AbilityID.DD2Stab, new Ability(AbilityID.DD2Stab)
            {
                ModifyDamageIncoming = (parameters) =>
                {
                    float damage = parameters.damage;

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                ForceStabWithItem = (parameters) =>
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
                            itemMatchesArmor = (helm == ItemID.ApprenticeHat && chest == ItemID.ApprenticeRobe && legs == ItemID.ApprenticeTrousers) || (helm == ItemID.ApprenticeAltHead && chest == ItemID.ApprenticeAltShirt && legs == ItemID.ApprenticeAltPants);
                            break;

                        case ItemID.DD2ExplosiveTrapT1Popper:
                        case ItemID.DD2ExplosiveTrapT2Popper:
                        case ItemID.DD2ExplosiveTrapT3Popper:
                            itemMatchesArmor = (helm == ItemID.HuntressWig && chest == ItemID.HuntressJerkin && legs == ItemID.HuntressPants) || (helm == ItemID.HuntressAltHead && chest == ItemID.HuntressAltShirt && legs == ItemID.HuntressAltPants);
                            break;

                        case ItemID.DD2LightningAuraT1Popper:
                        case ItemID.DD2LightningAuraT2Popper:
                        case ItemID.DD2LightningAuraT3Popper:
                            itemMatchesArmor = (helm == ItemID.MonkBrows && chest == ItemID.MonkShirt && legs == ItemID.MonkPants) || (helm == ItemID.MonkAltHead && chest == ItemID.MonkAltShirt && legs == ItemID.MonkAltPants);
                            break;

                        case ItemID.DD2BallistraTowerT1Popper:
                        case ItemID.DD2BallistraTowerT2Popper:
                        case ItemID.DD2BallistraTowerT3Popper:
                            itemMatchesArmor = (helm == ItemID.SquireGreatHelm && chest == ItemID.SquirePlating && legs == ItemID.SquireGreaves) || (helm == ItemID.SquireAltHead && chest == ItemID.SquireAltShirt && legs == ItemID.SquireAltPants);
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

        private static void AddAbility(AbilityID none, Ability ability)
        {
            abilityLookupTable.Add(ability.ID, ability);
        }

        private static void AddAbility(Ability ability)
        {
            abilityLookupTable.Add(ability.ID, ability);
        }
    }
}
