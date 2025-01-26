using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Lynx.Tools
{
    /// <summary>
    /// Represents a virtual directory with folders and files.
    /// </summary>
    public class VirtualDirectory
    {
        /// <summary>
        /// The name of the virtual directory.
        /// </summary>
        public string Name;

        /// <summary>
        /// The list of subfolders in the virtual directory.
        /// </summary>
        public List<VirtualDirectory> Folders;

        /// <summary>
        /// The dictionary of files in the virtual directory.
        /// </summary>
        public Dictionary<string, SubMemoryStream> Files;

        /// <summary>
        /// The color associated with the virtual directory.
        /// </summary>
        public Color Color = Color.Black;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDirectory"/> class.
        /// </summary>
        public VirtualDirectory()
        {
            Folders = new List<VirtualDirectory>();
            Files = new Dictionary<string, SubMemoryStream>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDirectory"/> class with a specified name.
        /// </summary>
        /// <param name="name">The name of the virtual directory.</param>
        public VirtualDirectory(string name)
        {
            Name = name;
            Folders = new List<VirtualDirectory>();
            Files = new Dictionary<string, SubMemoryStream>();
        }

        /// <summary>
        /// Gets a subfolder by name.
        /// </summary>
        /// <param name="name">The name of the subfolder.</param>
        /// <returns>The subfolder with the specified name.</returns>
        public VirtualDirectory GetFolder(string name)
        {
            return Folders.FirstOrDefault(folder => folder.Name == name);
        }

        /// <summary>
        /// Gets a subfolder from a full path.
        /// </summary>
        /// <param name="path">The full path of the subfolder.</param>
        /// <returns>The subfolder at the specified path.</returns>
        public VirtualDirectory GetFolderFromFullPath(string path)
        {
            var pathSplit = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var current = this;

            // Get Path
            for (int i = 0; i < pathSplit.Length; i++)
            {
                current = current.GetFolder(pathSplit[i]);

                if (current == null)
                {
                    throw new DirectoryNotFoundException(path + " not exist");
                }
            }

            return current;
        }

        /// <summary>
        /// Checks if a folder exists at the specified path.
        /// </summary>
        /// <param name="path">The path of the folder.</param>
        /// <returns>True if the folder exists, otherwise false.</returns>
        public bool IsFolderExists(string path)
        {
            var pathSplit = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var current = this;

            // Get Path
            for (int i = 0; i < pathSplit.Length; i++)
            {
                current = current.GetFolder(pathSplit[i]);

                if (current == null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a file exists at the specified full path.
        /// </summary>
        /// <param name="path">The full path of the file.</param>
        /// <returns>True if the file exists, otherwise false.</returns>
        public bool IsFullPathExists(string path)
        {
            var pathSplit = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var fileName = pathSplit[pathSplit.Length - 1];
            var current = this;

            // Get Path
            for (int i = 0; i < pathSplit.Length - 1; i++)
            {
                current = current.GetFolder(pathSplit[i]);

                if (current == null)
                {
                    throw new DirectoryNotFoundException(path + " not exist");
                }
            }

            return current.Files.ContainsKey(fileName);
        }

        /// <summary>
        /// Gets all subfolders in the virtual directory.
        /// </summary>
        /// <returns>A list of all subfolders.</returns>
        public List<VirtualDirectory> GetAllFolders()
        {
            List<VirtualDirectory> allFolders = new List<VirtualDirectory>();

            foreach (VirtualDirectory folder in Folders)
            {
                allFolders.Add(folder);
            }

            return allFolders;
        }

        /// <summary>
        /// Gets all subfolders as a dictionary.
        /// </summary>
        /// <returns>A dictionary of all subfolders.</returns>
        public Dictionary<string, VirtualDirectory> GetAllFoldersAsDictionnary()
        {
            var directories = new Dictionary<string, VirtualDirectory> { { Name + "/", this } };
            foreach (var folder in Folders)
            {
                foreach (var subDirectory in folder.GetAllFoldersAsDictionnary())
                {
                    var key = Name + "/" + subDirectory.Key;
                    if (!directories.ContainsKey(key))
                    {
                        directories.Add(key, subDirectory.Value);
                    }
                }
            }

            return directories;
        }

        /// <summary>
        /// Gets a file from the specified full path.
        /// </summary>
        /// <param name="path">The full path of the file.</param>
        /// <returns>The file as a byte array.</returns>
        public byte[] GetFileFromFullPath(string path)
        {
            var pathSplit = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var fileName = pathSplit[pathSplit.Length - 1];
            var current = this;

            // Get Path
            for (int i = 0; i < pathSplit.Length - 1; i++)
            {
                current = current.GetFolder(pathSplit[i]);

                if (current == null)
                {
                    throw new DirectoryNotFoundException(path + " not exist");
                }
            }

            if (current.Files.ContainsKey(fileName))
            {
                SubMemoryStream subMemoryStream = current.Files[fileName];

                // Fill subMemoryStream
                if (subMemoryStream.ByteContent == null)
                {
                    subMemoryStream.Read();
                }

                return subMemoryStream.ByteContent;
            }
            else
            {
                throw new FileNotFoundException(fileName + " not exist");
            }
        }

        /// <summary>
        /// Gets all files in the virtual directory.
        /// </summary>
        /// <returns>A dictionary of all files.</returns>
        public Dictionary<string, SubMemoryStream> GetAllFiles()
        {
            Dictionary<string, SubMemoryStream> allFiles = new Dictionary<string, SubMemoryStream>();

            foreach (KeyValuePair<string, SubMemoryStream> file in Files)
            {
                allFiles.Add(Name + "/" + file.Key, file.Value);
            }

            foreach (VirtualDirectory folder in Folders)
            {
                Dictionary<string, SubMemoryStream> subFiles = folder.GetAllFiles();
                foreach (KeyValuePair<string, SubMemoryStream> file in subFiles)
                {
                    allFiles.Add(Name + "/" + file.Key, file.Value);
                }
            }

            return allFiles;
        }

        /// <summary>
        /// Adds a file to the virtual directory.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <param name="data">The file data as a SubMemoryStream.</param>
        public void AddFile(string name, SubMemoryStream data)
        {
            Files.Add(name, data);
        }

        /// <summary>
        /// Adds a subfolder to the virtual directory.
        /// </summary>
        /// <param name="name">The name of the subfolder.</param>
        public void AddFolder(string name)
        {
            Folders.Add(new VirtualDirectory(name));
        }

        /// <summary>
        /// Adds a subfolder to the virtual directory.
        /// </summary>
        /// <param name="folder">The subfolder to add.</param>
        public void AddFolder(VirtualDirectory folder)
        {
            Folders.Add(folder);
        }

        /// <summary>
        /// Gets the total size of the virtual directory.
        /// </summary>
        /// <returns>The total size in bytes.</returns>
        public long GetSize()
        {
            long size = 0;

            foreach (SubMemoryStream file in Files.Values)
            {
                if (file.ByteContent == null)
                {
                    size += file.Size;
                }
                else
                {
                    size += file.ByteContent.Length;
                }
            }

            foreach (VirtualDirectory folder in Folders)
            {
                size += folder.GetSize();
            }

            return size;
        }

        /// <summary>
        /// Reorganizes the virtual directory.
        /// </summary>
        public void Reorganize()
        {
            // Retrieve all folders and order them by name
            VirtualDirectory[] folders = GetAllFolders().OrderBy(x => x.Name).ToArray();

            // Iterate through each folder path
            foreach (var folderPath in folders)
            {
                // Split the path into individual folder names
                var pathSplit = folderPath.Name.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var currentPath = "";

                // Iterate through each folder name in the path
                foreach (var folderName in pathSplit)
                {
                    // Build the current path
                    currentPath = Path.Combine(currentPath, folderName) + "\\";

                    // Check if the folder doesn't exist and has a valid depth
                    if (currentPath.Count(c => c == '\\') > 1 && GetFolder(currentPath.Replace("\\", "/")) == null)
                    {
                        // Add the folder to the virtual directory
                        AddFolder(currentPath.Replace("\\", "/"));
                    }
                }
            }

            // Reorganize folders with multiple levels
            folders = GetAllFolders().OrderBy(x => x.Name).ToArray();
            var result = new VirtualDirectory("");
            result.Files = Files;

            // Iterate through each folder in ordered folders
            foreach (var folder in folders.Where(x => x.Name != ""))
            {
                // Split the folder name into individual parts
                var path = folder.Name.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var current = result;

                // Traverse through the folder structure
                for (int i = 0; i < path.Length; i++)
                {
                    // Check if the folder doesn't exist
                    if (current.GetFolder(path[i]) == null)
                    {
                        // Create a new folder and assign files
                        VirtualDirectory newFolder = new VirtualDirectory(path[i]);
                        newFolder.Files = folder.Files;
                        current.AddFolder(newFolder);
                    }

                    // Move to the next level in the virtual directory
                    current = current.GetFolder(path[i]);
                }
            }

            // Update the files and folders in the current virtual directory
            Files = result.Files;
            Folders = result.Folders;
        }

        /// <summary>
        /// Sorts the folders and files in the virtual directory alphabetically.
        /// </summary>
        public void SortAlphabetically()
        {
            Folders.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (VirtualDirectory folder in Folders)
            {
                folder.SortAlphabetically();
            }

            var sortedFiles = Files.OrderBy(file => file.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            Files = sortedFiles;
        }

        /// <summary>
        /// Prints the structure of the virtual directory.
        /// </summary>
        public void Print()
        {
            Print(this);
        }

        /// <summary>
        /// Prints the structure of the specified virtual directory.
        /// </summary>
        /// <param name="directory">The virtual directory to print.</param>
        /// <param name="level">The indentation level for printing.</param>
        public void Print(VirtualDirectory directory, int level = 0)
        {
            string indentation = new string('\t', level);
            Console.WriteLine($"{indentation}/{directory.Name}: ");

            foreach (VirtualDirectory subDirectory in directory.Folders)
            {
                Print(subDirectory, level + 1);
            }

            foreach (KeyValuePair<string, SubMemoryStream> files in directory.Files)
            {
                indentation = new string('\t', level + 1);
                Console.WriteLine($"{indentation}{files.Key}");
            }
        }
    }
}

