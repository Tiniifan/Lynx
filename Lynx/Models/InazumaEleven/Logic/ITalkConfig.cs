namespace Lynx.Models.InazumaEleven.Logic
{
    public interface ITalkConfig
    {
        int TalkType { get; set; }
        int TalkValue { get; set; }
        string PhaseAppear { get; set; }
    }
}
