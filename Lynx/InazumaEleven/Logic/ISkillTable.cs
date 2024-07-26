namespace Lynx.InazumaEleven.Logic
{
    public interface ISkillTable
    {
        int SkillIndex { get; set; }
        int LevelLearned { get; set; }
        int SkillHash { get; set; }
        int Level { get; set; }
    }
}
