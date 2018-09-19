using ProjectBj.Entities.Enums;
using System;
using System.ComponentModel;
using System.Reflection;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }

        public static string GetCardRankName(int rankId)
        {
            string rankName = GetEnumDescription((CardRanks.Rank)rankId);
            return rankName;
        }
    }
}