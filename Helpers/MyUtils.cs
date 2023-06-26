using System;
using System.Collections.Generic;
using Terraria;

namespace TerraTyping.Helpers
{
    public static class MyUtils
    {
        public static Time TimeConverter(int militaryHours, int minutes, int seconds)
        {
            // convert the hours and minutes to seconds
            int totalSeconds = (militaryHours * 3600) + (minutes * 60) + seconds;
            int timeSince430;
            if (totalSeconds >= 16200)
            {
                timeSince430 = totalSeconds - 16200;
            }
            else
            {
                timeSince430 = totalSeconds + 70200;
            }
            bool day = true;
            if (timeSince430 >= 54000)
            {
                timeSince430 -= 54000;
                day = false;
            }
            return new Time(timeSince430, day);
        }
        public static Time TimeConverter(int hours, int minutes, int seconds, bool am)
        {
            // convert to military hours
            // 12:00 -> 0000
            int militaryHours = hours;
            if (militaryHours >= 12)
            {
                militaryHours -= 12;
            }
            if (!am)
            {
                militaryHours += 12;
            }
            return TimeConverter(militaryHours, minutes, seconds);
        }
        public static Time TimeConverter(int militaryHours, int minutes) => TimeConverter(militaryHours, minutes, 0);
        public static Time TimeConverter(int hours, int minutes, bool am) => TimeConverter(hours, minutes, 0, am);

        public static TValue AddIfMissingAndGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }

            return dictionary[key];
        }

        public static TValue AddIfMissingAndGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getValue)
        {
            getValue ??= () => default;

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, getValue() ?? default);
            }

            return dictionary[key];
        }

        public static TValue AddNewIfMissingAndGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new TValue());
            }

            return dictionary[key];
        }

        public static T[] FilledArray<T>(Func<T> filler, int count)
        {
            if (filler is null)
            {
                throw new ArgumentNullException(nameof(filler));
            }

            T[] values = new T[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = filler();
            }
            return values;
        }

        public static string SafeGet(this string[] stringArray, int index, string returnIfOutOfRange = "")
        {
            if (index >= 0 && index < stringArray.Length)
            {
                return stringArray[index];
            }
            else
            {
                return returnIfOutOfRange;
            }
        }

        public static string[] SafeGet(this string[] stringArray, Range range, bool returnEmptyArrayIfOutOfRange = true)
        {
            int start = range.Start.GetOffset(stringArray.Length);
            int end = range.End.GetOffset(stringArray.Length);
            if (start <= end && start >= 0 && end <= stringArray.Length)
            {
                return stringArray[range];
            }
            else
            {
                return returnEmptyArrayIfOutOfRange ? Array.Empty<string>() : null;
            }
        }

        public static bool TryGet(this string[] stringArray, int index, out string result, string returnIfOutOfRange = "")
        {
            if (index >= 0 && index < stringArray.Length)
            {
                result = stringArray[index];
                return true;
            }
            else
            {
                result = returnIfOutOfRange;
                return false;
            }
        }

        public static bool TryGet(this string[] stringArray, Range range, out string[] result, bool returnEmptyArrayIfOutOfRange = true)
        {
            int start = range.Start.GetOffset(stringArray.Length);
            int end = range.End.GetOffset(stringArray.Length);
            if (start <= end && start >= 0 && end <= stringArray.Length)
            {
                result = stringArray[range];
                return true;
            }
            else
            {
                result = returnEmptyArrayIfOutOfRange ? Array.Empty<string>() : null;
                return false;
            }
        }

        public static T? GetOrNull<T>(this T[] values, int index)
            where T : struct
        {
            return index < 0 || index >= values.Length ? null : values[index];
        }

        public static char? GetOrNull(this string str, int index)
        {
            return index < 0 || index >= str.Length ? null : str[index];
        }

#nullable enable
        public static bool TryGetValueAs<TKey, TValue, TExpected>(this IDictionary<TKey, TValue> dictionary, TKey key, out TExpected? expected, out string error)
        {
            if (!dictionary.TryGetValue(key, out TValue? value))
            {
                expected = default;
                error = $"Key could not be found. Key: {key}.";
                return false;
            }
            else if (value is not TExpected _expected)
            {
                expected = default;
                error = $"Value could not be cast to expected type. Value: {value}. Expected type: {typeof(TExpected)}.";
                return false;
            }
            else
            {
                expected = _expected;
                error = string.Empty;
                return true;
            }
        }

        public static bool TryPopValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue? value)
        {
            if (dictionary.TryGetValue(key, out value))
            {
                dictionary.Remove(key);
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public static bool TryPopValueAs<TKey, TValue, TExpected>(this IDictionary<TKey, TValue> dictionary, TKey key, out TExpected? expected, out string error)
        {
            if (!dictionary.TryGetValue(key, out TValue? value))
            {
                expected = default;
                error = $"Key could not be found. Key: {key}.";
                return false;
            }
            else if (value is not TExpected _expected)
            {
                expected = default;
                error = $"Value could not be cast to expected type. Value: {value}. Expected type: {typeof(TExpected)}.";
                return false;
            }
            else
            {
                expected = _expected;
                error = string.Empty;
                dictionary.Remove(key);
                return true;
            }
        }
#nullable restore

        public static T Random<T>(this IList<T> values, bool throwOnEmpty = true)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Count == 0)
            {
                if (throwOnEmpty)
                {
                    throw new ArgumentException("IList contains 0 elements.", nameof(values));
                }
                else
                {
                    return default;
                }
            }

            return values[Main.rand.Next(values.Count)];
        }

        /// <summary>
        /// Combines two arrays into one. All elements in <paramref name="secondArray"/> will come after all elements in <paramref name="firstArray"/>.
        /// </summary>
        public static T[] ArrayCombiner<T>(T[] firstArray, T[] secondArray)
        {
            ArgumentNullException.ThrowIfNull(firstArray);
            ArgumentNullException.ThrowIfNull(secondArray);

            T[] newArray = new T[firstArray.Length + secondArray.Length];
            for (int i = 0; i < firstArray.Length; i++)
            {
                newArray[i] = firstArray[i];
            }

            for (int i = 0; i < secondArray.Length; i++)
            {
                newArray[firstArray.Length + i] = secondArray[i];
            }
            return newArray;
        }
    }

    public struct Time
    {
        public double time;
        public bool dayTime;

        public Time(double time, bool dayTime)
        {
            this.time = time;
            this.dayTime = dayTime;
        }
    }
}
