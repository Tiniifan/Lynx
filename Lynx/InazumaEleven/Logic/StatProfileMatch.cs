namespace Lynx.InazumaEleven.Logic
{
    /// <summary>
    /// One candidate answer of <see cref="IStatCalculator.RankProfiles"/>: the base stats and
    /// growth curves that get closest to the wanted stats with one given growth profile.
    /// Every array holds ten entries, indexed by <see cref="Common.PlayerStats"/>.
    /// </summary>
    public class StatProfileMatch
    {
        /// <summary>The growth profile this match uses.</summary>
        public int Profile { get; set; }

        /// <summary>Base stats to write in chara_param.</summary>
        public int[] BaseStats { get; set; }

        /// <summary>Growth curves to write in chara_param.</summary>
        public int[] Grows { get; set; }

        /// <summary>Stats those values actually produce at the requested level.</summary>
        public int[] Values { get; set; }

        /// <summary>Stats that were asked for.</summary>
        public int[] Targets { get; set; }

        /// <summary>Sum of the absolute gaps between <see cref="Values"/> and <see cref="Targets"/>.</summary>
        public int TotalDeviation { get; set; }

        /// <summary>Sum of the squared gaps, used to break ties between equally close profiles.</summary>
        public int SquaredDeviation { get; set; }

        /// <summary>
        /// Sum of the base stats the match needs. Most targets are reachable with every profile,
        /// so this is what actually orders them: the lower it is, the more of the wanted stats the
        /// profile carries through growth instead of raw base, which is the more natural fit.
        /// </summary>
        public int BaseStatTotal { get; set; }

        /// <summary>Gap on one stat, negative when the result falls short of the target.</summary>
        public int GetDeviation(int stat)
        {
            return Values[stat] - Targets[stat];
        }
    }
}
