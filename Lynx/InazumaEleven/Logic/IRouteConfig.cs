using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.InazumaEleven.Logic
{
    public interface IRouteConfig
    {
        int Flag { get; set; }
        int CellType { get; set; }
        int ContentID { get; set; }
        int MatchRestriction { get; set; }
        int CellNum { get; set; }
        int CellLink1 { get; set; }
        int CellLink2 { get; set; }
        int CellLink3 { get; set; }
        string PhaseAppear { get; set; }
        string Map { get; set; }
        int Unk10 { get; set; }
        string MatchTextLock { get; set; }
    }
}
