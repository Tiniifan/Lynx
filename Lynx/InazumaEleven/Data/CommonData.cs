using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using Lynx.InazumaEleven.Logic;

namespace Lynx.InazumaEleven.Data
{
    /// <summary>
    /// One row of a lookup table stored in InazumaEleven/Common/*.json.
    /// </summary>
    public sealed class CommonEntry
    {
        public int Id { get; }
        public string Name { get; }
        public Color Color { get; }

        public CommonEntry(int id, string name, Color color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }

    /// <summary>
    /// Lookup tables (elements, positions, victory conditions...) loaded from InazumaEleven/Common/*.json
    /// next to the executable, falling back to the copies embedded in it.
    /// </summary>
    public static class CommonData
    {
        public const string Bodies = "Bodies";
        public const string CharaTypes = "CharaTypes";
        public const string Elements = "Elements";
        public const string ExperienceCurve = "ExperienceCurve";
        public const string Genders = "Genders";
        public const string GrowStats = "GrowStats";
        public const string GrowingTimes = "GrowingTimes";
        public const string ItemTypes = "ItemTypes";
        public const string MatchRestrictions = "MatchRestrictions";
        public const string MatchVictoryCondition = "MatchVictoryCondition";
        public const string MovePositions = "MovePositions";
        public const string PalpackConditionTypes = "PalpackConditionTypes";
        public const string PlayerPositions = "PlayerPositions";
        public const string PlayerStats = "PlayerStats";
        public const string Skins = "Skins";
        public const string Years = "Years";

        private const string AvatarGrowthStatsTable = "AvatarGrowthStats";

        /// <summary>
        /// Key under "games" in every JSON file that holds the data of the current game.
        /// </summary>
        public const string DefaultGame = "iego";

        private static readonly Dictionary<string, CommonEntry[]> Tables = new Dictionary<string, CommonEntry[]>();
        private static Dictionary<int, Dictionary<int, AvatarGrowthStat>> avatarGrowthStats;
        private static string game = DefaultGame;

        /// <summary>
        /// The game whose data is read ("iego" for now). Changing it drops the loaded tables.
        /// </summary>
        public static string Game
        {
            get { return game; }
            set
            {
                lock (Tables)
                {
                    game = value;
                    Tables.Clear();
                    avatarGrowthStats = null;
                }
            }
        }

        /// <summary>
        /// Gets the rows of a table, in file order.
        /// </summary>
        public static CommonEntry[] Get(string table)
        {
            lock (Tables)
            {
                if (!Tables.TryGetValue(table, out CommonEntry[] entries))
                {
                    entries = ReadTable(table);
                    Tables[table] = entries;
                }

                return entries;
            }
        }

        /// <summary>
        /// Gets the display names of a table, in file order (ready for a ComboBox).
        /// </summary>
        public static string[] GetNames(string table)
        {
            return Get(table).Select(x => x.Name).ToArray();
        }

        /// <summary>
        /// Gets the name of an id, or the id itself when the table has no such row.
        /// </summary>
        public static string GetName(string table, int id)
        {
            CommonEntry entry = Get(table).FirstOrDefault(x => x.Id == id);
            return entry != null ? entry.Name : id.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the color of an id (Skins), or black when the table has no such row.
        /// </summary>
        public static Color GetColor(string table, int id)
        {
            CommonEntry entry = Get(table).FirstOrDefault(x => x.Id == id);
            return entry != null ? entry.Color : Color.Black;
        }

        /// <summary>
        /// Avatar growth tables, indexed by stat growth then by level growth.
        /// </summary>
        public static Dictionary<int, Dictionary<int, AvatarGrowthStat>> AvatarGrowthStats
        {
            get
            {
                lock (Tables)
                {
                    if (avatarGrowthStats == null) avatarGrowthStats = ReadAvatarGrowthStats();
                    return avatarGrowthStats;
                }
            }
        }

        private static CommonEntry[] ReadTable(string table)
        {
            using (JsonDocument document = JsonDocument.Parse(ReadResource(table)))
            {
                return GameRoot(document).EnumerateArray().Select(row =>
                {
                    Color color = Color.Black;
                    if (row.TryGetProperty("color", out JsonElement colorElement))
                    {
                        string hex = colorElement.GetString().TrimStart('#');
                        color = Color.FromArgb(int.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture) | unchecked((int)0xFF000000));
                    }

                    return new CommonEntry(row.GetProperty("id").GetInt32(), row.GetProperty("name").GetString(), color);
                }).ToArray();
            }
        }

        private static Dictionary<int, Dictionary<int, AvatarGrowthStat>> ReadAvatarGrowthStats()
        {
            var result = new Dictionary<int, Dictionary<int, AvatarGrowthStat>>();

            using (JsonDocument document = JsonDocument.Parse(ReadResource(AvatarGrowthStatsTable)))
            {
                foreach (JsonElement growth in GameRoot(document).EnumerateArray())
                {
                    var levels = new Dictionary<int, AvatarGrowthStat>();
                    foreach (JsonElement level in growth.GetProperty("levels").EnumerateArray())
                    {
                        levels[level.GetProperty("level").GetInt32()] = new AvatarGrowthStat(
                            ReadIntList(level.GetProperty("fg")),
                            ReadIntList(level.GetProperty("attack")));
                    }

                    result[growth.GetProperty("statGrowth").GetInt32()] = levels;
                }
            }

            return result;
        }

        private static JsonElement GameRoot(JsonDocument document)
        {
            JsonElement games = document.RootElement.GetProperty("games");
            if (!games.TryGetProperty(game, out JsonElement data))
                throw new KeyNotFoundException("No data for game '" + game + "'.");
            return data;
        }

        private static List<int> ReadIntList(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null) return null;
            return element.EnumerateArray().Select(x => x.GetInt32()).ToList();
        }

        private static string ReadResource(string table)
        {
            // The copy next to the executable wins, so the user can edit the tables without rebuilding
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InazumaEleven", "Common", table + ".json");
            if (File.Exists(path)) return File.ReadAllText(path);

            string name = "Lynx.InazumaEleven.Common." + table + ".json";

            using (Stream stream = typeof(CommonData).Assembly.GetManifestResourceStream(name))
            {
                if (stream == null) throw new FileNotFoundException("Common table not found: " + path);
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
