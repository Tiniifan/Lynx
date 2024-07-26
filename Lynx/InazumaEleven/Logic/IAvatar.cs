namespace Lynx.InazumaEleven.Logic
{
    public interface IAvatar
    {
        int AvatarHash { get; set; }
        int AvatarNumber { get; set; }
        int NicknameHash { get; set; }
        int FullNameHash { get; set; }
        int DescriptionHash { get; set; }
        int FightingSpiritPoint { get; set; }
        int Attack { get; set; }
        int AttackUp { get; set; }
        int MoveHash { get; set; }
        int SkillHash { get; set; }
    }
}
