namespace AOC2023.Days2024
{
    public class Day8 : Day
    {
        public override object RunA(string[] lines)
        {
            int sum = 0;
            var freqToLoc = new Dictionary<char, List<(int, int)>>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] != '.')
                    {
                        if (freqToLoc.ContainsKey(lines[i][j]))
                        {
                            freqToLoc[lines[i][j]].Add((j, i));
                        }
                        else
                        {
                            freqToLoc[lines[i][j]] = new List<(int, int)>(new[] { (j, i) });
                        }
                    }
                }
            }

            var duos = new List<((int, int), (int, int))>();
            foreach (var key in freqToLoc.Keys)
            {
                var spots = freqToLoc[key];
                for (int i = 0; i < spots.Count; i++)
                {
                    for (int j = i+1; j < spots.Count; j++)
                    {
                        duos.Add((spots[i], spots[j]));
                    }
                }
            }

            var allAntiondes = new HashSet<(int, int)>();
            for (int i = 0; i < duos.Count; i++)
            {
                var (loc1, loc2) = duos[i];
                allAntiondes.Add(loc1);
                allAntiondes.Add(loc2);
                var diff = (loc2.Item1 - loc1.Item1, loc2.Item2 - loc1.Item2);

                // Try first way
                var antinodeLoc1 = (loc2.Item1 + diff.Item1, loc2.Item2 + diff.Item2);
                while (antinodeLoc1.Item1 >= 0 && antinodeLoc1.Item1 < lines[0].Length && antinodeLoc1.Item2 >= 0 && antinodeLoc1.Item2 < lines.Length)
                {
                    allAntiondes.Add(antinodeLoc1);
                    antinodeLoc1 = (antinodeLoc1.Item1 + diff.Item1, antinodeLoc1.Item2 + diff.Item2);
                }

                var antinodeLoc2 = (loc1.Item1 - diff.Item1, loc1.Item2 - diff.Item2);
                while (antinodeLoc2.Item1 >= 0 && antinodeLoc2.Item1 < lines[0].Length && antinodeLoc2.Item2 >= 0 && antinodeLoc2.Item2 < lines.Length)
                {
                    allAntiondes.Add(antinodeLoc2);
                    antinodeLoc2 = (antinodeLoc2.Item1 - diff.Item1, antinodeLoc2.Item2 - diff.Item2);
                }
            }

            return allAntiondes.Count;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
