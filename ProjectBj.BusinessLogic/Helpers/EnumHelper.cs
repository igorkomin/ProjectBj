using ProjectBj.BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }

        public static string GetRankName(int rankId)
        {
            string rankName = EnumHelper.GetEnumDescription((CardRanks.Rank)rankId);
            return rankName;
        }
    }
}