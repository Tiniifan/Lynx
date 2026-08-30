namespace Lynx.InazumaEleven.Logic
{
    public interface IPalpackCard
    {
        int PalpackCardId { get; set; }
        int NameId { get; set; }
        int DescriptionId { get; set; }
        int MaxQuantity { get; set; }
        int SellingPrice { get; set; }
        int PurchasePrice { get; set; }
        int ItemSubCategory { get; set; }
        int PrerequisitesType1 { get; set; }
        int PrerequisitesId1 { get; set; }
        int PrerequisitesType2 { get; set; }
        int PrerequisitesId2 { get; set; }
        int PrerequisitesType3 { get; set; }
        int PrerequisitesId3 { get; set; }
        int PrerequisitesType4 { get; set; }
        int PrerequisitesId4 { get; set; }
        int CharacterFlag { get; set; }
        int RecrutedCharacterId { get; set; }
        int ItemPosX { get; set; }
        int ItemPosY { get; set; }
    }
}
