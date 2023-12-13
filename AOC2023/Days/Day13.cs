namespace AOC2023.Days
{
    public class Day13 : Day
    {
        public Day13() { }
        public override object RunA(string[] lines)
        {
            var allPaterns = splitStringEnumerable(lines);
            var total = 0;
            foreach (var patern in allPaterns)
            {
                // Do something
                total += findMirror(patern);
            }

            return total;
        }

        private int findMirror(List<string> patern)
        {
            var verticalScore = findVerticalMirror(patern);
            return verticalScore > 0 ? verticalScore : 100 * findHorizontalMirror(patern);
        }

        private int findHorizontalMirror(List<string> patern)
        {
            for (int i = 0; i < patern.Count - 1; i++)
            {
                var smudgeUsed = false;
                var upperIndexer = i;
                var lowerIndexer = i + 1;

                while (upperIndexer >= 0 && lowerIndexer < patern.Count)
                {
                    var checkTwoRows = checkTwoHorizontalRowsOnEquality(patern, upperIndexer, lowerIndexer);

                    // Not equal with smudge
                    if (checkTwoRows == -1)
                    {
                        break;
                    }

                    // Equal with smudge
                    if (checkTwoRows == 0 && !smudgeUsed)
                    {
                        smudgeUsed = true;
                    }
                    else if (checkTwoRows == 0 && smudgeUsed)
                    {
                        break;
                    }

                    upperIndexer--;
                    lowerIndexer++;
                }

                if ((upperIndexer < 0 || lowerIndexer >= patern.Count) && smudgeUsed)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private int findVerticalMirror(List<string> patern)
        {
            // mirror index is between two in the array: i = 0, means between array index 0 and 1.
            for (int i = 0; i < patern[0].Length - 1; i++)
            {
                var smudgeUsed = false;
                var leftIndexer = i;
                var rightIndexer = i + 1;

                while (leftIndexer >= 0 && rightIndexer < patern[0].Length)
                {
                    var checkTwoColumns = checkTwoVerticalColumnsOnEquality(patern, leftIndexer, rightIndexer);

                    // Not equal with smudge
                    if (checkTwoColumns == -1)
                    {
                        break;
                    }

                    // Equal with smudge
                    if (checkTwoColumns == 0 && !smudgeUsed)
                    {
                        smudgeUsed = true;
                    }
                    else if (checkTwoColumns == 0 && smudgeUsed)
                    {
                        break;
                    }

                    leftIndexer--;
                    rightIndexer++;
                }

                if ((leftIndexer < 0 || rightIndexer >= patern[0].Length) && smudgeUsed)
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private int checkTwoHorizontalRowsOnEquality(List<string> patern, int upper, int lower)
        {
            // return 1 if just equal
            if (patern[upper] == patern[lower])
            {
                return 1;
            }

            // return 0 if there is 1 smudge difference
            var diff = 0;
            for (int i = 0; i < patern[upper].Length; i++)
            {
                if (patern[upper][i] != patern[lower][i])
                {
                    diff++;
                }

                if (diff > 1)
                {
                    return -1;
                }
            }

            return 0;
        }

        private int checkTwoVerticalColumnsOnEquality(List<string> patern, int leftIndexer, int rightIndexer)
        {
            var smudgeCounter = 0;
            for (int j = 0; j < patern.Count; j++)
            {
                if (patern[j][leftIndexer] != patern[j][rightIndexer])
                {
                    smudgeCounter++;

                    if (smudgeCounter > 1)
                    {
                        return -1;
                    }
                }
            }

            if (smudgeCounter == 1)
            {
                return 0;
            }

            return 1;
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
            throw new NotImplementedException();
        }
    }
}
