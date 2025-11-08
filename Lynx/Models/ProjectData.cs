using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.Models
{
    /// <summary>
    /// Represents the basic data structure for a project, including its metadata and file information.
    /// </summary>
    public class ProjectData
    {
        /// <summary>
        /// Gets or sets the unique identifier of the project.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the display name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the programming language used by the project.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the version number of the project.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the filesystem path where the project is located.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the path represents a directory.
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the relative path to the project's image or icon.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the project currently exists on the filesystem.
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Gets or sets the associated game type for this project.
        /// </summary>
        public GameType Game { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectData"/> class with a unique ID and default values.
        /// </summary>
        public ProjectData()
        {
            Id = Guid.NewGuid().ToString();
            Exists = true;
        }

        /// <summary>
        /// Returns the full image path by combining the base directory with the image filename.
        /// </summary>
        /// <returns>The full path to the project's image, or null if no image is defined.</returns>
        public string GetFullImagePath()
        {
            if (string.IsNullOrEmpty(ImagePath))
                return null;

            return System.IO.Path.Combine("projects", ImagePath);
        }
    }
}