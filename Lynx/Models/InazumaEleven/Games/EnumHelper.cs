using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Models.InazumaEleven.Common;

namespace Lynx.Models.InazumaEleven.Games
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
}
