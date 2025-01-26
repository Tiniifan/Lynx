using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Tools;

namespace Lynx.Level5.Archive
{
    /// <summary>
    /// Represents an archive interface with methods for saving and closing the archive.
    /// </summary>
    public interface IArchive
    {
        /// <summary>
        /// Gets the name of the archive.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the virtual directory of the archive.
        /// </summary>
        VirtualDirectory Directory { get; set; }

        /// <summary>
        /// Saves the archive to the specified path.
        /// </summary>
        /// <param name="path">The path to save the archive to.</param>
        void Save(string path);

        /// <summary>
        /// Saves the archive and returns the byte array representation.
        /// </summary>
        /// <returns>The byte array representation of the saved archive.</returns>
        byte[] Save();

        /// <summary>
        /// Closes the archive and returns the closed archive.
        /// </summary>
        /// <returns>The closed archive.</returns>
        IArchive Close();
    }
}
