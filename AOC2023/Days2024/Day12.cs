using System.Globalization;
using System.Numerics;
using System.Security;

namespace AOC2023.Days2024
{
    public class Day12 : Day
    {
        enum Side
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }
        public override object RunA(string[] lines)
        {
            Dictionary<(int, int), int> locToComponent = new Dictionary<(int, int), int>();
            Dictionary<int, List<(int, int)>> compToLocs = new Dictionary<int, List<(int, int)>>();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (!locToComponent.ContainsKey((x, y)) && lines[y][x] != ' ')
                    {
                        discoverComponent((x, y), lines, locToComponent, compToLocs);
                    }
                }
            }

            int totalCost = 0;
            var dirs = new List<((int, int), Side)>(new[] { ((1, 0), Side.Right), ((-1, 0), Side.Left), ((0, 1), Side.Down), ((0, -1), Side.Up), });
            HashSet<((int, int), Side)> checkedSides = new HashSet<((int, int), Side)>();
            foreach (var (compNum, locs) in compToLocs)
            {
                int sideSum = 0;
                foreach (var loc in locs)
                {
                    var letter = lines[loc.Item2][loc.Item1];
                    foreach (var (dir, side) in dirs)
                    { 
                        var otherLoc = (loc.Item1 + dir.Item1, loc.Item2 + dir.Item2);
                        if (otherLoc.Item2 < 0 || otherLoc.Item2 >= lines.Length || otherLoc.Item1 < 0 || otherLoc.Item1 >= lines[otherLoc.Item2].Length || letter != lines[otherLoc.Item2][otherLoc.Item1])
                        {
                            if (!checkedSides.Contains((loc, side)))
                            {
                                // Side is found, start crawler
                                checkedSides.Add((loc, side));
                                sideSum++;

                                var sameSide = true;
                                var crawlDirSide1 = (Side)(((int)side + 1) % 4);
                                var crawlDirSide2 = (Side)(((int)side + 3) % 4);
                                var (crawlDir1, _) = dirs.Find(x => x.Item2 == crawlDirSide1);
                                var (crawlDir2, _) = dirs.Find(x => x.Item2 == crawlDirSide2);

                                var crawlLoc = loc;
                                while (sameSide)
                                {
                                    crawlLoc = (crawlLoc.Item1 + crawlDir1.Item1, crawlLoc.Item2 + crawlDir1.Item2);
                                    if (crawlLoc.Item2 >= 0 && crawlLoc.Item2 < lines.Length && crawlLoc.Item1 >= 0 && crawlLoc.Item1 < lines[crawlLoc.Item2].Length && letter == lines[crawlLoc.Item2][crawlLoc.Item1])
                                    {
                                        var crawlOtherLoc = (crawlLoc.Item1 + dir.Item1, crawlLoc.Item2 + dir.Item2);

                                        if (crawlOtherLoc.Item2 < 0 || crawlOtherLoc.Item2 >= lines.Length || crawlOtherLoc.Item1 < 0 || crawlOtherLoc.Item1 >= lines[crawlOtherLoc.Item2].Length || letter != lines[crawlOtherLoc.Item2][crawlOtherLoc.Item1])
                                        {
                                            checkedSides.Add((crawlLoc, side));
                                        }
                                        else
                                        {
                                            sameSide = false;
                                        }
                                    }
                                    else
                                    {
                                        sameSide = false;
                                    }
                                }

                                crawlLoc = loc;
                                sameSide = true;
                                while (sameSide)
                                {
                                    crawlLoc = (crawlLoc.Item1 + crawlDir2.Item1, crawlLoc.Item2 + crawlDir2.Item2);
                                    if (crawlLoc.Item2 >= 0 && crawlLoc.Item2 < lines.Length && crawlLoc.Item1 >= 0 && crawlLoc.Item1 < lines[crawlLoc.Item2].Length && letter == lines[crawlLoc.Item2][crawlLoc.Item1])
                                    {
                                        var crawlOtherLoc = (crawlLoc.Item1 + dir.Item1, crawlLoc.Item2 + dir.Item2);

                                        if (crawlOtherLoc.Item2 < 0 || crawlOtherLoc.Item2 >= lines.Length || crawlOtherLoc.Item1 < 0 || crawlOtherLoc.Item1 >= lines[crawlOtherLoc.Item2].Length || letter != lines[crawlOtherLoc.Item2][crawlOtherLoc.Item1])
                                        {
                                            checkedSides.Add((crawlLoc, side));
                                        }
                                        else
                                        {
                                            sameSide = false;
                                        }
                                    }
                                    else
                                    {
                                        sameSide = false;
                                    }
                                }
                            }
                        }
                    }
                }
                totalCost += sideSum * locs.Count;
            }

            return totalCost;
        }

        public void discoverComponent((int,int) loc, string[] lines, Dictionary<(int, int), int> locToComponent, Dictionary<int, List<(int, int)>> compToLocs)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            var compNumber = compToLocs.Count;
            char letter = lines[loc.Item2][loc.Item1];
            locToComponent.Add(loc, compNumber);
            compToLocs.Add(compNumber, new List<(int, int)>(new[] { loc } ));
            visited.Add(loc);

            var queue = new Queue<(int, int)>();

            var dirs = new List<(int, int)>(new[] { (1,0), (-1, 0), (0, 1), (0, -1), });
            foreach (var dir in dirs)
            {
                queue.Enqueue((loc.Item1 + dir.Item1, loc.Item2 + dir.Item2));
            }

            while (queue.Count > 0)
            {
                var currentLoc = queue.Dequeue();

                if (!visited.Contains(currentLoc) && currentLoc.Item2 >= 0 && currentLoc.Item2 < lines.Length && currentLoc.Item1 >= 0 && currentLoc.Item1 < lines[currentLoc.Item2].Length)
                {
                    visited.Add(currentLoc);
                    if (lines[currentLoc.Item2][currentLoc.Item1] == letter)
                    {
                        locToComponent.Add(currentLoc, compNumber);
                        compToLocs[compNumber].Add(currentLoc);

                        foreach (var dir in dirs)
                        {
                            queue.Enqueue((currentLoc.Item1 + dir.Item1, currentLoc.Item2 + dir.Item2));
                        }
                    }
                }                
            }
        }
        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
