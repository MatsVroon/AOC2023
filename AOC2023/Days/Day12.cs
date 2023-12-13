namespace AOC2023.Days
{
    public class Day12 : Day
    {
        public Day12() { }
        public override object RunA(string[] lines)
        {
            long total = 0;
            for (int j = 0; j < lines.Length; j++)
            {
                Console.WriteLine($"{j}/{lines.Length}");
                var line = lines[j];
                var lineSplitOnSpace = line.Split(' ');
                var playField = String.Concat(lineSplitOnSpace[0], "?", lineSplitOnSpace[0], "?", lineSplitOnSpace[0], "?", lineSplitOnSpace[0], "?", lineSplitOnSpace[0]);
                var playFieldSplitPoints = playField.Split('.').Where(x => x != "").ToArray();
                var numsOnce = lineSplitOnSpace[1].Split(',').Select(x => int.Parse(x)).ToArray();
                var nums = new int[numsOnce.Length * 5];
                for (int i = 0; i < 5; i++)
                {
                    numsOnce.CopyTo(nums, i * numsOnce.Length);
                }

                var cache = new long[playField.Length + 1, nums.Length + 1];
                for (int x = 0; x < playField.Length + 1; x++)
                {
                    for (int y = 0; y < nums.Length + 1; y++)
                    {
                        cache[x, y] = -1;
                    }
                }

                total += amountOfPossibilities(playField.Length, nums, 0, playField, cache);
            }

            return total;
        }

        private long amountOfPossibilities(int n, int[] nums, int indexerNums, string playField, long[,] cache)
        {
            if (n > 0 && indexerNums > 0 && cache[n, indexerNums] >= 0)
            {
                return cache[n, indexerNums];
            }

            var playFieldIndex = playField.Length - n;
            if (indexerNums >= nums.Length)
            {
                if (n > 0 && indexerNums > 0) cache[n, indexerNums] = 1;
                return 1;
            }

            long totalPossibilities = 0;
            var currentNum = nums[indexerNums];

            if (currentNum > n)
            {
                if (n > 0 && indexerNums > 0) cache[n, indexerNums] = 0;
                return 0;
            }

            var diff = n - (currentNum - 1);

            for (int i = 0; i < diff; i++)
            {
                if (playField[playFieldIndex + i] == '#' && !possibleWithPlayField(currentNum, playField, playFieldIndex + i))
                {
                    break;
                }

                if (!possibleWithPlayField(currentNum, playField, playFieldIndex+i))
                {
                    continue;
                }

                if (indexerNums == nums.Length - 1 && playField[playFieldIndex + i] == '#' && isLastNumAndStillHashtagsInPlayfield(currentNum, playField, playFieldIndex + i))
                {
                    break;
                }

                if (indexerNums == nums.Length - 1 &&  isLastNumAndStillHashtagsInPlayfield(currentNum, playField, playFieldIndex + i))
                {
                    continue; 
                }


                var stepPlayField = currentNum + 1 + i;
                var restNum = n - stepPlayField;
                totalPossibilities += amountOfPossibilities(restNum, nums, indexerNums+1, playField, cache);

                if (playField[playFieldIndex + i] == '#')
                {
                    break;
                }
            }

            cache[n, indexerNums] = totalPossibilities;
            return totalPossibilities;
        }

        private bool isLastNumAndStillHashtagsInPlayfield(int currentNum, string playField, int indexPlayfield)
        {
            for (int i = indexPlayfield + currentNum; i < playField.Length; i++)
            {
                if (playField[i] == '#') return true;
            }
            return false;
        }

        private bool possibleWithPlayField(int currentNum, string playField, int indexPlayfield)
        {
            for (int i = 0; i < currentNum; i++)
            {
                if (playField[indexPlayfield + i] == '.')
                {
                    return false;
                }
            }
            if ((indexPlayfield + currentNum < playField.Length && playField[indexPlayfield + currentNum] == '#') || (indexPlayfield > 0 && playField[indexPlayfield - 1] == '#'))
            {
                return false;
            }
            return true;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
