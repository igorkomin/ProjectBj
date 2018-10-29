using ProjectBj.Enums;
using System;
using System.ComponentModel;
using System.Reflection;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class EnumHelper
    {
        public static string GetCardRankName(CardRank rank)
        {
            string rankName = GetEnumDescription(rank);
            return rankName;
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }
    }
}