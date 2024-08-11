using System.Collections.Generic;

namespace Lynx.Level5.Save.Logic.Competition_Route
{
    public class Route
    {
        public string Name { get; set; }

        public List<Cell> Cells { get; set; }

        public Route(string name, List<Cell> cells)
        {
            Name = name;
            Cells = cells;
        }
    }
}
