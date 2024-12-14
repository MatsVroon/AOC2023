using System.Numerics;

namespace AOC2023.Days2024
{
    public class Day10 : Day
    {
        public override object RunA(string[] lines)
        {
            int sum = 0;
            List<(int, int)> startLocation = new List<(int, int)>();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '0')
                    {
                        startLocation.Add((x, y));
                    }
                }
            }

            (int, int)[] dirs = new[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

            for (int i = 0; i < startLocation.Count; i++)
            {
                // Start de BFS
                Queue<(int, int)> queue = new Queue<(int, int)>();
                queue.Enqueue(startLocation[i]);
                HashSet<(int, int)> visited = new HashSet<(int, int)>();

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    visited.Add(current);
                    var currentHeight = lines[current.Item2][current.Item1];

                    if (currentHeight == '9')
                    {
                        sum++;
                    }
                    else
                    {
                        foreach (var dir in dirs)
                        {
                            var neighbour = (current.Item1 + dir.Item1, current.Item2 + dir.Item2);
                            if (!visited.Contains(neighbour))
                            {
                                if (neighbour.Item1 >= 0 && neighbour.Item1 < lines[0].Length && neighbour.Item2 >= 0 && neighbour.Item2 < lines.Length)
                                {
                                    // inside the map
                                    var neighbourHeight = lines[neighbour.Item2][neighbour.Item1];
                                    if (neighbourHeight - currentHeight == 1)
                                    {
                                        queue.Enqueue(neighbour);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return sum;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
