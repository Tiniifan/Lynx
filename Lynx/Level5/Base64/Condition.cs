using System;
using System.Text;
using Lynx.Tools;

namespace Lynx.Level5.Base64
{
    public static class Condition
    {
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
    }
}
