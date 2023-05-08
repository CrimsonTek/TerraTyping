using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTyping.Helpers;

namespace TerraTyping.Commands;

public class PrintCSVCommand : ModCommand
{
    /// <summary>
    /// The index is the projectile that the items in the list shoot.
    /// </summary>
    private static List<string>[] projectilesShotByItemsDisplayName;
    /// <summary>
    /// The index is the projectile that the items in the list shoot.
    /// </summary>
    private static List<string>[] projectilesShotByItemsInternalName;
    private static ProjectileDoesDamage[] _projectileDoesDamage;

    /// <summary>
    /// The index is the projectile that the items in the list shoot.
    /// </summary>
    private static Dictionary<int, List<string>> modProjectilesShotByItemsDisplayName;
    /// <summary>
    /// The index is the projectile that the items in the list shoot.
    /// </summary>
    private static Dictionary<int, List<string>> modProjectilesShotByItemsInternalName;
    private static Dictionary<int, ProjectileDoesDamage> modProjectileDoesDamage;

    public override string Command => "Print";

    public override CommandType Type => CommandType.Chat;

    public override string Usage => $"/{Command} ({string.Join('/', Enum.GetNames<PrintType>())}) [ModName (omit for vanilla)]";

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length < 1)
        {
            caller.Reply(Usage, Main.errorColor);
            return;
        }

        Mod mod = null;
        bool useMod = false;

        if (args.Length == 2)
        {
            bool badArg1 = !TryGetMod(args[1], out mod, caller);

            if (args[1].Equals("example", StringComparison.OrdinalIgnoreCase))
            {
                useMod = false;
                badArg1 = false;
            }
            else
            {
                useMod = true;
            }

            if (badArg1)
            {
                caller.Reply($"Could not find mod named \"{args[1]}\".");
                return;
            }
        }

        if (!Enum.TryParse(args[0], ignoreCase: true, out PrintType printType))
        {
            caller.Reply(Usage, Main.errorColor);
            return;
        }

        if (useMod)
        {
            PrintMod(caller, mod, printType);
        }
        else
        {
            PrintVanilla(caller, printType);
        }
    }

    private static void PrintVanilla(CommandCaller caller, PrintType printType)
    {
        switch (printType)
        {
            case PrintType.NPC:
                PrintAllVanilla(caller,
                    NPCID.NegativeIDCount + 1,
                    NPCID.Count,
                    typeof(NPCID),
                    "npcTypesOverride",
                    (NPC n) => n.TypeName,
                    (NPC n, short type) => n.SetDefaults(type),
                    (NPC npc) => true);
                break;

            case PrintType.Projectiles:
                ProjectilesSetup();
                PrintAllVanilla<Projectile>(caller,
                    1,
                    ProjectileID.Count,
                    typeof(ProjectileID),
                    "projectileTypesOverride",
                    (proj) => proj.Name,
                    (proj, type) => proj.SetDefaults(type),
                    (proj) => true,
                    ExtraProjectileColumns);
                break;

            case PrintType.Weapons:
                PrintAllVanilla<Item>(caller,
                    1,
                    ItemID.Count,
                    typeof(ItemID),
                    "weaponTypesOverride",
                    (item) => item.Name,
                    (item, type) => item.SetDefaults(type),
                    (item) => item.OriginalDamage > 0 && item.ammo == 0,
                    ExtraWeaponColumns);
                break;

            case PrintType.Ammo:
                PrintAllVanilla<Item>(caller,
                    1,
                    ItemID.Count,
                    typeof(ItemID),
                    "ammoTypesOverride",
                    (item) => item.Name,
                    (item, type) => item.SetDefaults(type),
                    (item) => item.OriginalDamage > 0 && item.ammo != 0);
                break;

            case PrintType.Armor:
                PrintArmorVanity(caller, true, false);
                break;

            case PrintType.Vanity:
                PrintArmorVanity(caller, false, true);
                break;

            case PrintType.Wearable:
                PrintArmorVanity(caller, true, true);
                break;

            case PrintType.AllItems:
                PrintAllVanilla<Item>(caller,
                    1,
                    ItemID.Count,
                    typeof(ItemID),
                    "allItemsOverride",
                    (item) => item.Name,
                    (item, type) => item.SetDefaults(type),
                    (item) => true,
                    (item) => $"{item.ammo},{item.OriginalDamage}",
                    "-ammo,-damage");
                caller.Reply("Don't forget to rename this appropriately.");
                break;
        }
    }

    private static void PrintMod(CommandCaller caller, Mod mod, PrintType printType)
    {
        switch (printType)
        {
            case PrintType.NPC:
                PrintAllFromMod<ModNPC>(mod, caller, $"npcTypes{mod.Name}", modNPC => modNPC.DisplayName, null, null);
                break;

            case PrintType.Projectiles:
                ProjectilesSetupForMods(mod);
                PrintAllFromMod<ModProjectile>(mod, caller, $"projectileTypes{mod.Name}", modProj => modProj.DisplayName, null, ExtraProjectileColumnsForMods, ExtraProjectileHeadersForMods);
                break;

            case PrintType.Weapons:
                PrintAllFromMod<ModItem>(mod, caller, $"weaponTypes{mod.Name}", (modItem) => modItem.DisplayName, (modItem) => modItem.Item.OriginalDamage > 0 && modItem.Item.ammo == 0, (mod, modItem) => ExtraWeaponColumns(modItem.Item), "");
                break;

            case PrintType.Ammo:
                PrintAllFromMod<ModItem>(mod, caller, $"ammoTypes{mod.Name}", (modItem) => modItem.DisplayName, (modItem) => modItem.Item.OriginalDamage > 0 && modItem.Item.ammo != 0);
                break;

            case PrintType.Armor:
                PrintArmorFromMod(mod, caller, true, false);
                break;

            case PrintType.Vanity:
                PrintArmorFromMod(mod, caller, false, true);
                break;

            case PrintType.Wearable:
                PrintArmorFromMod(mod, caller, true, true);
                break;

            case PrintType.AllItems:
                PrintAllFromMod<ModItem>(mod, caller, $"allItems{mod.Name}", (modItem) => modItem.DisplayName, (modItem) => true, (mod, modItem) => ExtraAllItemsColumns(modItem.Item));
                caller.Reply("Don't forget to rename this appropriately.");
                break;
        }
    }

    static string ExtraWeaponColumns(Item item)
    {
        StringBuilder toolType = new StringBuilder();

        bool foundToolType = false;
        if (item.pick > 0)
        {
            toolType.Append("Pickaxe");
            foundToolType = true;
        }

        if (item.axe > 0)
        {
            if (foundToolType)
            {
                toolType.Append('/');
            }

            toolType.Append("Axe");
        }

        if (item.hammer > 0)
        {
            if (foundToolType)
            {
                toolType.Append('/');
            }

            toolType.Append("Hammer");
        }


        return $",{toolType},{item.OriginalRarity},{item.DamageType},{item.OriginalDamage},{item.value}";
    }

    static string ExtraProjectileColumns(Projectile projectile)
    {
        List<string> itemsThatShootThisProjectileListDisplayName = projectilesShotByItemsDisplayName[projectile.type];
        List<string> itemsThatShootThisProjectileListInternalName = projectilesShotByItemsInternalName[projectile.type];

        string itemsThatShootThisProjectileStringDisplayName = "";
        string itemsThatShootThisProjectileStringInternalName = "";
        if (itemsThatShootThisProjectileListDisplayName is not null)
        {
            itemsThatShootThisProjectileStringDisplayName = string.Join(", ", itemsThatShootThisProjectileListDisplayName);
            itemsThatShootThisProjectileStringInternalName = string.Join(", ", itemsThatShootThisProjectileListInternalName);
        }

        List<string> projTypes = new List<string>();
        AddIf(projTypes, "Arrow", projectile.arrow);
        AddIf(projTypes, "Bobber", projectile.bobber);
        AddIf(projTypes, "Counterweight", projectile.counterweight);
        AddIf(projTypes, "Minion", projectile.minion);
        AddIf(projTypes, "Sentry", projectile.sentry);
        AddIf(projTypes, "Trap", projectile.trap);
        AddIf(projTypes, "WipableTurret", projectile.WipableTurret);

        string faction;
        switch ((projectile.friendly, projectile.hostile))
        {
            case (true, true):
                faction = "both";
                break;
            case (true, false):
                faction = "friendly";
                break;
            case (false, true):
                faction = "hostile";
                break;
            case (false, false):
                faction = "neither";
                break;
        }

        return $"\"{itemsThatShootThisProjectileStringDisplayName}\",\"{itemsThatShootThisProjectileStringInternalName}\",{faction},{string.Join(", ", projTypes)},{_projectileDoesDamage[projectile.type]}";
    }

    static string ExtraProjectileColumnsForMods(Mod mod, ModProjectile modProjectile)
    {
        Projectile projectile = modProjectile.Projectile;

        List<string> itemsThatShootThisProjectileListDisplayName = modProjectilesShotByItemsDisplayName.GetValueOrDefault(projectile.type);
        List<string> itemsThatShootThisProjectileListInternalName = modProjectilesShotByItemsInternalName.GetValueOrDefault(projectile.type);

        string itemsThatShootThisProjectileStringDisplayName = "";
        string itemsThatShootThisProjectileStringInternalName = "";
        if (itemsThatShootThisProjectileListDisplayName is not null)
        {
            itemsThatShootThisProjectileStringDisplayName = string.Join(", ", itemsThatShootThisProjectileListDisplayName);
            itemsThatShootThisProjectileStringInternalName = string.Join(", ", itemsThatShootThisProjectileListInternalName);
        }

        List<string> projTypes = new List<string>();
        AddIf(projTypes, "Arrow", projectile.arrow);
        AddIf(projTypes, "Bobber", projectile.bobber);
        AddIf(projTypes, "Counterweight", projectile.counterweight);
        AddIf(projTypes, "Minion", projectile.minion);
        AddIf(projTypes, "Sentry", projectile.sentry);
        AddIf(projTypes, "Trap", projectile.trap);
        AddIf(projTypes, "WipableTurret", projectile.WipableTurret);

        string faction;
        switch ((projectile.friendly, projectile.hostile))
        {
            case (true, true):
                faction = "both";
                break;
            case (true, false):
                faction = "friendly";
                break;
            case (false, true):
                faction = "hostile";
                break;
            case (false, false):
                faction = "neither";
                break;
        }

        ProjectileDoesDamage pdd = modProjectileDoesDamage.TryGetValue(projectile.type, out ProjectileDoesDamage value) ? value : ProjectileDoesDamage.NoItem;

        return $"\"{itemsThatShootThisProjectileStringDisplayName}\",\"{itemsThatShootThisProjectileStringInternalName}\",{faction},\"{string.Join(", ", projTypes)}\",{pdd}";
    }

    const string ExtraProjectileHeadersForMods = "-itemsThatShootThis,-itemsThatShootThisDisplayNames,-faction,-projectileType,-doesDamage";

    static void ProjectilesSetup()
    {
        Dictionary<int, string> internalNames = new Dictionary<int, string>();
        FieldInfo[] fieldInfos = typeof(ItemID).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.IsLiteral && fieldInfo.FieldType == typeof(short))
            {
                internalNames.Add((short)fieldInfo.GetRawConstantValue(), fieldInfo.Name);
            }
        }

        projectilesShotByItemsDisplayName = new List<string>[ProjectileID.Count];
        projectilesShotByItemsInternalName = new List<string>[ProjectileID.Count];
        _projectileDoesDamage = new ProjectileDoesDamage[ProjectileID.Count];
        Item item = new Item();
        for (int i = 0; i < ItemID.Count; i++)
        {
            item.SetDefaults(i);
            int projType = item.shoot;
            bool itemDoesDamage = item.damage > 0;

            if (projectilesShotByItemsDisplayName[projType] is null)
            {
                projectilesShotByItemsDisplayName[projType] = new List<string>();
                projectilesShotByItemsInternalName[projType] = new List<string>();
            }
            projectilesShotByItemsDisplayName[projType].Add(item.Name);
            projectilesShotByItemsInternalName[projType].Add(internalNames[item.type]);

            switch (_projectileDoesDamage[projType])
            {
                case ProjectileDoesDamage.NoItem:
                    _projectileDoesDamage[projType] = itemDoesDamage ? ProjectileDoesDamage.Damage : ProjectileDoesDamage.NoDamage;
                    break;
                case ProjectileDoesDamage.Damage when !itemDoesDamage:
                    _projectileDoesDamage[projType] = ProjectileDoesDamage.SometimesDamage;
                    break;
                case ProjectileDoesDamage.NoDamage when itemDoesDamage:
                    _projectileDoesDamage[projType] = ProjectileDoesDamage.SometimesDamage;
                    break;
            }
        }
    }

    static void ProjectilesSetupForMods(Mod mod)
    {
        Dictionary<int, string> internalNames = new Dictionary<int, string>();
        FieldInfo[] fieldInfos = typeof(ItemID).GetFields(BindingFlags.Public | BindingFlags.Static);
        int debugNumber = 0;
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.IsLiteral && fieldInfo.FieldType == typeof(short))
            {
                internalNames.Add((short)fieldInfo.GetRawConstantValue(), fieldInfo.Name);
            }
            debugNumber++;
        }

        modProjectilesShotByItemsDisplayName = new Dictionary<int, List<string>>();
        modProjectilesShotByItemsInternalName = new Dictionary<int, List<string>>();
        modProjectileDoesDamage = new Dictionary<int, ProjectileDoesDamage>();

        for (int i = 0; i < ItemLoader.ItemCount; i++)
        {
            Item item = new Item(i);
            int shoot = item.shoot;
            if (shoot < ProjectileID.Count)
            {
                continue;
            }

            if (!modProjectilesShotByItemsDisplayName.ContainsKey(shoot))
            {
                modProjectilesShotByItemsDisplayName[shoot] = new List<string>();
                modProjectilesShotByItemsInternalName[shoot] = new List<string>();
            }

            string internalName = (item.type >= ItemID.Count) ? item.ModItem.Name : internalNames[item.type];

            modProjectilesShotByItemsDisplayName[shoot].Add(item.Name);
            modProjectilesShotByItemsInternalName[shoot].Add(internalName);

            if (!modProjectileDoesDamage.ContainsKey(shoot))
            {
                modProjectileDoesDamage.Add(shoot, default);
            }

            bool itemDoesDamage = item.damage > 0;
            switch (modProjectileDoesDamage[shoot])
            {
                case ProjectileDoesDamage.NoItem:
                    modProjectileDoesDamage[shoot] = itemDoesDamage ? ProjectileDoesDamage.Damage : ProjectileDoesDamage.NoDamage;
                    break;
                case ProjectileDoesDamage.Damage when !itemDoesDamage:
                    modProjectileDoesDamage[shoot] = ProjectileDoesDamage.SometimesDamage;
                    break;
                case ProjectileDoesDamage.NoDamage when itemDoesDamage:
                    modProjectileDoesDamage[shoot] = ProjectileDoesDamage.SometimesDamage;
                    break;
            }
        }
    }

    static string ExtraAllItemsColumns(Item item)
    {
        return $"{item.ammo},{item.OriginalDamage}";
    }

    private static void PrintAllVanilla<T>(CommandCaller caller,
        short startValIncl,
        short endValExcl,
        Type idClassType,
        string fileName,
        Func<T, string> getDisplayName,
        Action<T, short> setDefaults,
        Func<T, bool> select,
        Func<T, string> extraColumns = null,
        string extraColumnsHeader = "")
    where T : new()
    {
        Dictionary<short, string> internalNames = new Dictionary<short, string>();
        FieldInfo[] fieldInfos = idClassType.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.IsLiteral && fieldInfo.FieldType == typeof(short))
            {
                internalNames.Add((short)fieldInfo.GetRawConstantValue(), fieldInfo.Name);
            }
        }

        string dir = Directory.GetCurrentDirectory();
        using FileStream fileStream = SafeFileCreate($"{dir}\\TerraTypingBlankCSVs", fileName, "csv", out string path);
        using StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.WriteLine($"id,-internalName,-displayName,{extraColumnsHeader}");
        streamWriter.Flush();

        T tObj = new T();
        for (short i = startValIncl; i < endValExcl; i++)
        {
            setDefaults(tObj, i);
            if (select(tObj))
            {
                if (internalNames.TryGetValue(i, out string internalName))
                {
                    string extra = extraColumns?.Invoke(tObj) ?? string.Empty;
                    streamWriter.WriteLine($"{i},{internalName},{getDisplayName(tObj)},{extra}");
                    streamWriter.Flush();
                    //caller.Reply($"{idClassName}.{internalName} {{{debugNumber}}}: \"{getCasualName(obj)}\"");
                }
                else
                {
                    caller.Reply($"{i} could not be found");
                    break;
                }
            }
        }
        caller.Reply($"Created file \"{path}\"");
    }

    private static void PrintAllFromMod<T>(
        Mod mod,
        CommandCaller caller,
        string fileName,
        Func<T, ModTranslation> getDisplayName,
        Func<T, bool> select = null,
        Func<Mod, T, string> extraColumns = null,
        string extraColumnsHeader = "")
    where T : ModType
    {
        string dir = Directory.GetCurrentDirectory();
        using FileStream fileStream = SafeFileCreate($"{dir}\\TerraTypingBlankCSVs", $"{fileName}", "csv", out string path);
        using StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.WriteLine($"internalName,-displayName,{extraColumnsHeader}");

        foreach (T tObj in mod.GetContent<T>())
        {
            if (select is null || select(tObj))
            {
                string internalName = tObj.Name;
                string hashedName = Encode(internalName);

                string extra = extraColumns?.Invoke(mod, tObj) ?? string.Empty;
                streamWriter.WriteLine($"{internalName},{getDisplayName(tObj).GetDefault()},{extra}");
                streamWriter.Flush();
            }
        }

        caller.Reply($"Created file \"{path}\"");
    }

    private static void PrintArmorVanity(CommandCaller caller, bool armor, bool vanity)
    {
        if (!armor && !vanity)
        {
            return;
        }
        PrintAllVanilla<Item>(caller, 1, ItemID.Count, typeof(ItemID),
            "armorTypesOverride",
            (item) => item.Name,
            (item, i) => item.SetDefaults(i),
            (item) => (item.legSlot is -1 || item.bodySlot is -1 || item.headSlot is -1) && (item.OriginalDefense > 0 || item.type == ItemID.WoodGreaves ? armor : vanity),
            (item) => armor && vanity ? $"{(item.OriginalDefense > 0 ? "armor" : "vanity")}" : "",
            "type");
    }

    private static void PrintArmorFromMod(Mod mod, CommandCaller caller, bool armor, bool vanity)
    {
        if (!armor && !vanity)
        {
            return;
        }

        PrintAllFromMod<ModItem>(mod, caller,
            $"armorTypes{mod.Name}",
            (modItem) => modItem.DisplayName,
            (modItem) => (modItem.Item.legSlot is -1 || modItem.Item.bodySlot is -1 || modItem.Item.headSlot is -1) && (modItem.Item.OriginalDefense > 0 ? armor : vanity),
            (mod, modItem) => armor && vanity ? $"{(modItem.Item.OriginalDefense > 0 ? "armor" : "vanity")}" : "",
            "type");
    }

    private static void AddIf<T>(List<T> list, T value, bool condition)
    {
        if (condition)
        {
            list.Add(value);
        }
    }

    private static bool TryGetMod(string input, out Mod mod, CommandCaller caller)
    {
        List<Mod> possibleMatches = new List<Mod>();
        foreach (Mod m in ModLoader.Mods)
        {
            if (m.Name.Equals(input, StringComparison.OrdinalIgnoreCase)
                || m.DisplayName.Equals(input, StringComparison.OrdinalIgnoreCase))
            {
                mod = m;
                return true;
            }
            else if (m.Name.Contains(input, StringComparison.OrdinalIgnoreCase)
                || m.DisplayName.Contains(input, StringComparison.OrdinalIgnoreCase))
            {
                possibleMatches.Add(m);
            }
        }

        if (possibleMatches.Count <= 0)
        {
            mod = null;
            caller.Reply($"No mod named \"{input}\" found.");
            return false;
        }
        else if (possibleMatches.Count <= 1)
        {
            mod = possibleMatches[0];
            caller.Reply($"Assuming you meant \"{possibleMatches[0].DisplayName}\"?");
            return true;
        }
        else
        {
            mod = null;
            caller.Reply($"Cannot guess between: {string.Join(", ", possibleMatches)}");
            return false;
        }
    }

    private static string Encode(string rawText)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < rawText.Length; i++)
        {
            stringBuilder.Append(Convert.ToString(rawText[i], 16));
        }
        return stringBuilder.ToString();
    }

    /// <param name="extension">Do not include the period. Examples: "exe", "txt"</param>
    private static FileStream SafeFileCreate(string directory, string fileName, string extension, out string path)
    {
        string testPath = $"{directory}\\{fileName}.{extension}";

        int i = 2;
        while (File.Exists(testPath))
        {
            testPath = $"{directory}\\{fileName} ({i}).{extension}";
            i++;
        }

        path = testPath;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.Create(path);
    }

    enum PrintType
    {
        NPC,
        Weapons,
        Projectiles,
        Ammo,
        Armor,
        Vanity,
        Wearable,
        AllItems
    }

    enum ProjectileDoesDamage
    {
        NoItem,
        Damage,
        NoDamage,
        SometimesDamage
    }
}
