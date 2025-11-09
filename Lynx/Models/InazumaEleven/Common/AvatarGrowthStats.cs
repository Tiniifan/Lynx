using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Models.InazumaEleven.Logic;

namespace Lynx.Models.InazumaEleven.Common
{
    public static class AvatarGrowthStats
    {
        public static Dictionary<int, Dictionary<int, AvatarGrowthStat>> IEGO = new Dictionary<int, Dictionary<int, AvatarGrowthStat>>
        {
            {
                1, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(new List<int> { 9, 9, 6, 3, 3 }, null) },
                    { 2, new AvatarGrowthStat(new List<int> { 6, 6, 6, 6, 6 }, null) },
                    { 3, new AvatarGrowthStat(new List<int> { 3, 3, 6, 9, 9 }, null) }
                }
            },
            {
                2, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(new List<int> { 7, 8, 5, 2, 3 }, new List<int> { 1, 2, 1, 0, 1 }) },
                    { 2, new AvatarGrowthStat(new List<int> { 5, 5, 0, 5, 5 }, new List<int> { 1, 1, 1, 1, 1 }) },
                    { 3, new AvatarGrowthStat(new List<int> { 2, 3, 5, 7, 8 }, new List<int> { 0, 1, 1, 0, 2 }) }
                }
            },
            {
                3, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(new List<int> { 6, 6, 4, 2, 2 }, new List<int> { 3, 3, 2, 1, 1 }) },
                    { 2, new AvatarGrowthStat(new List<int> { 4, 4, 4, 4, 4 }, new List<int> { 2, 2, 2, 2, 2 }) },
                    { 3, new AvatarGrowthStat(new List<int> { 2, 2, 4, 6, 6 }, new List<int> { 1, 1, 2, 3, 3 }) }
                }
            },
            {
                4, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(new List<int> { 4, 5, 3, 1, 2 }, new List<int> { 4, 5, 3, 1, 2 }) },
                    { 2, new AvatarGrowthStat(new List<int> { 3, 3, 3, 3, 3 }, new List<int> { 3, 3, 3, 3, 3 }) },
                    { 3, new AvatarGrowthStat(new List<int> { 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5 }) }
                }
            },
            {
                5, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(new List<int> { 3, 3, 2, 1, 1 }, new List<int> { 6, 6, 4, 2, 2 }) },
                    { 2, new AvatarGrowthStat(new List<int> { 2, 2, 2, 2, 2 }, new List<int> { 4, 4, 4, 4, 4 }) },
                    { 3, new AvatarGrowthStat(new List<int> { 1, 1, 2, 3, 3 }, new List<int> { 2, 2, 4, 6, 6 }) }
                }
            },
            {
                6, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(new List<int> { 1, 2, 1, 0, 1 }, new List<int> { 7, 8, 5, 2, 3 }) },
                    { 2, new AvatarGrowthStat(new List<int> { 1, 1, 1, 1, 1 }, new List<int> { 5, 5, 0, 5, 5 }) },
                    { 3, new AvatarGrowthStat(new List<int> { 0, 1, 1, 0, 2 }, new List<int> { 2, 3, 5, 7, 8 }) }
                }
            },
            {
                7, new Dictionary<int, AvatarGrowthStat>
                {
                    { 1, new AvatarGrowthStat(null, new List<int> { 9, 9, 6, 3, 3 }) },
                    { 2, new AvatarGrowthStat(null, new List<int> { 6, 6, 6, 6, 6 }) },
                    { 3, new AvatarGrowthStat(null, new List<int> { 3, 3, 6, 9, 9 }) }
                }
            },
        };
    }
}
