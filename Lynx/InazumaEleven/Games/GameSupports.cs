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
    public static class EnumHelper
    {
        public static (T Value, string Name)[] GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(value => (Value: value, Name: GetEnumName(value)))
                .ToArray();
        }

        public static string GetEnumName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }

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

        private static Color GetColor<T>(T value) where T : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (ColorAttribute)Attribute.GetCustomAttribute(field, typeof(ColorAttribute));
            return attribute == null ? Color.Black : attribute.Color;
        }
    }

    public class GameSupports
    {
        public class GameFile
        {
            public ARC0 File;
            public string Path;

            public GameFile()
            {

            }

            public GameFile(ARC0 file, string path)
            {
                File = file;
                Path = path;
            }
        }
    }
}
