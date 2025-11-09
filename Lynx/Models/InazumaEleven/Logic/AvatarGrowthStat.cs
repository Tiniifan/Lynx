using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.InazumaEleven.Logic
{
    public class AvatarGrowthStat
    {
        public List<int> FG { get; set; }
        public List<int> Attack { get; set; }

        public AvatarGrowthStat(List<int> fg, List<int> attack)
        {
            FG = fg ?? new List<int>() { 0, 0, 0, 0, 0 };
            Attack = attack ?? new List<int>() { 0, 0, 0, 0, 0 };
        }
    }
}
