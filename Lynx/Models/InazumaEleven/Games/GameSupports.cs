using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioElevenLib.Level5.Archive.ARC0;

namespace Lynx.Models.InazumaEleven.Games
{
    /// <summary>
    /// Provides support classes for the game.
    /// </summary>
    public class GameSupports
    {
        /// <summary>
        /// Represents a game file with an ARC0 archive and a path.
        /// </summary>
        public class GameFile
        {
            public ARC0 File;
            public string Path;


            /// <summary>
            /// Initializes a new instance of the <see cref="GameFile"/> class.
            /// </summary>
            public GameFile()
            {

            }

            /// <summary>
            /// Initializes a new instance of the <see cref="GameFile"/> class with a specified ARC0 archive and path.
            /// </summary>
            /// <param name="file">The ARC0 archive.</param>
            /// <param name="path">The path of the game file.</param>
            public GameFile(ARC0 file, string path)
            {
                File = file;
                Path = path;
            }
        }
    }
}
