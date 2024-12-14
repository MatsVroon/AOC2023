namespace AOC2023.Days2024
{
    public class Day4 : Day
    {
        public override object RunA(string[] lines)
        {
            string word = "XMAS";
            (int, int)[] dirs = new[] { (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (1, -1), (-1, -1), (-1, 1) };
            int sum = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    foreach (var dir in dirs)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            var checkSpot = (x + i * dir.Item1, y + i * dir.Item2);
                            if (checkSpot.Item1 >= 0 && checkSpot.Item1 < lines[0].Length && checkSpot.Item2 >= 0 && checkSpot.Item2 < lines.Length)
                            {
                                if (lines[checkSpot.Item2][checkSpot.Item1] == 'X')
                                    ;
                                // within bounds
                                if (lines[checkSpot.Item2][checkSpot.Item1] != word[i])
                                {
                                    break;
                                }

                                if (i == 3)
                                {
                                    sum++;
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
            int sum = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == 'A')
                    {
                        if (y - 1 >= 0 && x - 1 >= 0 && y + 1 < lines.Length && x + 1 < lines[0].Length)
                        {
                            if ((lines[y - 1][x - 1] == 'M' && lines[y + 1][x + 1] == 'S') ||
                                (lines[y - 1][x - 1] == 'S' && lines[y + 1][x + 1] == 'M'))
                            {
                                if (y - 1 >= 0 && x + 1 < lines[0].Length && y + 1 < lines.Length && x - 1 >= 0)
                                {
                                    if ((lines[y - 1][x + 1] == 'M' && lines[y + 1][x - 1] == 'S') ||
                                        (lines[y - 1][x + 1] == 'S' && lines[y + 1][x - 1] == 'M'))
                                    {
                                        sum++;
                                    }
                                }
                            }
                        }
                                //
                    }
    }
            }

            return sum;
        }
    }
}
