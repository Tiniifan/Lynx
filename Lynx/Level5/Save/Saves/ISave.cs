using System.Windows.Forms;
using System.Collections.Generic;
using Lynx.Tools;
using Lynx.Level5.Save.Games;

namespace Lynx.Level5.Save.Saves
{
    public interface ISave
    {
        string Name { get; }

        string Extension { get; }

        string Description { get; }

        IGame Game { get; set; }

        void Open(BinaryDataReader reader, Dictionary<int, Level5.Save.Logic.Player> players, Dictionary<int, Level5.Save.Logic.Move> moves, Dictionary<int, Level5.Save.Logic.Avatar> avatars);

        void Save(OpenFileDialog initialDirectory);
    }
}
