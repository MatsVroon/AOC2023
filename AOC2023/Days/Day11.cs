namespace AOC2023.Days
{
    public class Day11 : Day
    {
        public Day11() { }
        public override object RunA(string[] lines)
        {
            var emptyRows = new List<int>();
            var emptyColumns = new List<int>();
            var galaxies = new List<(int, int)>();

            // find empty rows and galaxies
            for (int i = 0; i < lines.Length; i++)
            {
                bool isEmptyRow = true;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        galaxies.Add((j, i));
                        isEmptyRow = false;
                    }
                }
                if (isEmptyRow) emptyRows.Add(i);
            }

            // Find empty columns
            for (int i = 0; i < lines.Length; i++)
            {
                bool isEmptyColumn = true;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[j][i] == '#')
                    {
                        isEmptyColumn = false;
                        break;
                    }
                }
                if (isEmptyColumn) emptyColumns.Add(i);
            }

            var allPairs = new List<((int, int), (int, int))>();
            for (int i = 0; i < galaxies.Count; i++)
            {
                var currentXY = galaxies[i];
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    allPairs.Add((currentXY, galaxies[j]));
                }
            }

            var distances = new List<long>();
            foreach (((int, int), (int, int)) pair in allPairs)
            {
                var start = pair.Item1;
                var end = pair.Item2;

                var smallestX = start.Item1 < end.Item1 ? start.Item1 : end.Item1;
                var largestX = start.Item1 < end.Item1 ? end.Item1 : start.Item1;
                var smallestY = start.Item2 < end.Item2 ? start.Item2 : end.Item2;
                var largestY = start.Item2 < end.Item2 ? end.Item2 : start.Item2;

                var diffX = largestX - smallestX;
                var diffY = largestY - smallestY;

                long distance = diffX + diffY;

                foreach (var emptyRow in emptyRows)
                {
                    if (emptyRow > smallestY && emptyRow < largestY)
                    {
                        distance += 999999;
                    }
                }

                foreach (var emptyColumn in emptyColumns)
                {
                    if (emptyColumn > smallestX && emptyColumn < largestX)
                    {
                        distance += 999999;
                    }
                }

                distances.Add(distance);
            }

            return distances.Sum();
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
