namespace Lynx.InazumaEleven.Logic
{
    public interface ICharaparam
    {
        int ParamHash { get; set; }
        int BaseHash { get; set; }
        int FP { get; set; }
        int TP { get; set; }
        int Kick { get; set; }
        int Dribble { get; set; }
        int Technique { get; set; }
        int Block { get; set; }
        int Speed { get; set; }
        int Stamina { get; set; }
        int Catch { get; set; }
        int Luck { get; set; }
        int Element { get; set; }
        int PlayerPosition { get; set; }
        int DescriptionHash { get; set; }
        int Freedom { get; set; }
        int FightingSpiritHash { get; set; }
        int SpecialMoveCount { get; set; }
        int SpecialMoveOffset { get; set; }
        int ExperienceGrow { get; set; }
        int StatVariance { get; set; }
        int FPGrow { get; set; }
        int TPGrow { get; set; }
        int KickGrow { get; set; }
        int DribbleGrow { get; set; }
        int TechniqueGrow { get; set; }
        int BlockGrow { get; set; }
        int SpeedGrow { get; set; }
        int StaminaGrow { get; set; }
        int CatchGrow { get; set; }
        int LuckGrow { get; set; }
        int TrainingUD { get; set; }
        int FightingSpiritMatchkHash { get; set; }
    }
}
