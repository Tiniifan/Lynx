using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.InazumaEleven.Logic
{
    public class Team
    {
        public int ID { get; set; }
        public int Emblem { get; set; }
        public int NameID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public Team() { }

        public Team(int id, int emblem, int nameID, int level)
        {
            ID = id;
            Emblem = emblem;
            NameID = nameID;
            Level = level;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}