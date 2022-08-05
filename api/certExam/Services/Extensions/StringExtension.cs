using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace certExam.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static int FormNumber(this string value)
        {
            var values = value.ToString().Split('-');
            int docNumber = 0;
            if (values.Length > 1)
            {
                var data = values[1].ToString().Substring(8);
                if (int.TryParse(data, out docNumber) == false)
                {
                    return docNumber;
                }
            }
            return docNumber + 1;
        }

        public static int ToNumber(this string value)
        {
            int num = 0;
            int.TryParse(value, out num);
            return num;
        }

        public static short ToShort(this string value)
        {
            short num = 0;
            short.TryParse(value, out num);
            return num;
        }
        public static decimal ToDecimal(this string value)
        {
            decimal num = 0;
            decimal.TryParse(value, out num);
            return num;
        }

        public static bool IsEmpty(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) == true)
            {
                return true;
            }
            if (string.IsNullOrEmpty(value) == true)
            {
                return true;
            }
            return false;
        }
    }
}