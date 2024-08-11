﻿using System.Collections.Generic;

namespace Lynx.Level5.Save.Logic
{
    public class Move
    {
        public string Name;

        public Element Element;

        public Position Position;

        public int Power;

        public int TP;

        public int Difficulty;

        public int Damage;

        public int EvolutionCount;

        public EvolutionSpeed EvolutionSpeed;

        public UltimateEvolution UltimateEvolution;

        public List<int> PowerLevel = new List<int>();

        public int Level;

        public bool Unlock;

        public int UsedCount;

        public Move()
        {

        }

        public Move(Move _Move, int level, bool unlock, int timeLevel)
        {
            Name = _Move.Name;
            Element = _Move.Element;
            Position = _Move.Position;
            Power = _Move.Power;
            TP = _Move.TP;
            Difficulty = _Move.Difficulty;
            Damage = _Move.Damage;
            EvolutionCount = _Move.EvolutionCount;
            EvolutionSpeed = _Move.EvolutionSpeed;
            UltimateEvolution = _Move.UltimateEvolution;

            Level = level;
            Unlock = unlock;
            UsedCount = timeLevel;
        }

        public Move(string _Name, Element _Element, Position _Position, int _Power, int _Tp, int _Difficulty, int _Damage, int _EvolutionCount, EvolutionSpeed _EvolutionSpeed, UltimateEvolution _UltimateEvolution)
        {
            Name = _Name;
            Element = _Element;
            Position = _Position;
            Power = _Power;
            TP = _Tp;
            Difficulty = _Difficulty;
            Damage = _Damage;
            EvolutionCount = _EvolutionCount;
            EvolutionSpeed = _EvolutionSpeed;
            Level = 1;

            if (this.EvolutionSpeed != null)
            {
                PowerLevel.Add(_Power);

                // true =  IE1/IE2/IE Game & False = Go/Cs/Galaxy
                if (this.EvolutionSpeed.PowerLevel.Count != 0)
                {
                    for (int i = 0; i < EvolutionCount - 1; i++)
                    {
                        PowerLevel.Add(_Power + EvolutionSpeed.PowerLevel[i]);
                    }
                }
                else
                {
                    if (_EvolutionCount < 6)
                    {
                        for (int i = 0; i < EvolutionCount - 1; i++)
                        {
                            PowerLevel.Add(PowerLevel[i] + (10 / PowerLevel[i] * 100));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            PowerLevel.Add(PowerLevel[i] + (10 / PowerLevel[i] * 100));
                        }
                        UltimateEvolution = _UltimateEvolution;
                        // Missing Ultimate Evolution
                    }
                }

                EvolutionSpeed.TimeLevel.Insert(0, 0);
            }
            else
            {
                EvolutionSpeed = new EvolutionSpeed("Skill", new List<int> { 0 }, new List<int> { 0 });
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public int? PowerAtLevel(int level)
        {
            if (level > PowerLevel.Count)
            {
                return null;
            }
            else if (level == 0 | level == 1)
            {
                return PowerLevel[0];
            }
            else if (level == 6)
            {
                return UltimateEvolution.PowerM;
            }
            else
            {
                return PowerLevel[level - 1];
            }
        }
    }

    public class EvolutionSpeed
    {
        public string Name;

        public List<int> PowerLevel = new List<int>();

        public List<int> TimeLevel = new List<int>();

        public EvolutionSpeed(string _Name, List<int> _PowerUp, List<int> _TimeLevel)
        {
            Name = _Name;
            PowerLevel = _PowerUp;
            TimeLevel = _TimeLevel;
        }

        public static EvolutionSpeed Slow(int evolutionCount, string game)
        {
            if (evolutionCount == 3)
            {
                return new EvolutionSpeed("Slow", new List<int> { 5, 12 }, new List<int> { 15, 33, 0 });
            }
            else if (evolutionCount == 4)
            {
                return new EvolutionSpeed("Slow", new List<int> { }, new List<int> { 20, 30, 35, 0 });
            }
            else if (evolutionCount == 5 & game == "ie")
            {
                return new EvolutionSpeed("Slow", new List<int> { 4, 10, 14, 18 }, new List<int> { 20, 45, 70, 100, 0 });
            }
            else if (evolutionCount == 5 & game == "go")
            {
                return new EvolutionSpeed("Slow", new List<int> { }, new List<int> { 20, 30, 35, 40, 0 });
            }
            else // evolutionCount == 6
            {
                return new EvolutionSpeed("Slow", new List<int> { }, new List<int> { 15, 20, 20, 25, 0 });
            }
        }

        public static EvolutionSpeed Medium(int evolutionCount, string game)
        {
            if (evolutionCount == 3)
            {
                return new EvolutionSpeed("Medium", new List<int> { 3, 10 }, new List<int> { 9, 27, 0 });
            }
            else if (evolutionCount == 4)
            {
                return new EvolutionSpeed("Medium", new List<int> { }, new List<int> { 15, 20, 30, 0 });
            }
            else if (evolutionCount == 5 & game == "ie")
            {
                return new EvolutionSpeed("Medium", new List<int> { 3, 8, 12, 16 }, new List<int> { 20, 40, 60, 90, 0 });
            }
            else if (evolutionCount == 5 & game == "go")
            {
                return new EvolutionSpeed("Medium", new List<int> { }, new List<int> { 15, 20, 30, 40, 0 });
            }
            else // evolutionCount == 6
            {
                return new EvolutionSpeed("Medium", new List<int> { }, new List<int> { 10, 15, 20, 25, 0 });
            }
        }

        public static EvolutionSpeed Fast(int evolutionCount, string game)
        {
            if (evolutionCount == 3)
            {
                return new EvolutionSpeed("Fast", new List<int> { 2, 8 }, new List<int> { 6, 21, 0 });
            }
            else if (evolutionCount == 4)
            {
                return new EvolutionSpeed("Fast", new List<int> { }, new List<int> { 10, 15, 25, 0 });
            }
            else if (evolutionCount == 5 & game == "ie")
            {
                return new EvolutionSpeed("Fast", new List<int> { 2, 6, 10, 14 }, new List<int> { 15, 35, 55, 80, 0 });
            }
            else if (evolutionCount == 5 & game == "go")
            {
                return new EvolutionSpeed("Fast", new List<int> { }, new List<int> { 10, 15, 25, 40, 0 });
            }
            else // evolutionCount == 6
            {
                return new EvolutionSpeed("Fast", new List<int> { }, new List<int> { 10, 10, 15, 20, 0 });
            }
        }

        public static EvolutionSpeed Turbo(int evolutionCount, string game)
        {
            return new EvolutionSpeed("Turbo", new List<int> { }, new List<int> { 5, 10, 10, 15, 0 });
        }
    }

    public class UltimateEvolution
    {
        public string Name;

        public int PowerM;

        public int TP;

        public int Difficulty;

        public int Damage;

        public UltimateEvolution(string _Name)
        {

        }

        public UltimateEvolution Power(string _Position, int _Difficulty)
        {
            UltimateEvolution power = new UltimateEvolution("Power");

            switch (_Position)
            {
                case "Shoot":
                    PowerM = 320;
                    TP = 99;
                    Damage = 10;
                    break;
                case "Dribble":
                    PowerM = 320;
                    TP = 99;
                    Damage = 5;
                    break;
                case "Block":
                    PowerM = 320;
                    TP = 85;
                    Damage = 5;
                    break;
                case "Catch":
                    PowerM = 320;
                    TP = 85;
                    Damage = -10;
                    break;
                default:
                    break;
            }

            if (Difficulty < 100)
            {
                Difficulty = 100;
            }
            else
            {
                Difficulty = _Difficulty;
            }

            return power;
        }

        public UltimateEvolution Balanced(string _Position, int _Difficulty)
        {
            UltimateEvolution balanced = new UltimateEvolution("Balanced");

            switch (_Position)
            {
                case "Shoot":
                    PowerM = 300;
                    TP = 99;
                    Damage = 50;
                    break;
                case "Dribble":
                    PowerM = 300;
                    TP = 99;
                    Damage = 80;
                    break;
                case "Block":
                    PowerM = 300;
                    TP = 85;
                    Damage = 80;
                    break;
                case "Catch":
                    PowerM = 300;
                    TP = 85;
                    Damage = -40;
                    break;
                default:
                    break;
            }

            if (Difficulty < 100)
            {
                Difficulty = 100;
            }
            else
            {
                Difficulty = _Difficulty;
            }

            return balanced;
        }

        public UltimateEvolution Stunner(string _Position, int _Difficulty)
        {
            UltimateEvolution stunner = new UltimateEvolution("Balanced");

            switch (_Position)
            {
                case "Shoot":
                    PowerM = 290;
                    TP = 99;
                    Damage = 100;
                    break;
                case "Dribble":
                    PowerM = 290;
                    TP = 99;
                    Damage = 150;
                    break;
                case "Block":
                    PowerM = 290;
                    TP = 85;
                    Damage = 150;
                    break;
                case "Catch":
                    PowerM = 290;
                    TP = 85;
                    Damage = -80;
                    break;
                default:
                    break;
            }

            if (Difficulty < 100)
            {
                Difficulty = 100;
            }
            else
            {
                Difficulty = _Difficulty;
            }

            return stunner;
        }

        public UltimateEvolution ChainBlockLongShot(int _Difficulty)
        {
            UltimateEvolution power = new UltimateEvolution("Power");

            PowerM = 300;
            TP = 99;
            Damage = 5;

            if (Difficulty < 100)
            {
                Difficulty = 100;
            }
            else
            {
                Difficulty = _Difficulty;
            }

            return power;
        }

        public UltimateEvolution PunchingCatchDamageDribble(string _Position, int _Difficulty)
        {
            UltimateEvolution stunner = new UltimateEvolution("Stunner");

            PowerM = 280;

            switch (_Position.ToString())
            {
                case "Dribble":
                    TP = 99;
                    Damage = 150;
                    break;
                case "Catch":
                    TP = 85;
                    Damage = -80;
                    break;
                default:
                    break;
            }

            if (Difficulty < 100)
            {
                Difficulty = 100;
            }
            else
            {
                Difficulty = _Difficulty;
            }

            return stunner;
        }

        public UltimateEvolution DefenseBlock(int _Difficulty)
        {
            UltimateEvolution balanced = new UltimateEvolution("Balanced");

            PowerM = 280;
            TP = 85;
            Damage = 50;

            if (Difficulty < 100)
            {
                Difficulty = 100;
            }
            else
            {
                Difficulty = _Difficulty;
            }

            return balanced;
        }
    }
}
