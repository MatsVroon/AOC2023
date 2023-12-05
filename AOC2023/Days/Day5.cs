namespace AOC2023.Days
{
    public class Day5 : Day
    {
        public Day5() { }
        public override object RunA(string[] lines)
        {
            var splittedOnNewLines = splitStringEnumerable(lines).ToList();
            var startNumbersSplitted = splittedOnNewLines[0][0].Split(' ');
            var currentIds = startNumbersSplitted.Skip(1).Select(x => long.Parse(x));

            for (int i = 1; i < splittedOnNewLines.Count; i++)
            {
                var currentStringMap = splittedOnNewLines[i].Skip(1).ToList();
                var nextIds = new List<long>();

                foreach (long currentId in currentIds)
                {
                    checkMapForCurrentId(currentStringMap, nextIds, currentId);
                }
                currentIds = nextIds;
            }

            return currentIds.Min();
        }

        private static void checkMapForCurrentId(List<string> currentStringMap, List<long> nextIds, long currentId)
        {
            foreach (var currentString in currentStringMap)
            {
                var splittedCurrentString = currentString.Split(' ');
                var destination = long.Parse(splittedCurrentString[0]);
                var source = long.Parse(splittedCurrentString[1]);
                var range = long.Parse(splittedCurrentString[2]);

                var diff = currentId - source;
                if (diff >= 0 && diff < range)
                {
                    nextIds.Add(destination + diff);
                    return;
                }
            }
            nextIds.Add(currentId);
        }

        private List<List<string>> splitStringEnumerable(IEnumerable<string> strings)
        {
            List<List<string>> list = new List<List<string>>();
            while (strings.Any())
            {
                var newList = strings.TakeWhile(x => x != "").ToList();
                list.Add(newList);
                strings = strings.SkipWhile(x => x != "");
                strings = strings.Skip(1);
            }

            return list;
        }

        public override object RunB(string[] lines)
        {
            var splittedOnNewLines = splitStringEnumerable(lines).ToList();
            var startNumbersSplitted = splittedOnNewLines[0][0].Split(' ');
            var currentIdsStartRange = startNumbersSplitted.Skip(1).Select(x => long.Parse(x));
            var starts = currentIdsStartRange.Where((item, index) => index % 2 == 0);
            var ranges = currentIdsStartRange.Where((item, index) => index % 2 != 0);
            var currentIds = starts.Zip(ranges);

            for (int i = 1; i < splittedOnNewLines.Count; i++)
            {
                var currentStringMap = splittedOnNewLines[i].Skip(1).ToList();
                var nextIds = new List<(long, long)>();

                foreach (var (startId, rangeId) in currentIds)
                {
                    checkMapForCurrentIdRange(currentStringMap, nextIds, startId, rangeId);
                }

                currentIds = nextIds;
            }

            return currentIds.Select(x => x.First).Min();
        }

        private void checkMapForCurrentIdRange(List<string> currentStringMap, List<(long, long)> nextIds, long startId, long rangeId)
        {
            var ranges = new Stack<(long, long)>();
            ranges.Push((startId, rangeId));
            var mapIndexer = 0;
            while (ranges.Count > 0)
            {
                var isFullyDisjunct = true;
                var (currentRangeStart, currentRangeRange) = ranges.Pop();
                while (mapIndexer < currentStringMap.Count)
                {
                    var currentString = currentStringMap[mapIndexer];
                    var splittedCurrentString = currentString.Split(' ');
                    var destination = long.Parse(splittedCurrentString[0]);
                    var source = long.Parse(splittedCurrentString[1]);
                    var range = long.Parse(splittedCurrentString[2]);

                    var startCurrent = currentRangeStart;                    // 79
                    var endCurrent = currentRangeStart + currentRangeRange;  // 93  -- notice that 93 is not included
                    var startMap = source;                                   // 50  
                    var endMap = source + range;                             // 98  -- notice that 98 is not included

                    mapIndexer++;

                    // The map is fully strictly inside domain of current ids 
                    if (startMap >= startCurrent && endMap <= endCurrent)
                    {
                        var diffLeft = startMap - startCurrent;
                        if (diffLeft != 0) ranges.Push((startCurrent, diffLeft));

                        var diffRight = endCurrent - endMap;
                        if (diffRight != 0) ranges.Push((endMap, diffRight));

                        nextIds.Add((destination, range));
                        isFullyDisjunct = false;
                        break;
                    }

                    // The map starts inside the domain but ends outside of current ids
                    if (startMap >= startCurrent && startMap < endCurrent && endMap > endCurrent)
                    {
                        var diffLeft = startMap - startCurrent;
                        if (diffLeft != 0) ranges.Push((startCurrent, diffLeft));

                        var diffMiddle = endCurrent - startMap;
                        nextIds.Add((destination, diffMiddle));

                        isFullyDisjunct = false;
                        break;
                    }

                    // The map start outside the domain but ends inside of current ids
                    if (startMap < startCurrent && endMap > startCurrent && endMap <= endCurrent)
                    {
                        var diffLeft = startCurrent - startMap;
                        var diffMiddle = endMap - startCurrent;
                        nextIds.Add((destination + diffLeft, diffMiddle));

                        var diffRight = endCurrent - endMap;
                        if (diffRight != 0) ranges.Push((endMap, diffRight));
                        isFullyDisjunct = false;
                        break;
                    }

                    // The map starts and ends outside the domain of current ids
                    if (startMap < startCurrent && endMap > endCurrent)
                    {
                        var diff = startCurrent - startMap;
                        nextIds.Add((destination + diff, rangeId));
                        isFullyDisjunct = false;
                        break;
                    }
                }
                if (isFullyDisjunct)
                {
                    nextIds.Add((currentRangeStart, currentRangeRange));
                }
            }
        }
    }
}
