using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.InazumaEleven.Logic
{
    public class ChallengeRoute
    {
        public string Filename { get; set; }
        public int NameID { get; set; }
        public List<IRouteConfig> Cells { get; set; }

        public ChallengeRoute() { }

        public ChallengeRoute(string filename, int nameID, List<IRouteConfig> cells)
        {
            Filename = filename;
            NameID = nameID;
            Cells = cells;
        }
    }
}
