using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Lynx.Tools
{
    /// <summary>
    /// Represents a reader for embedded resources.
    /// </summary>
    public class ResourceReader
    {
        /// <summary>
        /// The path of the resource.
        /// </summary>
        public string Path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceReader"/> class.
        /// </summary>
        public ResourceReader()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceReader"/> class with a specified file path.
        /// </summary>
        /// <param name="file">The file path of the resource.</param>
        public ResourceReader(string file)
        {
            this.Path = file;
        }

        /// <summary>
        /// Gets the stream of the embedded resource.
        /// </summary>
        /// <returns>The stream of the embedded resource.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the resource is not found.</exception>
        public Stream GetResourceStream()
        {
            string resourcePath = Path;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            List<string> source = new List<string>(executingAssembly.GetManifestResourceNames());
            resourcePath = source.FirstOrDefault((string r) => r.Contains(resourcePath));

            if (resourcePath == null)
            {
                throw new FileNotFoundException("Resource not found");
            }

            return executingAssembly.GetManifestResourceStream(resourcePath);
        }
    }
}
