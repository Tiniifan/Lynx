namespace Lynx.InazumaEleven.Logic
{
    public interface IEncountTeamInfo
    {
        int TeamConfigID { get; set; }
        int NameID { get; set; }
        int Player1 { get; set; }
        int NumberPlayer1 { get; set; }
        int Player2 { get; set; }
        int NumberPlayer2 { get; set; }
        int Player3 { get; set; }
        int NumberPlayer3 { get; set; }
        int Player4 { get; set; }
        int NumberPlayer4 { get; set; }
        int Player5 { get; set; }
        int NumberPlayer5 { get; set; }
        int Emblem1 { get; set; }
        int Emblem2 { get; set; }
        int Emblem3 { get; set; }
        int DifferenceLevelPlayer1 { get; set; }
        int DifferenceLevelPlayer2 { get; set; }
        int DifferenceLevelPlayer3 { get; set; }
        int DifferenceLevelPlayer4 { get; set; }
        int DifferenceLevelPlayer5 { get; set; }
    }

    public interface IStoryTeamInfo : IEncountTeamInfo
    {
        int Player6 { get; set; }
        int NumberPlayer6 { get; set; }
        int Player7 { get; set; }
        int NumberPlayer7 { get; set; }
        int Player8 { get; set; }
        int NumberPlayer8 { get; set; }
        int Player9 { get; set; }
        int NumberPlayer9 { get; set; }
        int Player10 { get; set; }
        int NumberPlayer10 { get; set; }
        int Player11 { get; set; }
        int NumberPlayer11 { get; set; }
        int Player12 { get; set; }
        int NumberPlayer12 { get; set; }
        int Player13 { get; set; }
        int NumberPlayer13 { get; set; }
        int Player14 { get; set; }
        int NumberPlayer14 { get; set; }
        int Player15 { get; set; }
        int NumberPlayer15 { get; set; }
        int Player16 { get; set; }
        int NumberPlayer16 { get; set; }
        int DifferenceLevelPlayer6 { get; set; }
        int DifferenceLevelPlayer7 { get; set; }
        int DifferenceLevelPlayer8 { get; set; }
        int DifferenceLevelPlayer9 { get; set; }
        int DifferenceLevelPlayer10 { get; set; }
        int DifferenceLevelPlayer11 { get; set; }
        int DifferenceLevelPlayer12 { get; set; }
        int DifferenceLevelPlayer13 { get; set; }
        int DifferenceLevelPlayer14 { get; set; }
        int DifferenceLevelPlayer15 { get; set; }
        int DifferenceLevelPlayer16 { get; set; }
    }
}