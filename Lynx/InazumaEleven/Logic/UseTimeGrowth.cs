namespace Lynx.InazumaEleven.Logic
{
    public interface IAvatarTimeGrowth
    {
        int Level1 { get; set; }
        int Level2 { get; set; }
        int Level3 { get; set; }
        int Level4 { get; set; }
        int Level5 { get; set; }
        int Level6 { get; set; }
    }

    public interface ISkillUseTimeGrowth
    {
        int Level1 { get; set; }
        int Level2 { get; set; }
        int Level3 { get; set; }
        int Level4 { get; set; }
    }
}
