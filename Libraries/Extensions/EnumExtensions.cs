using System;
using System.ComponentModel;
using System.Linq;

namespace HouseofCat.Library.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Extension method of getting the Description value to string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : "Description Not Found";
        }

    }
}
