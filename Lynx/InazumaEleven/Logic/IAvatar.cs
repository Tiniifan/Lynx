namespace Lynx.InazumaEleven.Logic
{
    public interface IAvatar
    {
        int AvatarHash { get; set; }
        int AvatarNumber { get; set; }
        int NicknameHash { get; set; }
        int FullNameHash { get; set; }
        int DescriptionHash { get; set; }
        int CanBeSold { get; set; }
        int CanBeBought { get; set; }
        int SellingPrice { get; set; }
        int PurchasePrice { get; set; }
        int FightingSpiritPoint { get; set; }
        int Attack { get; set; }
        int EvolutionStatGrow { get; set; }
        int Position { get; set; }
        int Element { get; set; }
        int MaxQuantity { get; set; }
        int SpecialMoveID { get; set; }
        int SkillID { get; set; }
        int EvolutionGrow { get; set; }
        int ItemPosX { get; set; }
        int ItemPosY { get; set; }
        int FusionID { get; set; }
        int Partner1FusionID { get; set; }
        int Partner2FusionID { get; set; }
    }
}
