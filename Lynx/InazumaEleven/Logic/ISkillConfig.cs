namespace Lynx.InazumaEleven.Logic
{
    public interface ISkillConfig
    {
        int SkillHash { get; set; }
        int NameHash { get; set; }
        int DescriptionHash { get; set; }
        int Element { get; set; }
        int EvolutionType { get; set; }
        int EvolutionGrow { get; set; }
        int SkillPosition { get; set; }
        int TPCost { get; set; }
        int Number { get; set; }
        int SkillType { get; set; }
        int EffectType { get; set; }
        int PartnerNumber { get; set; }
        int Fault { get; set; }
        int Power { get; set; }
        int Technique { get; set; }
        int BoostActiveOn { get; set; }
        int SkillBoost { get; set; }
        int NameWazaID { get; set; }
    }
}
