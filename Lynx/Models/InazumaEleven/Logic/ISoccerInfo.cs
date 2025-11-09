using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.Models.InazumaEleven.Logic
{
    public interface ISoccerInfo
    {
        int SoccerID { get; set; }
        int TeamParamID { get; set; }
        int SoccerMode { get; set; }
        string Sound { get; set; }
        int VictoryCondition { get; set; }
        int Script { get; set; }
        int Time { get; set; }
        int NextScript { get; set; }
    }
}
