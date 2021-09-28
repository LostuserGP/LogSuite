using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Helpers
{
    public static class StringParser
    {
        public static Dictionary<string, string> ParamsToDict(Params parameters)
        {
            var result = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            return result;
        }

        public static string LatinToCyr(string name)
        {
            name = name.Replace("e", "е");
            name = name.Replace("E", "Е");
            name = name.Replace("t", "т");
            name = name.Replace("T", "Т");
            name = name.Replace("y", "у");
            name = name.Replace("Y", "У");
            name = name.Replace("u", "и");
            name = name.Replace("U", "И");
            name = name.Replace("o", "о");
            name = name.Replace("O", "О");
            name = name.Replace("p", "р");
            name = name.Replace("P", "Р");
            name = name.Replace("x", "х");
            name = name.Replace("X", "Х");
            name = name.Replace("a", "а");
            name = name.Replace("A", "А");
            name = name.Replace("h", "н");
            name = name.Replace("H", "Н");
            name = name.Replace("k", "к");
            name = name.Replace("K", "К");
            name = name.Replace("c", "с");
            name = name.Replace("C", "С");
            name = name.Replace("b", "в");
            name = name.Replace("B", "В");
            name = name.Replace("m", "м");
            name = name.Replace("M", "М");

            return name;
        }

        public static bool Like(string source, string name)
        {
            source = LatinToCyr(source);
            name = LatinToCyr(name);
            return source.ToLower().Contains(name.ToLower());
        }

        public static bool StrictLike(string source, string name)
        {
            source = LatinToCyr(source);
            name = LatinToCyr(name);
            return source.ToLower().Trim().Equals(name.ToLower().Trim());
        }

        public static bool NameContainAnyList(List<string> source, string name)
        {
            name = LatinToCyr(name);
            foreach (var item in source)
            {
                var val = LatinToCyr(item);
                if (name.ToLower().Contains(val.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool NameEqualsAnyList(List<string> source, string name)
        {
            name = LatinToCyr(name);
            foreach (var item in source)
            {
                var val = LatinToCyr(item);
                if (name.ToLower().Trim().Equals(val.ToLower().Trim()))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool NameContainAllList(List<string> source, string name)
        {
            name = LatinToCyr(name);
            foreach (var item in source)
            {
                var val = LatinToCyr(item);
                if (!name.ToLower().Contains(val.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }

        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };

        public static DateTime? GetFirstDateFromString(string name)
        {
            var array = name.Split(new char[] { ',', ' ', '.', '_', '-' });

            int day = 0;
            int month = 0;
            int year = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i].Length == 2 && int.TryParse(array[i], out day))
                {
                    if (int.TryParse(array[i + 1], out month) && int.TryParse(array[i + 2], out year))
                    {
                        return TryGetDate(year, month, day);
                    }
                }
                if (array[i].Length == 4 && int.TryParse(array[i], out year))
                {
                    if (int.TryParse(array[i], out year) && int.TryParse(array[i + 1], out month) && int.TryParse(array[i + 2], out day))
                    {
                        return TryGetDate(year, month, day);
                    }
                }
            }
            return null;
        }

        public static DateTime? GetSecondDateFromString(string name)
        {
            var array = name.Split(new char[] { ',', ' ', '.', '_', '-' });

            int day = 0;
            int month = 0;
            int year = 0;
            int count = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i].Length == 2 && int.TryParse(array[i], out day))
                {
                    if (int.TryParse(array[i + 1], out month) && int.TryParse(array[i + 2], out year))
                    {
                        count++;
                        if (count > 1) return TryGetDate(year, month, day);
                    }
                }
                if (array[i].Length == 4 && int.TryParse(array[i], out year))
                {
                    if (int.TryParse(array[i], out year) && int.TryParse(array[i + 1], out month) && int.TryParse(array[i + 2], out day))
                    {
                        count++;
                        if (count > 1) return TryGetDate(year, month, day);
                    }
                }
            }
            return null;
        }

        public static DateTime? GetDateWithTimeFromString(string name)
        {
            var array = name.Split(new char[] { ',', ' ', '.', '_', '-' });

            int day = 0;
            int month = 0;
            int year = 0;
            int hour = 0;
            int minute = 0;
            for (int i = 0; i < array.Length - 3; i++)
            {
                if (array[i].Length == 2 && int.TryParse(array[i], out day)
                    && (array[i + 1].Length == 2)
                    && (array[i + 2].Length == 4)
                    && (array[i + 3].Length == 2 || array[i + 3].Length == 1)
                    && (array[i + 4].Length == 2 || array[i + 4].Length == 1))
                {
                    if (int.TryParse(array[i + 1], out month) && int.TryParse(array[i + 2], out year) && int.TryParse(array[i + 3], out hour) && int.TryParse(array[i + 4], out minute))
                    {
                        return TryGetDate(year, month, day, hour, minute);
                    }
                }
                if (array[i].Length == 4 && int.TryParse(array[i], out year)
                    && (array[i + 1].Length == 2)
                    && (array[i + 2].Length == 2)
                    && (array[i + 3].Length == 2 || array[i + 3].Length == 1)
                    && (array[i + 4].Length == 2 || array[i + 4].Length == 1))
                {
                    if (int.TryParse(array[i + 1], out month) && int.TryParse(array[i + 2], out day) && int.TryParse(array[i + 3], out hour) && int.TryParse(array[i + 4], out minute))
                    {
                        return TryGetDate(year, month, day, hour, minute);
                    }
                }
            }
            return null;
        }

        private static DateTime? TryGetDate(int one, int two, int three)
        {
            try
            {
                if (one >= 1000)
                {
                    return new DateTime(one, two, three);
                }
                else
                {
                    return new DateTime(three, two, one);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static DateTime? TryGetDate(int one, int two, int three, int four, int five)
        {
            try
            {
                if (one >= 1000)
                {
                    return new DateTime(one, two, three, four, five, 0);
                }
                else
                {
                    return new DateTime(three, two, one, four, five, 0);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Double PrepareDoubleForDb(double val)
        {
            if (val < 0)
            {
                return 0d;
            }
            else
            {
                return Math.Round(val, 8);
            }
        }

        public static double TryGetDouble(string value)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                if (value.Contains(","))
                {
                    provider.NumberDecimalSeparator = ",";
                }
                else
                {
                    provider.NumberDecimalSeparator = ".";
                }
                return Convert.ToDouble(value, provider);
            }
            catch (Exception e)
            {
                return 0d;
            }
        }

        public static decimal TryGetDecimal(string value)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                if (value.Contains(","))
                {
                    provider.NumberDecimalSeparator = ",";
                }
                else
                {
                    provider.NumberDecimalSeparator = ".";
                }
                return Convert.ToDecimal(value, provider);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
