using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.InazumaEleven.Logic
{
    public interface ITeamParamInfo
    {
        int TeamParamID { get; set; }
        int TeamConfigID { get; set; }
        int Friendship { get; set; }
        int Prestige { get; set; }
        int VictoryPoints { get; set; }
        int BootsID { get; set; }
        int GlovesID { get; set; }
        int BraceletID { get; set; }
        int PendantID { get; set; }
        int DropID1 { get; set; }
        int DropID2 { get; set; }
        int DropID3 { get; set; }
        int DropID4 { get; set; }
        int DropID5 { get; set; }
        int Uniform { get; set; }
        int DropRate1 { get; set; }
        int DropRate2 { get; set; }
        int DropRate3 { get; set; }
        int DropRate4 { get; set; }
        int DropRate5 { get; set; }
        int FormationID { get; set; }
        int Level { get; set; }
        int DropID6 { get; set; }
        int DropRate6 { get; set; }
        int NicePlayBonus { get; set; }
        int CoachID { get; set; }
        int TacticID { get; set; }
        int AILevel { get; set; }
    }
}
