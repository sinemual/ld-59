using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class StringExtensions
    {
        private static string[] _suffixes =
        {
            "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah",
            "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az",
            "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br",
            "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
        };

        private static readonly Dictionary<string, Dictionary<double, string>> _cachedAppendString =
            new Dictionary<string, Dictionary<double, string>>();

        private static readonly Dictionary<string, Dictionary<double, string>> _cachedPrependString =
            new Dictionary<string, Dictionary<double, string>>();

        private static readonly StringBuilder _stringBuilder = new StringBuilder();

        private static readonly Dictionary<Type, string> _cachedStateNames = new Dictionary<Type, string>();

        private static double GetPrettifiedNumber(this double number, out string suffix)
        {
            if (number < 1000)
            {
                suffix = _suffixes[0];
                return number;
            }

            var powerOfTen = (int)Math.Log10(number);
            powerOfTen = (powerOfTen / 3) * 3;
            number /= Math.Pow(10, powerOfTen);
            suffix = _suffixes[powerOfTen / 3];
            return Math.Floor(number * 10) / 10;
        }

        public static string GetCachedAppend(this double value, string append)
        {
            if (_cachedAppendString.TryGetValue(append, out var dic))
            {
                if (dic.TryGetValue(value, out var str))
                    return str;
            }
            else
                dic = _cachedAppendString[append] = new Dictionary<double, string>();

            _stringBuilder.Clear();
            return dic[value] = _stringBuilder.Append($"{value}{append}").ToString();
        }

        public static string GetCachedPrepend(this double value, string prepend)
        {
            if (_cachedPrependString.TryGetValue(prepend, out var dic))
            {
                if (dic.TryGetValue(value, out var str))
                    return str;
            }
            else
                dic = _cachedPrependString[prepend] = new Dictionary<double, string>();

            _stringBuilder.Clear();
            return dic[value] = _stringBuilder.Append($"{prepend}{value}").ToString();
        }


        public static string GetCachedPrepend(this ulong number, string prepend) =>
            GetCachedPrepend((double)number, prepend);


        public static string PrettifyNumber(this ulong number) => GetPrettifiedNumber(number, out var s).GetCachedAppend(s);
        public static string PrettifyNumber(this float number) => PrettifyNumber((ulong)number);
        public static string PrettifyNumber(this int number) => PrettifyNumber((ulong)number);
        public static string PrettifyNumber(this double number) => GetPrettifiedNumber(number, out var s).GetCachedAppend(s);

        public static string GetStateName(this Type type)
        {
            if (_cachedStateNames.TryGetValue(type, out var name))
                return name;
            var split = type.ToString().Split('.');
            return _cachedStateNames[type] = split[split.Length - 1];
        }

        public static bool IsNullOrWhitespace(this string str) =>
            string.IsNullOrWhiteSpace(str);
    }
}