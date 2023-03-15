using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.DataTypes.Structs;

namespace TerraTyping.TypeLoaders;

public class ProjectileTypeLoader : TypeLoader
{
    ProjectileTypeInfo[] typeInfos;
    ElementArray[] pewmaticHornElements;
    ElementArray[] deerclopsDebrisElements;

    protected override string CSVFileName => CSVFileNames.Projectiles;
    
    public static ProjectileTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Projectile projectile)
    {
        ProjectileTypeInfo projectileTypeInfo = Instance.typeInfos[projectile.type];
        if (projectileTypeInfo is null)
        {
            return ElementArray.Default;
        }
        else
        {
            if (projectileTypeInfo.specialType is not null)
            {
                return projectileTypeInfo.specialType.Invoke(projectile);
            }
            else
            {
                return projectileTypeInfo.elements;
            }
        }
    }

    /// <summary>
    /// Use the actual projectile whenever possible.
    /// </summary>
    /// <param name="projectileType">Use the actual projectile whenever possible.</param>
    public static ElementArray GetElements(int projectileType)
    {
        ProjectileTypeInfo projectileTypeInfo = Instance.typeInfos[projectileType];
        if (projectileTypeInfo is null)
        {
            return ElementArray.Default;
        }
        else
        {
            return projectileTypeInfo.elements;
        }
    }

    /// <summary>
    /// Modifies the effectiveness of an interaction. Used for Harpoons to do bonus damage against water types.
    /// </summary>
    public static void ModifyEffectiveness(ref float baseEffectiveness, Element offensiveElement, Element defensiveElement, Projectile projectile)
    {
        ProjectileTypeInfo projectileTypeInfo = Instance.typeInfos[projectile.type];
        if (projectileTypeInfo is not null)
        {
            projectileTypeInfo.modifyEffectiveness?.Invoke(ref baseEffectiveness, offensiveElement, defensiveElement);
        }
    }

    protected override void InitTypeInfoCollection()
    {
        typeInfos = new ProjectileTypeInfo[ProjectileLoader.ProjectileCount];
    }

    protected override void ParseLine(string line, string[] cells, int lineCount)
    {
        if (cells.Length <= ColumnToIndex.D)
        {
            return;
        }

        try
        {
            int projectile = int.Parse(cells[0]);


            string[] typeCells = cells[ColumnToIndex.B..ColumnToIndex.D];
            string modifyTypeMethodName = cells[ColumnToIndex.D];
            if (typeCells.All((str) => string.IsNullOrWhiteSpace(str)))
            {
                return;
            }

            ElementArray elementArray = ParseAtLeastOneElement(typeCells);

            typeInfos[projectile] = ProjectileTypeInfo.Get(elementArray,
                GetModifyTypeFunction(projectile),
                ModifyEffectivenessDelegate(projectile));
        }
        catch (ParsedNoneException) { }
    }

    private static ModifyEffectivenessDelegate ModifyEffectivenessDelegate(int projectileType)
    {
        switch (projectileType)
        {
            case ProjectileID.Harpoon:
                return (ref float baseEffectiveness, Element offensive, Element defensive) =>
                {
                    if (defensive == Element.water)
                    {
                        baseEffectiveness = Table.Multiplier;
                    }
                };
            default: return null;
        }
    }

    private static Func<Projectile, ElementArray> GetModifyTypeFunction(int projectileType)
    {
        switch (projectileType)
        {
            case ProjectileID.PewMaticHornShot:
                Instance.LoadPewmaticHorn();
                return PewmaticHornProj;

            case ProjectileID.DeerclopsRangedProjectile:
                Instance.LoadDeerclopsDebrisElements();
                return DeerclopsDebrisProj;

            case ProjectileID.FinalFractal:
                return ZenithProj;

            default: return null;
        }
    }

    private void LoadPewmaticHorn()
    {
        pewmaticHornElements = new ElementArray[]
        {
            ElementArray.Get(Element.rock),     //  0, stone block
            ElementArray.Get(Element.ground),   //  1, dirt block
            ElementArray.Get(Element.steel), //  2, copper ore
            ElementArray.Get(Element.bug), //  3, cobweb
            ElementArray.Get(Element.water), //  4, waterleaf
            ElementArray.Get(Element.bone), //  5, bone
            ElementArray.Get(Element.blood), //  6, rotten chunk
            ElementArray.Get(Element.flying), //  7, feather
            ElementArray.Get(Element.grass), //  8, jungle grass seeds
            ElementArray.Get(Element.grass), //  9, mushroom
            ElementArray.Get(Element.grass), // 10, daybloom
            ElementArray.Get(Element.dark), // 11, moonglow
            ElementArray.Get(Element.ground), // 12, blinkroot
            ElementArray.Get(Element.normal), // 13, wood platform
            ElementArray.Get(Element.ghost), // 14, deathweed
            ElementArray.Get(Element.fire), // 15, fireblossom
            ElementArray.Get(Element.ice), // 16, shiverthorn
            ElementArray.Get(Element.rock), // 17, amethyst
            ElementArray.Get(Element.rock), // 18, topaz
            ElementArray.Get(Element.rock), // 19, emerald
            ElementArray.Get(Element.rock), // 20, ruby
            ElementArray.Get(Element.rock), // 21, sapphire
            ElementArray.Get(Element.rock, Element.fairy), // 22, diamond
            ElementArray.Get(Element.rock), // 23, amber
        };
    }

    private void LoadDeerclopsDebrisElements()
    {
        deerclopsDebrisElements = new ElementArray[]
        {
            ElementArray.Get(Element.ground), // 0, dirt
            ElementArray.Get(Element.ground), // 1, dirt
            ElementArray.Get(Element.ground), // 2, dirt
            ElementArray.Get(Element.rock), // 3, stone
            ElementArray.Get(Element.rock), // 4, stone
            ElementArray.Get(Element.rock), // 5, stone
            ElementArray.Get(Element.ice), // 6, ice
            ElementArray.Get(Element.ice), // 7, ice
            ElementArray.Get(Element.ice), // 8, ice
            ElementArray.Get(Element.ice), // 9, ice
            ElementArray.Get(Element.ice), // 10, ice
            ElementArray.Get(Element.ice), // 11, ice
        };
    }

    private static ElementArray PewmaticHornProj(Projectile projectile)
    {
        if (Instance is not null && Instance.pewmaticHornElements is not null && projectile is not null)
        {
            int ai1 = (int)projectile.ai[1];
            if (ai1 >= 0 && ai1 < Instance.pewmaticHornElements.Length)
            {
                return Instance.pewmaticHornElements[ai1];
            }
        }

        return ElementArray.Default;
    }

    private static ElementArray DeerclopsDebrisProj(Projectile projectile)
    {
        if (Instance is not null && Instance.deerclopsDebrisElements is not null && projectile is not null)
        {
            int ai1 = (int)projectile.ai[1];
            if (ai1 >= 0 && ai1 < Instance.deerclopsDebrisElements.Length)
            {
                return Instance.deerclopsDebrisElements[ai1];
            }
        }

        return ElementArray.Default;
    }

    private static ElementArray ZenithProj(Projectile projectile)
    {
        if (projectile is not null)
        {
            return WeaponTypeLoader.GetElements((int)projectile.ai[1]);
        }
        else
        {
            return ElementArray.Default;
        }
    }

    public override void Load()
    {
        Instance = this;
    }

    public override void Unload()
    {
        Instance = null;
    }

    private class ProjectileTypeInfo
    {
        public readonly ElementArray elements;
        public readonly ModifyEffectivenessDelegate modifyEffectiveness;
        public readonly Func<Projectile, ElementArray> specialType;

        public static ProjectileTypeInfo Default => new ProjectileTypeInfo(ElementArray.Default, null, null); // change

        private ProjectileTypeInfo(ElementArray elements, ModifyEffectivenessDelegate modifyEffectiveness, Func<Projectile, ElementArray> specialType)
        {
            this.elements = elements;
            this.modifyEffectiveness = modifyEffectiveness;
            this.specialType = specialType;
        }

        public static ProjectileTypeInfo Get(ElementArray elements)
        {
            return new ProjectileTypeInfo(elements, null, null);
        }

        public static ProjectileTypeInfo Get(ElementArray elements, ModifyEffectivenessDelegate modifyEffectiveness)
        {
            return new ProjectileTypeInfo(elements, modifyEffectiveness, null);
        }

        public static ProjectileTypeInfo Get(ElementArray fallbackElements, Func<Projectile, ElementArray> specialType)
        {
            return new ProjectileTypeInfo(fallbackElements, null, specialType);
        }

        public static ProjectileTypeInfo Get(ElementArray fallbackElements, Func<Projectile, ElementArray> specialType, ModifyEffectivenessDelegate modifyEffectiveness)
        {
            return new ProjectileTypeInfo(fallbackElements, modifyEffectiveness, specialType);
        }
    }
}
