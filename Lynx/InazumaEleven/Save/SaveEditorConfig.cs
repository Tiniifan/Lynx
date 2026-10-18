using System;
using System.IO;
using System.Globalization;

namespace Lynx.InazumaEleven.Save
{
    /// <summary>
    /// Settings of the save editor, read from SAVE_EDITOR.ini located next to the .exe.
    /// A missing file, key or invalid value falls back to the default.
    /// </summary>
    public static class SaveEditorConfig
    {
        public const string FileName = "SAVE_EDITOR.ini";

        public const int DefaultMaxPlayer = 112;

        public static int MaxPlayer { get; private set; } = Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName));

        public static int Load(string path)
        {
            int maxPlayer = DefaultMaxPlayer;

            try
            {
                if (File.Exists(path))
                {
                    foreach (string rawLine in File.ReadAllLines(path))
                    {
                        string line = rawLine.Trim();

                        if (line.Length == 0 || line[0] == ';' || line[0] == '#' || line[0] == '[')
                        {
                            continue;
                        }

                        int separator = line.IndexOf('=');
                        if (separator < 0)
                        {
                            continue;
                        }

                        string key = line.Substring(0, separator).Trim();
                        string value = line.Substring(separator + 1).Trim();

                        if (key.Equals("MAX_PLAYER", StringComparison.OrdinalIgnoreCase)
                            && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed)
                            && parsed > 0)
                        {
                            maxPlayer = parsed;
                        }
                    }
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }

            MaxPlayer = maxPlayer;
            return maxPlayer;
        }
    }
}
