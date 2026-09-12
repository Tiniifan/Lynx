using System.Windows.Forms;
using System.Collections.Generic;
using StudioElevenLib.Tools;
using Lynx.InazumaEleven.Save.Games;

namespace Lynx.InazumaEleven.Save.Saves
{
    public interface ISave
    {
        string Name { get; }

        string Extension { get; }

        string Description { get; }

        IGame Game { get; set; }

        void Open(BinaryDataReader reader, Dictionary<int, InazumaEleven.Save.Logic.Player> players, Dictionary<int, InazumaEleven.Save.Logic.Move> moves, Dictionary<int, InazumaEleven.Save.Logic.Avatar> avatars);

        void Save(OpenFileDialog initialDirectory);
    }
}
