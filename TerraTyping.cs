using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using TerraTyping.DataTypes;
using TerraTyping.Helpers;
using TerraTyping.TypeLoaders;
using TerraTyping.Core;
using TerraTyping.Core.Abilities.Buffs;

namespace TerraTyping
{
    class TerraTyping : Mod
    {
        private static TerraTyping instance;

        public static TerraTyping Instance
        {
            get => (instance ??= ModContent.GetInstance<TerraTyping>());
            private set => instance = value;
        }

        public override void Load()
        {
            Instance = this;

            ElementHelper.Load();
            ElementArray.Load();

            TypeLoader.Logger = Logger;

            NPCTypeLoader.Instance.InitTypeInfoCollection();
            WeaponTypeLoader.Instance.InitTypeInfoCollection();
            AmmoTypeLoader.Instance.InitTypeInfoCollection();
            ArmorTypeLoader.Instance.InitTypeInfoCollection();
            ProjectileTypeLoader.Instance.InitTypeInfoCollection();
            SpecialItemTypeLoader.Instance.InitTypeInfoCollection();
        }

        public override void PostSetupContent()
        {
            TypeLoader.Logger = Logger;
            Table.Load();
            Calc.Load();

            SpecialTooltip.StaticLoad();

            TypeLoader.SetupAllDefaultVanillaTypes();
            TypeLoader.SetupAllDefaultModTypes();
            TypeLoader.SetupAllUserOverrides();

            ProjectileWrapper.PostSetupContent();

            SpecialTooltip.Finish();

            ElementArray.Clean(false, true);
        }

        public override void Unload()
        {
            BuffUtils.addTypeBuffs = null;
            BuffUtils.replaceTypeBuffs = null;

            Calc.Unload();

            ProjectileWrapper.Unload();
            ElementHelper.Unload();
            ElementArray.Unload();
            Table.Unload();

            SpecialTooltip.StaticUnload();
            Instance = null;
        }

        public override object Call(params object[] args)
        {
            if (args.Length != 1)
            {
                LogHelper.Log(Logger, Verbosity.Warn, "Call", "Invoked Call(object[]) with too many or too few arguments. Expected 1 argument. Please use an IDictionary<string, object> to pass your arguments.");
                return null;
            }

            if (args[0] is null)
            {
                LogHelper.Log(Logger, Verbosity.Warn, "Call", $"Argument passed to Call(object[]) is null.");
                return null;
            }

            if (args[0] is not IDictionary<string, object> argumentDictionary)
            {
                LogHelper.Log(Logger, Verbosity.Warn, "Call", $"Argument passed to Call(object[]) is not an IDictionary<string, object>. It is a {args[0].GetType().FullName}.");
                return null;
            }

            Dictionary<string, object> sanitizedArgumentDictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> kvp in argumentDictionary)
            {
                bool result = sanitizedArgumentDictionary.TryAdd(kvp.Key.ToLower(), kvp.Value);
                if (!result)
                {
                    LogHelper.Log(Logger, Verbosity.Error, "Call", $"The given key appears multiple times with different cases. Key: '{kvp.Key}'.");
                }
            }

            if (!sanitizedArgumentDictionary.TryPopValueAs("call", out string callStr, out string _error))
            {
                LogHelper.Log(Logger, Verbosity.Warn, "Call", _error);
                return null;
            }

            if (callStr.Equals("AddTypes", StringComparison.OrdinalIgnoreCase))
            {
                ModifyTypes(sanitizedArgumentDictionary, AddOrModifyTypes.AddTypes);
                return null;
            }

            if (callStr.Equals("OverrideTypes", StringComparison.OrdinalIgnoreCase))
            {
                ModifyTypes(sanitizedArgumentDictionary, AddOrModifyTypes.ModifyTypes);
                return null;
            }

            return null;
        }

        private void ModifyTypes(IDictionary<string, object> argumentDictionary, AddOrModifyTypes addOrModifyTypes)
        {
            const string TypesToAddKey = "typestoadd";
            const string CallingModKey = "callingmod";
            const string FileNameKey = "filename";
            const string ModTargetKey = "modtarget";

            if (!argumentDictionary.TryPopValueAs(TypesToAddKey, out string typesToAdd, out string error))
            {
                LogHelper.Log(Logger, Verbosity.Error, "Call", error);
                return;
            }

            if (!argumentDictionary.TryPopValueAs(CallingModKey, out Mod callingMod, out error))
            {
                LogHelper.Log(Logger, Verbosity.Error, "Call", error);
                return;
            }

            if (!argumentDictionary.TryPopValueAs(FileNameKey, out string fileName, out error))
            {
                LogHelper.Log(Logger, Verbosity.Error, "Call", error);
                return;
            }

            if (!callingMod.FileExists(fileName))
            {
                LogHelper.Log(Logger, Verbosity.Error, "Call", $"File does not exist. FileName: {fileName}.");
                return;
            }

            Mod modTarget = callingMod;
            if (argumentDictionary.TryPopValue(ModTargetKey, out object modTargetObj))
            {
                if (modTargetObj is null)
                {
                    modTarget = null;
                }
                else if (modTargetObj is Mod mod)
                {
                    modTarget = mod;
                }
                else
                {
                    LogHelper.Log(Logger, Verbosity.Warn, "Call", $"Argument named {ModTargetKey} could not be cast to type {typeof(Mod)}.", ("Argument Type", modTargetObj.GetType()));
                }
            }

            switch (typesToAdd.ToLower())
            {
                case "ammo":
                    AmmoTypeLoader.Instance.LoadOtherTypesFromCall(callingMod, fileName, modTarget);
                    break;
                case "armor":
                    ArmorTypeLoader.Instance.LoadOtherTypesFromCall(callingMod, fileName, modTarget);
                    break;
                case "npc":
                    NPCTypeLoader.Instance.LoadOtherTypesFromCall(callingMod, fileName, modTarget);
                    break;
                case "projectile":
                    ProjectileTypeLoader.Instance.LoadOtherTypesFromCall(callingMod, fileName, modTarget);
                    break;
                case "specialitem":
                    SpecialItemTypeLoader.Instance.LoadOtherTypesFromCall(callingMod, fileName, modTarget);
                    break;
                case "weapon":
                    WeaponTypeLoader.Instance.LoadOtherTypesFromCall(callingMod, fileName, modTarget);
                    break;
               default:
                    break;
            }

            argumentDictionary.Remove("call");
            if (argumentDictionary.Count > 0)
            {
                LogHelper.Log(Logger, Verbosity.Error, "Call", $"The provided argument dictionary contains more entries than expected. Unused entries: {string.Join(", ", argumentDictionary.Keys)}.");
            }
        }

        enum AddOrModifyTypes
        {
            AddTypes,
            ModifyTypes
        }

        private static bool IsArg<T>(object[] args, int index, out T t, out string error)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (index < 0 || index >= args.Length)
            {
                t = default;
                error = $"TerraTyping.Call(params object[] args): Not enough arguments provided.";
                return false;
            }
            else if (args[index] is not T _t)
            {
                t = default;
                error = $"TerraTyping.Call(params object[] args): Argument at index {index} is not expected type {typeof(T)}.";
                return false;
            }
            else
            {
                t = _t;
                error = string.Empty;
                return true;
            }
        }
    }
}
