using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.Models.InazumaEleven.Logic
{
    public class CoachText
    {
        public string Name { get; set; }
        public string StatText1 { get; set; }
        public string StatText2 { get; set; }

        public CoachText() { }

        public CoachText(string name, string statText1, string statText2)
        {
            Name = name;
            StatText1 = statText1;
            StatText2 = statText2;
        }
    }
}
