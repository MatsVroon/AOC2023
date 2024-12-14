

namespace AOC2023.Days2024
{
    public class Day14 : Day
    {
        public override object RunA(string[] lines)
        {
            List<((int, int), (int, int))> posDir = new List<((int, int), (int, int))>();
            foreach (var line in lines)
            {
                var split = line.Split(" v=");
                var splitLeft = split[0].Split("=");
                var pos = splitLeft[1].Split(",").Select(x => int.Parse(x)).ToList();
                var dir = split[1].Split(",").Select(x => int.Parse(x)).ToList();
                posDir.Add(((pos[0], pos[1]), (dir[0], dir[1])));
            }

            int width = 101;
            int tall = 103;
            var middlePos = (50, 51);
            int minManDistance = int.MaxValue;
            for (int i = 0; i < 10000; i++)
            {
                int totalManDist = 0;
                HashSet<(int, int)> allPos = new HashSet<(int, int)>();
                foreach (var (pos, dir) in posDir)
                {
                    var newPos = (posMod(pos.Item1 + (dir.Item1 * i), width), posMod(pos.Item2 + (dir.Item2 * i), tall));
                    allPos.Add(newPos);
                    var distVec = (newPos.Item1 - middlePos.Item1, newPos.Item2 - middlePos.Item2);
                    var manDistance = Math.Abs(distVec.Item1) + Math.Abs(distVec.Item2);
                    totalManDist += manDistance;
                }

                if (totalManDist < minManDistance)
                {
                    minManDistance = totalManDist;
                    Console.WriteLine($"New minimum found on second {i}, with total man dist: {totalManDist}");
                    var map = new char[tall, width];
                    foreach (var pos in allPos)
                    {
                        map[pos.Item2, pos.Item1] = '*';
                    }

                    for (int y = 0; y < tall; y++)
                    {
                        Console.Write("\n");
                        for (int x = 0; x < width; x++)
                        {
                            if (map[y, x] == '\0')
                            {
                                Console.Write(".");
                            }
                            else if (map[y, x] == '*')
                            {
                                Console.Write("*");
                            }
                        }
                    }
                }
            }

            return 0;
        }

        public int posMod (int a, int b)
        {
            var r = a % b;
            return r < 0 ? r + b : r;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
