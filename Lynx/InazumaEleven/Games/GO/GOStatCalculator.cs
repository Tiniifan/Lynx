using System;
using System.Collections.Generic;
using Lynx.InazumaEleven.Common;
using Lynx.InazumaEleven.Logic;

namespace Lynx.InazumaEleven.Games.GO
{
    /// <summary>
    /// Inazuma Eleven GO stat calculator.
    ///
    /// Port of the game function at 0x0033E404, which fills the ten stats of a player
    /// (FP, TP, Kick, Dribble, Technique, Block, Speed, Stamina, Catch, Luck) from its
    /// chara_param record and its level:
    ///
    ///     A = ProfileTable[GrowthProfile - 1][stat]   (0x00586C18, 10 rows of 10 floats)
    ///     B = ElementTable[Element       - 1][stat]   (0x00586DA8,  4 rows of 10 floats)
    ///     L = level, base = base stat
    ///
    ///     grow 1 (and grow 0, and any unknown value) :
    ///         base + ((B*(L/12) + L*0.9) * 0.05 + L*0.5) * A
    ///     grow 2 :
    ///         base + ((B*(L/6)  + L*1.2) * 0.05 + L*0.5) * A
    ///     grow 3 :
    ///         base + ((L + L*0.02)     + B*0.25)    * ((455-2L)*0.30303) * 0.013158 * 0.45 * A
    ///     grow 4 :
    ///         base + ((L + L*0.028571) + B*0.55556) * ((250-L)*0.33333)  * 0.016129 * 0.55 * A
    ///     grow 5 :
    ///         base + ((B*(L*0.0034483) + (L*4.5)*0.10526) * 0.65 * L / (145-L)) * A
    ///     grow 6 :
    ///         base + ((B*(L*0.0095238) + (L*5.5)*0.10526) * 0.55 * L / (144-L)) * A
    ///     grow 7 : base - 0.0146*L^2 + 2.7831*L - 2.7685      (A and B unused)
    ///     grow 8 : base - 0.0084*L^2 + 1.8554*L - 1.8470      (A and B unused)
    ///     grow 9 : base - 0.0083*L^2 + 1.5461*L - 1.5378      (A and B unused)
    ///
    /// Every step runs in 32-bit float, then VCVT.U32.F32 truncates towards zero and
    /// saturates negatives to 0, and the result is stored on 16 bits. The arithmetic is
    /// reproduced operation by operation because the game's rounding is visible in the
    /// final integer.
    ///
    /// What this returns is the stat the level alone gives. What the game displays adds
    /// the equipment bonuses and the invested training points on top (see 0x00490484).
    ///
    /// Curves 0 to 6 shape the eight field stats, curves 7 to 9 are the FP/TP ones. Curves
    /// 0 and 1 are the same branch of the switch, so they behave identically.
    /// </summary>
    public class GOStatCalculator : IStatCalculator
    {
        public static readonly GOStatCalculator Instance = new GOStatCalculator();

        public int MinLevel => 0;

        public int MaxLevel => 255;

        public int MaxLegalLevel => 99;

        public int MinBaseStat => 0;

        public int MaxBaseStat => 255;

        public int GrowCount => 10;

        public int ProfileCount => ProfileTable.Length;

        /// <summary>Per stat multiplier, picked by the growth profile (0x00586C18).</summary>
        private static readonly float[][] ProfileTable =
        {
            new[] { 1.0f, 1.0f, 0.7f, 1.0f, 1.0f, 0.9f, 1.1f, 1.1f, 1.2f, 1.0f },
            new[] { 1.0f, 1.0f, 0.7f, 0.9f, 1.0f, 1.0f, 0.9f, 1.2f, 1.3f, 1.0f },
            new[] { 1.0f, 1.0f, 0.8f, 0.9f, 1.0f, 1.3f, 1.0f, 1.3f, 0.7f, 1.0f },
            new[] { 1.0f, 1.0f, 0.9f, 1.0f, 1.0f, 1.1f, 1.1f, 1.2f, 0.7f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.3f, 1.1f, 0.9f, 1.0f, 1.0f, 0.7f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.1f, 1.2f, 1.1f, 0.9f, 0.7f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.1f, 1.0f, 0.9f, 1.2f, 1.1f, 0.7f, 1.0f },
            new[] { 1.0f, 1.0f, 1.3f, 0.9f, 1.0f, 1.1f, 0.9f, 1.0f, 0.7f, 1.1f },
            new[] { 1.0f, 1.0f, 1.0f, 1.1f, 1.0f, 0.9f, 1.1f, 1.2f, 0.7f, 1.0f },
            new[] { 1.0f, 1.0f, 1.1f, 0.9f, 1.0f, 1.0f, 1.2f, 1.1f, 0.7f, 1.0f },
        };

        /// <summary>Per stat factor inside the growth curve, picked by Element (0x00586DA8).</summary>
        private static readonly float[][] ElementTable =
        {
            new[] { 1.0f, 1.0f,  2.0f, 12.0f, 12.0f,  1.0f, 12.0f,  3.0f,  4.0f, 1.0f },
            new[] { 1.0f, 1.0f,  1.0f, 12.0f,  1.0f, 12.0f, 12.0f,  1.0f,  1.0f, 1.0f },
            new[] { 1.0f, 1.0f, 12.0f,  1.0f, 12.0f,  1.0f, 12.0f,  1.0f,  1.0f, 1.0f },
            new[] { 1.0f, 1.0f,  2.0f,  1.0f,  2.0f,  3.0f,  1.0f, 12.0f, 12.0f, 1.0f },
        };

        /// <summary>Curves offered for the eight field stats, in the order they are listed.</summary>
        private static readonly int[] FieldCurves = { 3, 4, 1, 2, 5, 6 };

        /// <summary>Curves offered for FP and TP. Curve 6 is there because chara_param uses it once.</summary>
        private static readonly int[] PointCurves = { 9, 8, 7, 6 };

        /// <summary>
        /// Curve names, indexed by raw value. A field stat never lists 7 to 9 and FP/TP never
        /// list 3 to 5, so the reused "Early" names stay unambiguous inside a single list.
        /// </summary>
        private static readonly string[] CurveNames =
        {
            /* 0 */ "Linear +",   // same branch of the switch as curve 1
            /* 1 */ "Linear +",
            /* 2 */ "Linear ++",
            /* 3 */ "Early +",
            /* 4 */ "Early ++",
            /* 5 */ "Late +",
            /* 6 */ "Late ++",
            /* 7 */ "Early +++",
            /* 8 */ "Early ++",
            /* 9 */ "Early +",
        };

        /// <summary>The stat that defines each position, used to name the profiles.</summary>
        private static readonly PlayerStats[] PositionStats =
        {
            PlayerStats.Catch,      // goalkeeper
            PlayerStats.Block,      // defender
            PlayerStats.Dribble,    // midfielder
            PlayerStats.Kick,       // forward
        };

        private static readonly string[] PositionNames = { "Goalkeeper", "Defender", "Midfielder", "Forward" };

        private static readonly string[] ProfileNames = BuildProfileNames();

        // Float constants read straight out of the binary (0x0033E7E0..0x0033E854). The
        // decimals IDA prints are rounded, these are the exact single precision values.
        private const float OneTwelfth = 0.0833333358168602f;
        private const float ZeroPointNine = 0.8999999761581421f;
        private const float ZeroPointZeroFive = 0.05000000074505806f;
        private const float Half = 0.5f;
        private const float OneSixth = 0.1666666716337204f;
        private const float OnePointTwo = 1.2000000476837158f;
        private const float ZeroPointZeroTwo = 0.019999999552965164f;
        private const float Quarter = 0.25f;
        private const float FourFiftyFive = 455.0f;
        private const float TenOverThirtyThree = 0.3030303120613098f;
        private const float OneOverSeventySix = 0.01315789483487606f;
        private const float ZeroPointFourFive = 0.44999998807907104f;
        private const float OneOverThirtyFive = 0.02857142873108387f;
        private const float FiveOverNine = 0.5555555820465088f;
        private const float TwoFifty = 250.0f;
        private const float OneThird = 0.3333333432674408f;
        private const float OneOverSixtyTwo = 0.016129031777381897f;
        private const float ZeroPointFiveFive = 0.550000011920929f;
        private const float OneOverTwoNinety = 0.003448275849223137f;
        private const float FourPointFive = 4.5f;
        private const float TwoOverNineteen = 0.10526315867900848f;
        private const float ZeroPointSixFive = 0.6499999761581421f;
        private const float OneFortyFive = 145.0f;
        private const float OneOverOneOhFive = 0.009523809887468815f;
        private const float FivePointFive = 5.5f;
        private const float OneFortyFour = 144.0f;

        /// <summary>
        /// Names each profile after the position its strongest multiplier serves, numbered in
        /// profile order inside that position.
        /// </summary>
        private static string[] BuildProfileNames()
        {
            string[] names = new string[ProfileTable.Length];
            int[] used = new int[PositionStats.Length];

            for (int profile = 0; profile < ProfileTable.Length; profile++)
            {
                int best = 0;

                for (int position = 1; position < PositionStats.Length; position++)
                {
                    if (ProfileTable[profile][(int)PositionStats[position]] > ProfileTable[profile][(int)PositionStats[best]])
                    {
                        best = position;
                    }
                }

                names[profile] = PositionNames[best] + " Profile " + (++used[best]);
            }

            return names;
        }

        public int[] GetCurves(PlayerStats stat)
        {
            return IsPointStat(stat) ? (int[])PointCurves.Clone() : (int[])FieldCurves.Clone();
        }

        public string GetCurveName(PlayerStats stat, int grow)
        {
            return grow >= 0 && grow < CurveNames.Length ? CurveNames[grow] : "Curve " + grow;
        }

        public string GetProfileName(int profile)
        {
            int index = profile - 1;

            return index >= 0 && index < ProfileNames.Length ? ProfileNames[index] : "Profile " + profile;
        }

        public float GetProfileMultiplier(PlayerStats stat, int profile)
        {
            return ProfileTable[Clamp(profile - 1, 0, ProfileTable.Length - 1)][(int)stat];
        }

        public int GetStat(PlayerStats stat, int baseStat, int grow, int level, int element, int profile)
        {
            int index = (int)stat;
            int lv = level & 0xFF;
            float b = baseStat & 0xFF;

            if (grow >= 7 && grow <= 9)
            {
                float square, linear, offset;

                switch (grow)
                {
                    case 7:
                        square = -0.014600000344216824f;
                        linear = 2.783099889755249f;
                        offset = 2.7685000896453857f;
                        break;
                    case 8:
                        square = -0.008399999700486660f;
                        linear = 1.8554000854492188f;
                        offset = 1.8470000028610230f;
                        break;
                    default:
                        square = -0.008299999870359898f;
                        linear = 1.5461000204086304f;
                        offset = 1.5377999544143677f;
                        break;
                }

                // the level is squared as an integer (sub_157264(level, 2))
                float squared = lv * lv;
                float withSquare = b + squared * square;
                float withLinear = withSquare + lv * linear;
                return Saturate(withLinear - offset);
            }

            float a = ProfileTable[Clamp(profile - 1, 0, ProfileTable.Length - 1)][index];
            float e = ElementTable[Clamp(element - 1, 0, ElementTable.Length - 1)][index];

            return Saturate(b + GrowthDelta(grow, lv, a, e));
        }

        public void GetReachableRange(PlayerStats stat, int level, int element, int profile,
                                      out int minimum, out int maximum)
        {
            minimum = int.MaxValue;
            maximum = int.MinValue;

            foreach (int grow in GetCurves(stat))
            {
                int lowest = GetStat(stat, MinBaseStat, grow, level, element, profile);
                int highest = GetStat(stat, MaxBaseStat, grow, level, element, profile);

                if (lowest < minimum) minimum = lowest;
                if (highest > maximum) maximum = highest;
            }
        }

        public void GetReachableRange(PlayerStats stat, int level, int element, out int minimum, out int maximum)
        {
            minimum = int.MaxValue;
            maximum = int.MinValue;

            for (int profile = 1; profile <= ProfileCount; profile++)
            {
                GetReachableRange(stat, level, element, profile, out int lowest, out int highest);

                if (lowest < minimum) minimum = lowest;
                if (highest > maximum) maximum = highest;
            }
        }

        public bool TryGetBaseAndGrow(PlayerStats stat, int value, int level, int element, int profile,
                                      int preferredGrow, out int baseStat, out int grow)
        {
            if (value >= 0)
            {
                foreach (int candidate in CurvesToTry(stat, preferredGrow))
                {
                    int found = ClosestBase(stat, value, level, element, profile, candidate, out int reached);

                    if (reached == value)
                    {
                        baseStat = found;
                        grow = candidate;
                        return true;
                    }
                }
            }

            baseStat = 0;
            grow = 0;
            return false;
        }

        public StatProfileMatch[] RankProfiles(int[] targets, int[] preferredGrows, int level, int element)
        {
            List<StatProfileMatch> matches = new List<StatProfileMatch>();

            for (int profile = 1; profile <= ProfileCount; profile++)
            {
                StatProfileMatch match = new StatProfileMatch
                {
                    Profile = profile,
                    BaseStats = new int[targets.Length],
                    Grows = new int[targets.Length],
                    Values = new int[targets.Length],
                    Targets = (int[])targets.Clone(),
                };

                for (int index = 0; index < targets.Length; index++)
                {
                    PlayerStats stat = (PlayerStats)index;
                    int preferred = preferredGrows != null && index < preferredGrows.Length ? preferredGrows[index] : -1;

                    int bestBase = 0;
                    int bestGrow = 0;
                    int bestValue = 0;
                    int bestGap = int.MaxValue;

                    foreach (int candidate in CurvesToTry(stat, preferred))
                    {
                        int found = ClosestBase(stat, targets[index], level, element, profile, candidate, out int reached);
                        int gap = Math.Abs(reached - targets[index]);

                        // strict comparison, so the preferred curve comes first and keeps its tie
                        if (gap < bestGap)
                        {
                            bestGap = gap;
                            bestBase = found;
                            bestGrow = candidate;
                            bestValue = reached;

                            if (gap == 0) break;
                        }
                    }

                    match.BaseStats[index] = bestBase;
                    match.Grows[index] = bestGrow;
                    match.Values[index] = bestValue;
                    match.TotalDeviation += bestGap;
                    match.SquaredDeviation += bestGap * bestGap;
                    match.BaseStatTotal += bestBase;
                }

                matches.Add(match);
            }

            matches.Sort((left, right) =>
            {
                int order = left.TotalDeviation.CompareTo(right.TotalDeviation);
                if (order != 0) return order;

                order = left.SquaredDeviation.CompareTo(right.SquaredDeviation);
                if (order != 0) return order;

                // almost every target is reachable with all ten profiles, so this is the
                // comparison that really ranks them
                order = left.BaseStatTotal.CompareTo(right.BaseStatTotal);
                if (order != 0) return order;

                return left.Profile.CompareTo(right.Profile);
            });

            return matches.ToArray();
        }

        /// <summary>The curves worth trying for a stat, the preferred one first when it is valid.</summary>
        private IEnumerable<int> CurvesToTry(PlayerStats stat, int preferredGrow)
        {
            int[] curves = IsPointStat(stat) ? PointCurves : FieldCurves;

            if (preferredGrow >= 0 && preferredGrow < GrowCount)
            {
                yield return preferredGrow;
            }

            foreach (int curve in curves)
            {
                if (curve != preferredGrow) yield return curve;
            }
        }

        /// <summary>
        /// Base stat that gets closest to <paramref name="value"/> with that one growth curve.
        /// The stat moves by exactly one unit per unit of base stat, so a single probe points
        /// straight at the answer and a small window around it absorbs the float rounding.
        /// </summary>
        private int ClosestBase(PlayerStats stat, int value, int level, int element, int profile,
                                int grow, out int reached)
        {
            int probe = (MinBaseStat + MaxBaseStat) / 2;
            int atProbe = GetStat(stat, probe, grow, level, element, profile);
            int guess = Clamp(probe + (value - atProbe), MinBaseStat, MaxBaseStat);

            int bestBase = guess;
            int bestValue = GetStat(stat, guess, grow, level, element, profile);
            int bestGap = Math.Abs(bestValue - value);

            for (int candidate = guess - 2; candidate <= guess + 2; candidate++)
            {
                if (candidate < MinBaseStat || candidate > MaxBaseStat || candidate == guess) continue;

                int current = GetStat(stat, candidate, grow, level, element, profile);
                int gap = Math.Abs(current - value);

                if (gap < bestGap)
                {
                    bestGap = gap;
                    bestBase = candidate;
                    bestValue = current;
                }
            }

            reached = bestValue;
            return bestBase;
        }

        /// <summary>
        /// The amount the level adds to the base stat, growth curve by growth curve.
        /// Each intermediate is kept in a float local so the rounding matches the VFP code.
        /// </summary>
        private static float GrowthDelta(int grow, int level, float a, float b)
        {
            float l = level;
            float t;

            switch (grow)
            {
                case 3:
                    {
                        t = l + l * ZeroPointZeroTwo;
                        t = t + b * Quarter;
                        float span = FourFiftyFive - l * 2.0f;
                        span = span * TenOverThirtyThree;
                        t = t * span;
                        t = t * OneOverSeventySix;
                        t = t * ZeroPointFourFive;
                        break;
                    }

                case 4:
                    {
                        t = l + l * OneOverThirtyFive;
                        t = t + b * FiveOverNine;
                        float span = TwoFifty - l;
                        span = span * OneThird;
                        t = t * span;
                        t = t * OneOverSixtyTwo;
                        t = t * ZeroPointFiveFive;
                        break;
                    }

                case 5:
                    t = b * (l * OneOverTwoNinety);
                    t = t + l * FourPointFive * TwoOverNineteen;
                    t = t * ZeroPointSixFive;
                    t = t * l;
                    t = t / (OneFortyFive - l);
                    break;

                case 6:
                    t = b * (l * OneOverOneOhFive);
                    t = t + l * FivePointFive * TwoOverNineteen;
                    t = t * ZeroPointFiveFive;
                    t = t * l;
                    t = t / (OneFortyFour - l);
                    break;

                case 2:
                    t = b * (l * OneSixth);
                    t = t + l * OnePointTwo;
                    t = t * ZeroPointZeroFive;
                    t = t + l * Half;
                    break;

                // grow 1, grow 0 and anything the game does not know fall on the same branch
                default:
                    t = b * (l * OneTwelfth);
                    t = t + l * ZeroPointNine;
                    t = t * ZeroPointZeroFive;
                    t = t + l * Half;
                    break;
            }

            return t * a;
        }

        private static bool IsPointStat(PlayerStats stat)
        {
            return stat == PlayerStats.FP || stat == PlayerStats.TP;
        }

        /// <summary>VCVT.U32.F32 then a 16 bit store: truncate towards zero, negatives become 0.</summary>
        private static int Saturate(float value)
        {
            if (float.IsNaN(value) || value <= 0.0f) return 0;
            if (value >= 65535.0f) return 0xFFFF;

            return (int)value;
        }

        private static int Clamp(int value, int minimum, int maximum)
        {
            if (value < minimum) return minimum;
            if (value > maximum) return maximum;
            return value;
        }
    }
}
