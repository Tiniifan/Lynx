using Lynx.InazumaEleven.Common;

namespace Lynx.InazumaEleven.Logic
{
    /// <summary>
    /// Converts between the raw values stored in chara_param (base stat + growth curve +
    /// growth profile) and the stat a player actually shows in game at a given level.
    /// </summary>
    public interface IStatCalculator
    {
        /// <summary>Lowest level the game can represent (the level is stored on one byte).</summary>
        int MinLevel { get; }

        /// <summary>Highest level the game can represent.</summary>
        int MaxLevel { get; }

        /// <summary>Level a player legally caps at.</summary>
        int MaxLegalLevel { get; }

        /// <summary>Lowest value a base stat can take (base stats are read as one byte).</summary>
        int MinBaseStat { get; }

        /// <summary>Highest value a base stat can take.</summary>
        int MaxBaseStat { get; }

        /// <summary>Number of growth curves the game implements.</summary>
        int GrowCount { get; }

        /// <summary>Number of growth profiles (the field chara_param stores next to Freedom).</summary>
        int ProfileCount { get; }

        /// <summary>Raw growth values that make sense for that stat, in the order they should be listed.</summary>
        int[] GetCurves(PlayerStats stat);

        /// <summary>Readable name of a growth curve, e.g. "Late ++".</summary>
        string GetCurveName(PlayerStats stat, int grow);

        /// <summary>Readable name of a growth profile, e.g. "Goalkeeper Profile 1".</summary>
        string GetProfileName(int profile);

        /// <summary>Multiplier a profile applies to the growth of one stat.</summary>
        float GetProfileMultiplier(PlayerStats stat, int profile);

        /// <summary>
        /// Computes the stat a player shows at <paramref name="level"/>, equipment and
        /// invested training points excluded.
        /// </summary>
        int GetStat(PlayerStats stat, int baseStat, int grow, int level, int element, int profile);

        /// <summary>
        /// Lowest and highest value <paramref name="stat"/> can reach at <paramref name="level"/>,
        /// over every valid base stat and every growth curve, for that one profile.
        /// </summary>
        void GetReachableRange(PlayerStats stat, int level, int element, int profile, out int minimum, out int maximum);

        /// <summary>
        /// Same range, widened over every profile as well: what the stat can reach if the profile
        /// is still free to change.
        /// </summary>
        void GetReachableRange(PlayerStats stat, int level, int element, out int minimum, out int maximum);

        /// <summary>
        /// Finds a base stat and a growth curve that make <paramref name="stat"/> land exactly on
        /// <paramref name="value"/> at <paramref name="level"/>.
        /// <paramref name="preferredGrow"/> is tried first so an edit keeps the player's growth
        /// profile whenever it can.
        /// </summary>
        /// <returns><c>false</c> when no combination reaches that value; the out parameters then hold the defaults.</returns>
        bool TryGetBaseAndGrow(PlayerStats stat, int value, int level, int element, int profile,
                               int preferredGrow, out int baseStat, out int grow);

        /// <summary>
        /// Builds, for every growth profile, the best set of base stats and growth curves matching
        /// <paramref name="targets"/> at <paramref name="level"/>, and returns them closest first.
        /// <paramref name="preferredGrows"/> wins every tie so the player's growth shape survives
        /// when it can.
        /// </summary>
        StatProfileMatch[] RankProfiles(int[] targets, int[] preferredGrows, int level, int element);
    }
}
