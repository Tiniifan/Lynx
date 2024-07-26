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
        int EffectType { get; set; }
        int PartnerNumber { get; set; }
        int Power { get; set; }
    }
}
