using System;
using System.IO;
using System.Linq;
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
                        //reader.Skip(0x08);
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
                    else if (flag == 0x71)
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
            using (var stream = new MemoryStream())
            using (var writer = new BinaryDataWriter(stream))
            {
                int subCount = 0;

                writer.BigEndian = true;

                writer.Write((int)0);

                int lengthPos = (int)writer.Position;

                writer.Skip(0x02);

                var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                int tabCount = 0;

                foreach (var line in lines)
                {
                    int currentTabs = line.TakeWhile(c => c == '\t').Count();
                    string trimmedLine = line.Trim();

                    // Adjust tab count based on current context
                    if (currentTabs < tabCount)
                    {
                        for (int i = 0; i < (tabCount - currentTabs); i++)
                        {
                            writer.Write((byte)0x6F); // End block
                            //writer.Write(new byte[8]); // Padding
                            subCount += 1;
                        }
                        tabCount = currentTabs;
                    }

                    if (trimmedLine.StartsWith("if (phase >= "))
                    {
                        writer.Write((byte)0x32); // Start block
                        int phase = int.Parse(trimmedLine.Substring(13).Split(')')[0]);
                        writer.Write(phase);
                        tabCount++;
                        subCount += 2;
                    }
                    else if (trimmedLine.StartsWith("SetPhase("))
                    {
                        string[] parts = trimmedLine.Substring(9).Split(new[] { ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        int setCond = int.Parse(parts[0]);
                        bool isEnabled = parts[1].Trim() == "true";

                        writer.Write((byte)0x35); // Set phase
                        writer.WriteInt24(setCond);
                        writer.Write((short)0x4700); // Reserved bytes
                        writer.Write((short)(isEnabled ? 0x0100 : 0x0000)); // Enabled flag
                        subCount += 2;
                    }
                    else if (trimmedLine == "EXIT();")
                    {
                        writer.Write((byte)0x6E); // Exit
                        //writer.Write(new byte[8]); // Padding
                        subCount += 1;
                    }
                    else if (trimmedLine == "return;")
                    {
                        writer.Write((byte)0x78); // Return
                        //writer.Write(new byte[8]); // Padding
                        subCount += 1;
                    }
                }

                // Handle remaining tabs (close open blocks)
                while (tabCount > 0)
                {
                    writer.Write((byte)0x6F); // End block
                    //writer.Write(new byte[8]); // Padding
                    tabCount--;
                    subCount += 1;
                }

                writer.Seek((uint)lengthPos);
                writer.Write((byte) (writer.Length - 5));
                writer.Write((byte)(subCount));

                // Convert stream to Base64
                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }
}
