using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
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
            public Element incoming;
            public float damage;
            public ITarget targetInterface;
            public float knockback;
            public IDamageClass damageClass;

            public ModifyDamageParameters(Element incoming, float damage, ITarget targetInterface, float knockback, IDamageClass damageClass)
            {
                this.incoming = incoming;
                this.damage = damage;
                this.targetInterface = targetInterface;
                this.knockback = knockback;
                this.damageClass = damageClass;
            }
        }

        public delegate bool ForceStab(Element type);
        public static ForceStab ForceStabDefault => (type) =>
        {
            return false;
        };

        public delegate Element ModifyAttackType(Element def);
        public static ModifyAttackType ModifyAttackTypeDefault => (def) =>
        {
            return def;
        };

        public delegate float ModifyEffectivenessIncoming(ModifyEffectivenessIncomingParameters parameters);
        public static ModifyEffectivenessIncoming ModifyEffectivenessIncomingDefault => (parameters) =>
        {
            return parameters.damage;
        };
        public struct ModifyEffectivenessIncomingParameters
        {
            public float damage;
            public Element incoming;
            public ITarget targetInterface;
            public IDamageClass damageClass;

            public ModifyEffectivenessIncomingParameters(float effectiveness, Element type, ITarget targetInterface, IDamageClass damageClass)
            {
                this.damage = effectiveness;
                this.incoming = type;
                this.targetInterface = targetInterface;
                this.damageClass = damageClass;
            }
        }

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
            public Element incoming;
            public ITarget target;
            public Wrapper attacker;

            public BuffOnHitParameters(Element incoming, ITarget target, Wrapper attacker)
            {
                this.incoming = incoming;
                this.target = target;
                this.attacker = attacker;
            }
        }

        public delegate MessageOnHitReturn MessageOnHit(MessageOnHitParameters messageOnHitParameters);
        public static MessageOnHit MessageOnHitDefault => (parameters) => { return default; };
        public struct MessageOnHitParameters
        {
            public ITarget target;
            public Element incoming;
            public IDamageClass damageClass;
            public Wrapper attacker;

            public MessageOnHitParameters(Element incoming, ITarget target, IDamageClass damageClass, Wrapper attacker)
            {
                this.target = target;
                this.incoming = incoming;
                this.damageClass = damageClass;
                this.attacker = attacker;
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
            public Element outgoing;
            public ITarget target;
            public IPrimaryType primaryType;
            public ISecondaryType secondaryType;

            public MessageHitEnemyParameters(Element outgoing, ITarget target, IPrimaryType primaryType, ISecondaryType secondaryType)
            {
                this.outgoing = outgoing;
                this.target = target;
                this.primaryType = primaryType;
                this.secondaryType = secondaryType;
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
                Tuple<int, int, int> tuple = Colors.Type[type];
                textColor = new Color(tuple.Item1, tuple.Item2, tuple.Item3);
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
                if (parameters.incoming == typeToAbsorb)
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
                if (buffOnHitData.addBuff && typesThatBuff.Contains(parameters.incoming))
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
                if (types.Contains(parameters.incoming))
                {
                    return new MessageOnHitReturn(text, parameters.incoming);
                }
                return MessageOnHitReturn.None;
            };
        }
        static MessageOnHit MessageOnHitFactory(Element type, string text) => MessageOnHitFactory(new Element[] { type }, text);
        #endregion

        public static Ability GetAbility(AbilityID ability)
        {
            if (abilityLookupTable.ContainsKey(ability))
            {
                return abilityLookupTable[ability];
            }
            else
            {
                return new Ability(AbilityID.None);
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
                return new Ability(AbilityID.None);
            }
        }

        static readonly Dictionary<AbilityID, Ability> abilityLookupTable = new Dictionary<AbilityID, Ability>() { };
        static void InitializeAbilityLookupTable()
        {
            abilityLookupTable.Add(AbilityID.None, new Ability(AbilityID.None));
            abilityLookupTable.Add(AbilityID.Levitate, new Ability(AbilityID.Levitate)
            {
                ModifyDamage = AbsorbFactory(Element.ground, false)
            });
            abilityLookupTable.Add(AbilityID.VoltAbsorb, new Ability(AbilityID.VoltAbsorb)
            {
                ModifyDamage = AbsorbFactory(Element.electric, true),
                MessageOnHit = MessageOnHitFactory(Element.electric, "Volt Absorb!")
            });
            abilityLookupTable.Add(AbilityID.LightningRod, new Ability(AbilityID.LightningRod)
            {
                ModifyDamage = AbsorbFactory(Element.electric, false),
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
                            parameters.projectile.Offensive == Element.electric)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            });
            abilityLookupTable.Add(AbilityID.StormDrain, new Ability(AbilityID.StormDrain)
            {
                ModifyDamage = AbsorbFactory(Element.water, false),
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
                            parameters.projectile.Offensive == Element.water)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            });
            abilityLookupTable.Add(AbilityID.MotorDrive, new Ability(AbilityID.MotorDrive)
            { 
                ModifyDamage = AbsorbFactory(Element.electric, false),
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<MotorDrive>(), 
                AbilityData.motorDriveDurationPlayer,
                AbilityData.motorDriveDurationNPC, false), Element.electric),
                MessageOnHit = MessageOnHitFactory(Element.electric, "Motor Drive!")
            });
            abilityLookupTable.Add(AbilityID.WaterAbsorb, new Ability(AbilityID.WaterAbsorb)
            {
                ModifyDamage = AbsorbFactory(Element.water, true),
                MessageOnHit = MessageOnHitFactory(Element.water, "Water Absorb!")
            });
            abilityLookupTable.Add(AbilityID.FlashFire, new Ability(AbilityID.FlashFire)
            {
                ModifyDamage = AbsorbFactory(Element.fire, false),
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<FlashFire>(), 
                AbilityData.flashFireDurationPlayer,
                AbilityData.flashFireDurationNPC, false), Element.fire),
                MessageOnHit = MessageOnHitFactory(Element.fire, "Flash Fire!")
            });
            abilityLookupTable.Add(AbilityID.SapSipper, new Ability(AbilityID.SapSipper)
            {
                ModifyDamage = AbsorbFactory(Element.grass, false),
                MessageOnHit = MessageOnHitFactory(Element.grass, "Sap Sipper!")
            });
            abilityLookupTable.Add(AbilityID.ThickFat, new Ability(AbilityID.ThickFat)
            {
                ModifyDamage = (parameters) =>
                {
                    float damage = parameters.damage;
                    Element type = parameters.incoming;
                    ITarget targetInterface = parameters.targetInterface;
                    float knockback = parameters.knockback;
                    switch (type)
                    {
                        case Element.ice:
                        case Element.fire:
                            Tuple<int, int, int> tuple = Colors.Type[type];
                            Color color = new Color(tuple.Item1, tuple.Item2, tuple.Item3);
                            CombatTextInfo combatTextInfo = new CombatTextInfo(targetInterface.GetRect(), color, "Thick Fat!", false, true);

                            return new ModifyDamageReturn(damage * AbilityData.thickFatFireIceDamageTaken, false, knockback);
                        default:
                            return new ModifyDamageReturn(damage, false, knockback);
                    }
                },
                MessageOnHit = (parameters) =>
                {
                    Element element = parameters.incoming;
                    if (element == Element.ice || element == Element.fire)
                    {
                        return new MessageOnHitReturn("Thick Fat!", element);
                    }
                    return MessageOnHitReturn.None;
                }
            });
            abilityLookupTable.Add(AbilityID.Heatproof, new Ability(AbilityID.Heatproof)
            {
                ModifyDamage = (parameters) =>
                {
                    if (parameters.incoming == Element.fire)
                    {
                        return new ModifyDamageReturn(parameters.damage * AbilityData.heatproofFireDamageTaken, false, parameters.knockback);
                    }
                    return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                },
                MessageOnHit = MessageOnHitFactory(Element.fire, "Heatproof!")
            });
            abilityLookupTable.Add(AbilityID.WaterBubble, new Ability(AbilityID.WaterBubble)
            {
                ModifyDamage = (parameters) =>
                {
                    if (parameters.incoming == Element.fire)
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
            abilityLookupTable.Add(AbilityID.Fluffy, new Ability(AbilityID.Fluffy)
            {
                ModifyDamage = (parameters) =>
                {
                    float damage = parameters.damage;
                    if (parameters.damageClass.Melee)
                    {
                        damage *= AbilityData.fluffyMeleeDamageTaken;
                    }

                    if (parameters.incoming == Element.fire)
                    {
                        damage *= AbilityData.fluffyFireDamageTaken;
                    }

                    if (parameters.damageClass.Melee || parameters.incoming == Element.fire)
                    {
                        return new ModifyDamageReturn(damage, false, parameters.knockback);
                    }

                    return new ModifyDamageReturn(damage, false, parameters.knockback);
                },
                MessageOnHit = (parameters) =>
                {
                    if (parameters.incoming == Element.fire)
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
            abilityLookupTable.Add(AbilityID.Justified, new Ability(AbilityID.Justified)
            {
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<Justified>(), 
                AbilityData.justifiedDurationPlayer,
                AbilityData.justifiedDurationNPC, false), Element.dark),
                MessageOnHit = MessageOnHitFactory(Element.dark, "Justified!")
            });
            abilityLookupTable.Add(AbilityID.WaterCompaction, new Ability(AbilityID.WaterCompaction)
            {
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<WaterCompaction>(), 
                AbilityData.waterCompactionDurationPlayer,
                AbilityData.waterCompactionDurationNPC, false), Element.water),
                MessageOnHit = MessageOnHitFactory(Element.water, "Water Compaction!")
            });
            abilityLookupTable.Add(AbilityID.SteamEngine, new Ability(AbilityID.SteamEngine)
            {
                BuffOnHit = BuffOnHitFactory(new BuffOnHitData(ModContent.BuffType<SteamEngine>(),
                AbilityData.steamEngineDurationPlayer,
                AbilityData.steamEngineDurationNPC, false), new Element[] { Element.water, Element.fire }),
                MessageOnHit = (parameters) =>
                {
                    if (parameters.incoming == Element.water || parameters.incoming == Element.fire)
                    {
                        return new MessageOnHitReturn("Steam Engine!", parameters.incoming);
                    }
                    else
                    {
                        return MessageOnHitReturn.None;
                    }
                }
            });
            abilityLookupTable.Add(AbilityID.DrySkin, new Ability(AbilityID.DrySkin)
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
                ModifyDamage = (parameters) =>
                {
                    switch (parameters.incoming)
                    {
                        case Element.fire:
                            return new ModifyDamageReturn(parameters.damage * 1.25f, false, parameters.knockback);
                        case Element.water:
                            return new ModifyDamageReturn(0, true, parameters.knockback);
                        default:
                            return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                    }
                },
                MessageOnHit = MessageOnHitFactory(new Element[] { Element.fire, Element.water }, "Dry Skin!")
            });
            abilityLookupTable.Add(AbilityID.Mummy, new Ability(AbilityID.Mummy)
            {
                BuffOnHit = (parameters) =>
                {
                    if (parameters.attacker is ItemWrapper itemWrapper)
                    {
                        Main.player[itemWrapper.Player].AddBuff(ModContent.BuffType<Mummy>(), AbilityData.mummyDurationPlayer);
                    }
                    else if (parameters.attacker is ProjectileWrapper projWrapper)
                    {
                        if (projWrapper.OwnerType == Owner.Player)
                        {
                            Main.player[projWrapper.GetProjectile().owner].AddBuff(ModContent.BuffType<Mummy>(), AbilityData.mummyDurationPlayer);
                        }
                    }
                    else if (parameters.attacker is NPCWrapper npcWrapper)
                    {
                        npcWrapper.GetNPC().AddBuff(ModContent.BuffType<Mummy>(), AbilityData.mummyDurationNPC);
                    }
                },
                MessageOnHit = (parameters) =>
                {
                    if (parameters.attacker is ItemWrapper ||
                        parameters.attacker is ProjectileWrapper || 
                        parameters.attacker is NPCWrapper)
                    {
                        return new MessageOnHitReturn("Mummy!", Element.ghost);
                    }

                    return MessageOnHitReturn.None;
                }
            });
            abilityLookupTable.Add(AbilityID.Corrosion, new Ability(AbilityID.Corrosion)
            {
                ModifyEffectivenessOutgoing = (parameters) =>
                {
                    if (parameters.outgoingType == Element.poison)
                    {
                        if (parameters.defendingType == Element.poison || parameters.defendingType == Element.steel)
                        {
                            return 1;
                        }
                    }

                    return parameters.normalEffectiveness;
                },
                MessageHitEnemy = (parameters) => // why isn't this working :(
                {
                    if (parameters.outgoing == Element.poison)
                    {
                        if (parameters.primaryType.Primary == Element.poison || parameters.primaryType.Primary == Element.steel ||
                            parameters.secondaryType.Secondary == Element.poison || parameters.secondaryType.Secondary == Element.steel)
                        {
                            Main.NewText("Corrosion!");
                            return new MessageHitEnemyReturn(new Message("Corrosion!", Element.poison));
                        }
                    }
                    return new MessageHitEnemyReturn(Message.None);
                }
            });
            abilityLookupTable.Add(AbilityID.ColorChange, new Ability(AbilityID.ColorChange)
            {
                BuffOnHit = (parameters) =>
                {
                    parameters.target.ModifyType = parameters.incoming;

                    Element[] elements = (Element[])Enum.GetValues(typeof(Element));
                    for (int i = 0; i < elements.Length; i++)
                    {
                        ModBuff addTypeBuff = BuffUtils.ModBuffAddType(elements[i]);
                        ModBuff replaceTypeBuff = BuffUtils.ModBuffReplaceType(elements[i]);
                        if (addTypeBuff != null && parameters.target.HasBuff(addTypeBuff.Type))
                        {
                            parameters.target.RemoveBuff(addTypeBuff.Type);
                        }
                        if (replaceTypeBuff != null && parameters.target.HasBuff(replaceTypeBuff.Type))
                        {
                            parameters.target.RemoveBuff(replaceTypeBuff.Type);
                        }
                    }

                    if (parameters.target is PlayerWrapper)
                    {
                        ModBuff modBuff = BuffUtils.ModBuffReplaceType(parameters.incoming);
                        if (modBuff != null)
                        {
                            parameters.target.AddBuff(modBuff.Type, AbilityData.colorChangeDurationPlayer);
                        }
                    }
                    else
                    {
                        ModBuff modBuff = BuffUtils.ModBuffReplaceType(parameters.incoming);
                        if (modBuff != null)
                        {
                            parameters.target.AddBuff(modBuff.Type, AbilityData.colorChangeDurationNPC);
                        }
                    }
                },
                MessageOnHit = (parameters) =>
                {
                    return new MessageOnHitReturn("Color Change!", parameters.incoming);
                }
            });
            abilityLookupTable.Add(AbilityID.MoldBreaker, new Ability(AbilityID.MoldBreaker)
            {
                ModifyOpponentsAbility = (defaultAbility) =>
                {
                    return AbilityID.None;
                }
            });
            abilityLookupTable.Add(AbilityID.SandForce, new Ability(AbilityID.SandForce)
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
            abilityLookupTable.Add(AbilityID.Scrappy, new Ability(AbilityID.Scrappy)
            {
                ModifyEffectivenessOutgoing = (parameters) =>
                {
                    if (parameters.defendingType == Element.ghost)
                    {
                        Element attacking = parameters.outgoingType;
                        if (attacking == Element.normal || attacking == Element.fighting)
                        {
                            return 1f;
                        }
                    }
                    return parameters.normalEffectiveness;
                }
            });
            abilityLookupTable.Add(AbilityID.Flammable, new Ability(AbilityID.Flammable)
            {
                ModifyDamage = (parameters) =>
                {
                    switch (parameters.incoming)
                    {
                        case Element.fire:
                            return new ModifyDamageReturn(parameters.damage * AbilityData.flammableDamageMultiplierFire, false, parameters.knockback);
                        case Element.water:
                            return new ModifyDamageReturn(parameters.damage * AbilityData.flammableDamageMultiplierWater, false, parameters.knockback);
                    }
                    return new ModifyDamageReturn(parameters.damage, false, parameters.knockback);
                }
            });
        }
    }
}
