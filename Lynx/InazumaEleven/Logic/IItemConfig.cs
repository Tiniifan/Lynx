namespace Lynx.InazumaEleven.Logic
{
    public interface IItemConfig
    {
        int ItemID { get; set; }
        int NameID { get; set; }
        int DescriptionID { get; set; }
        int ItemCategory { get; set; }
    }

    public interface IItemDirector : IItemConfig
    {
        new int ItemID { get; set; }
        int ItemNumber { get; set; }
        new int NameID { get; set; }
        new int DescriptionID { get; set; }
        new int ItemCategory { get; set; }
        int PlayerGroupBuff1 { get; set; }
        int PlayerGroupBuff2 { get; set; }
        int PlayerGroupBuff3 { get; set; }
        int PlayerGroupDebuff1 { get; set; }
        int PlayerGroupDebuff2 { get; set; }
        int PlayerGroupDebuff3 { get; set; }
        int FPCompatible { get; set; }
        int TPCompatible { get; set; }
        int KickCompatible { get; set; }
        int DribbleCompatible { get; set; }
        int TechniqueCompatible { get; set; }
        int BlockCompatible { get; set; }
        int SpeedCompatible { get; set; }
        int StaminaCompatible { get; set; }
        int CatchCompatible { get; set; }
        int LuckCompatible { get; set; }
        int FPNotCompatible { get; set; }
        int TPNotCompatible { get; set; }
        int KickNotCompatible { get; set; }
        int DribbleNotCompatible { get; set; }
        int TechniqueNotCompatible { get; set; }
        int BlockNotCompatible { get; set; }
        int SpeedNotCompatible { get; set; }
        int StaminaNotCompatible { get; set; }
        int CatchNotCompatible { get; set; }
        int LuckNotCompatible { get; set; }
        int ItemPositionX { get; set; }
        int ItemPositionY { get; set; }
    }
}
