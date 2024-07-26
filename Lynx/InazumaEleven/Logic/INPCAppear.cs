namespace Lynx.InazumaEleven.Logic
{
    public interface INPCAppear
    {
        float LocationX { get; set; }
        float LocationZ { get; set; }
        float LocationY { get; set; }
        float Rotation { get; set; }
        string StandAnimation { get; set; }
        string TalkAnimation { get; set; }
        string UnkAnimation { get; set; }
        string PhaseAppear { get; set; }
    }
}
