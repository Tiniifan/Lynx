using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using Lynx.Level5.Archive.ARC0;
using Lynx.InazumaEleven.Common;

namespace Lynx.InazumaEleven.Games
{
    /// <summary>
    /// Provides helper methods for working with enums.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the values and names of the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>An array of tuples containing the enum values and names.</returns>
        public static (T Value, string Name)[] GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(value => (Value: value, Name: GetEnumName(value)))
                .ToArray();
        }

        /// <summary>
        /// Gets the name of the specified enum value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The name of the enum value.</returns>
        public static string GetEnumName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Gets the color associated with the specified enum value ID.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="id">The ID of the enum value.</param>
        /// <returns>The color associated with the enum value.</returns>
        /// <exception cref="ArgumentException">Thrown when no enum value with the specified ID is found.</exception>
        public static Color GetColorById<T>(int id) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>();

            foreach (var value in values)
            {
                if ((int)(object)value == id)
                {
                    return GetColor(value);
                }
            }

            throw new ArgumentException($"No enum value with id {id} found.");
        }

        /// <summary>
        /// Gets the color associated with the specified enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The color associated with the enum value.</returns>
        private static Color GetColor<T>(T value) where T : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (ColorAttribute)Attribute.GetCustomAttribute(field, typeof(ColorAttribute));
            return attribute == null ? Color.Black : attribute.Color;
        }
    }

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
