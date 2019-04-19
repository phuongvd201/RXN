using System;
using System.Globalization;

namespace Rxn.WebApi.Helpers
{
    internal static class ParsingHelper
    {
        private const string DateFormat = "MM/dd/yyyy";

        public static int? ParseNullableInt(this string value, int? @default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return @default;
            }

            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException();
        }

        public static int? ParseNullableInt(this string value)
        {
            return value.ParseNullableInt(null);
        }

        public static int? ParseNullableId(this string value)
        {
            var result = value.ParseNullableInt();
            if (result.HasValue && result.Value <= 0)
            {
                throw new ArgumentException();
            }

            return result;
        }

        public static DateTime ParseDate(this string value, string dateFormat)
        {
            DateTime result;
            if (string.IsNullOrWhiteSpace(value) || !DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
            {
                throw new ArgumentException();
            }

            return result;
        }

        public static DateTime ParseDate(this string value)
        {
            return value.ParseDate(DateFormat);
        }

        public static DateTime? ParseNullableDate(this string value, string dateFormat)
        {
            DateTime result;
            if (string.IsNullOrWhiteSpace(value) || !DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
            {
                return null;
            }

            return result;
        }

        public static DateTime? ParseNullableDate(this string value)
        {
            return value.ParseNullableDate(DateFormat);
        }

        public static int ParseInt(this string value)
        {
            return ParseInt(value, int.MinValue, int.MaxValue);
        }

        public static int ParseInt(this string value, int minValue)
        {
            return ParseInt(value, minValue, int.MaxValue);
        }

        public static int ParseInt(this string value, int minValue, int maxValue)
        {
            int result;
            if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out result) || result < minValue || result > maxValue)
            {
                throw new ArgumentException();
            }
            return result;
        }

        public static bool ParseBool(this string value)
        {
            bool result;
            if (string.IsNullOrWhiteSpace(value) || !bool.TryParse(value, out result))
            {
                throw new ArgumentException();
            }
            return result;
        }

        public static bool? ParseNullableBool(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return ParseBool(value);
        }

        public static decimal? ParseNullableDecimal(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return ParseDecimal(value);
        }

        public static decimal ParseDecimal(this string value)
        {
            return ParseDecimal(value, decimal.MinValue, decimal.MaxValue);
        }

        public static decimal ParseDecimal(this string value, decimal minValue, decimal maxValue)
        {
            decimal result;
            if (string.IsNullOrWhiteSpace(value) || !decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result) || result < minValue || result > maxValue)
            {
                throw new ArgumentException();
            }
            return result;
        }
    }
}