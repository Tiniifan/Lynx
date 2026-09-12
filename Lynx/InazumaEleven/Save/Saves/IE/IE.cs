using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using StudioElevenLib.Tools;
using Lynx.InazumaEleven.Save.Saves;
using Lynx.InazumaEleven.Save.Logic;
using Lynx.InazumaEleven.Save.Games;
using Lynx.InazumaEleven.Save.Games.IEGO;
using Lynx.InazumaEleven.Save.Games.IEGOCS;
using Lynx.InazumaEleven.Save.Games.IEGOGalaxy;
using StudioElevenLib.Level5.Compression.XORShift;

namespace Lynx.InazumaEleven.Save.Saves.IE
{
    public class IE : ISave
    {
        public string Name => "Inazuma Eleven";

        public string Extension => ".ie";

        public string Description => "Inazuma Eleven GO CS/Galaxy Format by Level 5 (Go == .ie4)";

        public IGame Game { get; set; }

        public void Open(BinaryDataReader reader, Dictionary<int, InazumaEleven.Save.Logic.Player> players, Dictionary<int, InazumaEleven.Save.Logic.Move> moves, Dictionary<int, InazumaEleven.Save.Logic.Avatar> avatars)
        {
            reader = new BinaryDataReader(reader.GetSection(0,(int)reader.Length));
            reader.Skip(4);
            ushort header = reader.ReadValue<ushort>();

            if (header != 0x2CF1 & header != 0x4CF1 & header != 0x40F1 & header != 0x6CF1 & header != 0xC4F1)
            {
                reader = new BinaryDataReader(new XORShift().Decompress(reader.GetSection(0, (int)reader.Length)));
                reader.Skip(4);
                header = reader.ReadValue<ushort>();
            }

            switch (header)
            {
                case 0x2CF1:
                    Game = new GO(reader.BaseStream, players, moves, avatars, false);
                    break;
                case 0x6CF1:
                    Game = new GO(reader.BaseStream, players, moves, avatars, true);
                    break;
                case 0x4CF1:
                    Game = new CS(reader.BaseStream, players, moves, false);
                    break;
                case 0xC4F1:
                    Game = new CS(reader.BaseStream, players, moves, true);
                    break;
                case 0x40F1:
                    Game = new Galaxy(reader.BaseStream, players, moves);
                    break;
                default:
                    throw new FormatException("Save Game not supported");
            }
        }

        public void Save(OpenFileDialog initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = Path.GetFileName(initialDirectory.FileName);

            if (Game.Code == "IEGO")
            {
                saveFileDialog.Title = "Save IEGO save file";
                saveFileDialog.Filter = "IE files|*.ie4|IE files decrypted|*.ie4";
            } else if (Game.Code == "IEGOCS")
            {
                saveFileDialog.Title = "Save IEGOCS save file";
                saveFileDialog.Filter = "IE files|*.ie|IE files decrypted|*.ie";
            } else
            {
                saveFileDialog.Title = "Save IEGOGalaxy save file";
                saveFileDialog.Filter = "IE files|*.ie|IE files decrypted|*.ie";
            }

            saveFileDialog.InitialDirectory = initialDirectory.InitialDirectory;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string saveFileName = saveFileDialog.FileName;
                byte[] saveData = Game.Save();

                if (saveFileDialog.FilterIndex == 1)
                {
                    saveData = new XORShift().Compress(saveData);
                }

                File.WriteAllBytes(saveFileName, saveData);
                MessageBox.Show("Saved!");
            }
        }
    }
}
