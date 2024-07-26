using System.Collections.Generic;

namespace Lynx.InazumaEleven.Logic
{
    public class Player
    {
        public ICharaparam Charaparam { get; set; }
        public List<ISkillTable> SpecialMoves { get; set; }

        public Player(ICharaparam charaparam)
        {
            Charaparam = charaparam;
        }

        public Player(ICharaparam charaparam, List<ISkillTable> specialMoves)
        {
            Charaparam = charaparam;
            SpecialMoves = specialMoves;
        }
    }
}
