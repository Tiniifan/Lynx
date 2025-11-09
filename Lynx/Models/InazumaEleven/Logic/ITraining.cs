namespace Lynx.Models.InazumaEleven.Logic
{
    public interface ITraining
    {
        int KickDown { get; set; }
        int DribbleDown { get; set; }
        int TechniqueDown { get; set; }
        int BlockDown { get; set; }
        int SpeedDown { get; set; }
        int StaminaDown { get; set; }
        int CatchDown { get; set; }
        int LuckDown { get; set; }
    }
}
