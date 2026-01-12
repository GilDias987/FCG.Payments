using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace FCG.Payments.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            // Get the field info for the specific enum value
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            // Retrieve the DescriptionAttribute applied to the field
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // Return the description if it exists, otherwise return the enum member's name as a string
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
