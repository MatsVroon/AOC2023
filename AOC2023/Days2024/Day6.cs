using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AOC2023.Days2024
{
    public class Day6 : Day
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public override object RunA(string[] lines)
        {
            char[,] map = new char[lines.Length, lines[0].Length];
            (int, int) startLocation = (0, 0);

            Direction dir = Direction.Up;
            (int, int) location = (0, 0);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    map[y, x] = lines[y][x];
                    if (lines[y][x] == '^')
                    {
                        dir = Direction.Up;
                        location = (x, y);
                        startLocation = (x, y);
                    }
                }
            }
            bool gaande = true;
            var hashSetTried = new HashSet<(int, int)>();
            var hashSet = new HashSet<(int, int)>();
            var route = new List<(int, int)>();
            while (gaande)
            {
                route.Add(location);
                if (dir == Direction.Up)
                {
                    location = (location.Item1, location.Item2 - 1);
                    if (location.Item2 < 0)
                    {
                        break;
                    }
                    if (lines[location.Item2][location.Item1] == '#')
                    {
                        dir = Direction.Right;
                        location = (location.Item1, location.Item2 + 1);
                    }
                    else
                    {
                        if (location != startLocation && lines[location.Item2][location.Item1] == '.')
                        {
                            if (!hashSetTried.Contains(location))
                            {
                                map[location.Item2, location.Item1] = '#';
                                if (hasCycle(map, (location.Item1, location.Item2 + 1), Direction.Right))
                                {
                                    hashSetTried.Add(location);
                                    hashSet.Add(location);
                                }
                                else
                                {
                                    hashSetTried.Add(location);
                                }
                                map[location.Item2, location.Item1] = '.';
                            }
                        }
                    }
                }
                else if (dir == Direction.Down)
                {
                    location = (location.Item1, location.Item2 + 1);
                    if (location.Item2 >= lines.Length)
                    {
                        break;
                    }
                    if (lines[location.Item2][location.Item1] == '#')
                    {
                        dir = Direction.Left;
                        location = (location.Item1, location.Item2 - 1);
                    }
                    else
                    {
                        if (location != startLocation)
                        {
                            if (!hashSetTried.Contains(location))
                            {
                                map[location.Item2, location.Item1] = '#';
                                if (hasCycle(map, (location.Item1, location.Item2 - 1), Direction.Left))
                                {
                                    hashSetTried.Add(location);
                                    hashSet.Add(location);
                                }
                                else
                                {
                                    hashSetTried.Add(location);
                                }
                                map[location.Item2, location.Item1] = '.';
                            }
                        }
                    }
                }
                else if (dir == Direction.Left)
                {
                    location = (location.Item1 - 1, location.Item2);
                    if (location.Item1 < 0)
                    {
                        break;
                    }

                    if (lines[location.Item2][location.Item1] == '#')
                    {
                        dir = Direction.Up;
                        location = (location.Item1 + 1, location.Item2);
                    }
                    else
                    {
                        if (location != startLocation)
                        {
                            if (!hashSetTried.Contains(location))
                            {
                                map[location.Item2, location.Item1] = '#';
                                if (hasCycle(map, (location.Item1 + 1, location.Item2), Direction.Up))
                                {
                                    hashSetTried.Add(location);
                                    hashSet.Add(location);
                                }
                                else
                                {
                                    hashSetTried.Add(location);
                                }
                                map[location.Item2, location.Item1] = '.';
                            }
                        }
                    }
                }
                else
                {
                    location = (location.Item1 + 1, location.Item2);
                    if (location.Item1 >= lines[location.Item2].Length)
                    {
                        break;
                    }

                    if (lines[location.Item2][location.Item1] == '#')
                    {
                        dir = Direction.Down;
                        location = (location.Item1 - 1, location.Item2);
                    }
                    else
                    {
                        if (startLocation != location)
                        {
                            if (!hashSetTried.Contains(location))
                            {
                                map[location.Item2, location.Item1] = '#';
                                if (hasCycle(map, (location.Item1 - 1, location.Item2), Direction.Down))
                                {
                                    hashSetTried.Add(location);
                                    hashSet.Add(location);
                                }
                                else
                                {
                                    hashSetTried.Add(location);
                                }
                                map[location.Item2, location.Item1] = '.';
                            }
                        }
                    }
                }
            }
            return hashSet.Count;
        }

        private bool hasCycle(char[,] lines, (int, int) location, Direction dir)
        {
            var visited = new HashSet<(int, int, Direction)>();
            var gaande = true;
            while (gaande)
            {
                if (visited.Contains((location.Item1, location.Item2, dir)))
                {
                    return true;
                }
                else
                {
                    visited.Add((location.Item1, location.Item2, dir));
                }
                if (dir == Direction.Up)
                {
                    location = (location.Item1, location.Item2 - 1);
                    if (location.Item2 < 0)
                    {
                        return false;
                    }
                    if (lines[location.Item2, location.Item1] == '#')
                    {
                        dir = Direction.Right;
                        location = (location.Item1, location.Item2 + 1);
                    }
                }
                else if (dir == Direction.Down)
                {
                    location = (location.Item1, location.Item2 + 1);
                    if (location.Item2 >= lines.GetLength(0))
                    {
                        return false;
                    }
                    if (lines[location.Item2, location.Item1] == '#')
                    {
                        dir = Direction.Left;
                        location = (location.Item1, location.Item2 - 1);
                    }
                }
                else if (dir == Direction.Left)
                {
                    location = (location.Item1 - 1, location.Item2);
                    if (location.Item1 < 0)
                    {
                        return false;
                    }

                    if (lines[location.Item2, location.Item1] == '#')
                    {
                        dir = Direction.Up;
                        location = (location.Item1 + 1, location.Item2);
                    }
                }
                else
                {
                    location = (location.Item1 + 1, location.Item2);
                    if (location.Item1 >= lines.GetLength(1))
                    {
                        return false;
                    }

                    if (lines[location.Item2, location.Item1] == '#')
                    {
                        dir = Direction.Down;
                        location = (location.Item1 - 1, location.Item2);
                    }
                }
            }
            return false;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
