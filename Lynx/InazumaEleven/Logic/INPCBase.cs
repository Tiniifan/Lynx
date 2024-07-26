namespace Lynx.InazumaEleven.Logic
{
    public interface INPCBase
    {
        int NPCID { get; set; }
        int HeadID { get; set; }
        int Type { get; set; }
        int UniformID { get; set; }
        int BootsID { get; set; }
        int GlovesID { get; set; }
        int IconID { get; set; }
    }
}
