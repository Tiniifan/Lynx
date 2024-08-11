using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Level5.Save.Logic;

namespace Lynx.Level5.Save.Games
{
    public interface IGame
    {
        string Code { get; }

        string Name { get; }

        int MaximumPlayer { get; }

        Dictionary<int, Player> Players { get; }

        Dictionary<int, Move> Moves { get; }

        Dictionary<int, Avatar> Avatars { get; }

        Dictionary<int, Item> Items { get; }

        Dictionary<int, Equipment> Equipments { get; set; }

        Dictionary<int, List<PlayRecord>> PlayRecords { get; set; }

        List<Player> Reserve { get; set; }

        Dictionary<int, Player> Auras { get; set; }

        Dictionary<int, Item> Inventory { get; set; }

        SaveInfo SaveInfo { get; set; }

        List<Team> Teams { get; set; }

        void RecruitPlayer(Player player, bool configure);

        Player ChangePlayer(Player selectedPlayer, Player newPlayer, bool keepOldPlayerInformation);

        void NewMixiMax(Player player, int miximaxIndex, int miximaxMove1, int miximaxMove2);

        (int, int, string, bool) Training(Player player, int newStat, int statIndex);

        void UpdateInventory();

        void OpenPlayRecords();

        void OpenSaveInfo();

        void OpenTactics();

        byte[] Save();
    }
}
