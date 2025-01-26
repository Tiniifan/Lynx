using System;
using System.IO;
using System.Text;
using Lynx.Tools;

namespace Lynx.Level5.Base64
{
    public static class Condition
    {
        /// <summary>
        /// Converts a Base64 string to a readable representation.
        /// </summary>
        /// <param name="base64String">The Base64 string to convert.</param>
        /// <returns>A readable string representing the conditions.</returns>
        public static string ToString(string base64String)
        {
            StringBuilder output = new StringBuilder();

            byte[] byteArray = Convert.FromBase64String(base64String);

            using (var reader = new BinaryDataReader(byteArray))
            {
                reader.BigEndian = true;

                reader.Seek(0x06);
                int tabCount = 0;

                while (reader.Position < reader.Length)
                {
                    byte flag = reader.ReadValue<byte>();

                    if (flag == 0x32)
                    {
                        output.Append($"if (phase >= {reader.ReadValue<int>()}) {{{Environment.NewLine}");
                        tabCount++;
                    }
                    else if (flag == 0x35)
                    {
                        int setCond = reader.ReadInt24();
                        reader.Skip(0x02);
                        bool setCondEnabled = reader.ReadValue<short>() == 0x0100;

                        if (setCondEnabled)
                        {
                            output.Append($"{new string('\t', tabCount)}SetPhase({setCond}, true);{Environment.NewLine}");
                        }
                        else
                        {
                            output.Append($"{new string('\t', tabCount)}SetPhase({setCond}, false);{Environment.NewLine}");
                        }
                    }
                    else if (flag == 0x6F)
                    {
                        tabCount--;
                        output.Append($"{new string('\t', tabCount)}}}{Environment.NewLine}");
                        reader.Skip(0x08);
                    }
                    else if (flag == 0x6E)
                    {
                        output.Append($"{new string('\t', tabCount)}EXIT();{Environment.NewLine}}}{Environment.NewLine}");
                        tabCount--;
                        break;
                    }
                    else if (flag == 0x78)
                    {
                        output.Append($"{new string('\t', tabCount)}return;{Environment.NewLine}}}{Environment.NewLine}");
                        tabCount--;
                        break;
                    }
                }

                return output.ToString();
            }
        }


        /// <summary>
        /// Converts a readable string to a Base64 string.
        /// </summary>
        /// <param name="input">The readable string to convert.</param>
        /// <returns>A Base64 string representing the conditions.</returns>
        public static string ToBase64String(string input)
        {
            StringBuilder output = new StringBuilder();
            byte[] byteArray;

            using (MemoryStream memoryStream = new MemoryStream())
            using (var writer = new BinaryDataWriter(memoryStream))
            {
                writer.BigEndian = true;

                string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.StartsWith("if (phase >= "))
                    {
                        writer.Write((byte)0x32);
                        int phase = int.Parse(line.Substring(13, line.IndexOf(')') - 13));
                        writer.Write(phase);
                    }
                    else if (line.Contains("SetPhase("))
                    {
                        writer.Write((byte)0x35);
                        int setCond = int.Parse(line.Substring(line.IndexOf('(') + 1, line.IndexOf(',') - line.IndexOf('(') - 1));
                        writer.WriteInt24(setCond);
                        writer.Write((short)(line.Contains("true") ? 0x0100 : 0x0000));
                    }
                    else if (line.Contains("EXIT();"))
                    {
                        writer.Write((byte)0x6E);
                        writer.Write(new byte[8]);
                    }
                    else if (line.Contains("return;"))
                    {
                        writer.Write((byte)0x78);
                        writer.Write(new byte[8]);
                    }
                    else if (line.Contains("}"))
                    {
                        writer.Write((byte)0x6F);
                        writer.Write(new byte[8]);
                    }
                }

                byteArray = memoryStream.ToArray();
            }

            return Convert.ToBase64String(byteArray);
        }
    }
}
