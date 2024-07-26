namespace Lynx.InazumaEleven.Logic
{
    public interface ICharabase
    {
        int BaseHash { get; set; }
        int ModelNumber { get; set; }
        int NameHash { get; set; }
        int NicknameHash { get; set; }
        int CharaBaseType { get; set; }
        int Body { get; set; }
        int Skin { get; set; }
        int Gender { get; set; }
        int Year { get; set; }
        int DescriptionHash { get; set; }
    }
}
