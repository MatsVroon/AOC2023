namespace AOC2023.Days2024
{
    public class Day2 : Day
    {
        public override object RunA(string[] lines)
        {
            int count = 0;
            foreach (string line in lines)
            {
                var splitted = line.Split().ToList();
                for (int i = 0; i < splitted.Count - 1; i++)
                {
                    int num1 = int.Parse(splitted[i]);
                    int num2 = int.Parse(splitted[i + 1]);
                    int diff = num2 - num1;
                    if (diff <= 0 || diff > 3)
                    {
                        var newListA = new List<string>(splitted);
                        var newListB = new List<string>(splitted);
                        newListA.RemoveAt(i);
                        newListB.RemoveAt(i + 1);
                        if (isIncreasing(newListA.ToArray()) || isIncreasing(newListB.ToArray()))
                        {
                            count++;
                        }

                        break;
                    }
                    else if (i == splitted.Count - 2)
                    {
                        count++;
                    }
                }

                for (int i = 0; i < splitted.Count - 1; i++)
                {
                    int num1 = int.Parse(splitted[i]);
                    int num2 = int.Parse(splitted[i + 1]);
                    int diff = num2 - num1;
                    if (diff >= 0 || diff < -3)
                    {
                        var newListA = new List<string>(splitted);
                        var newListB = new List<string>(splitted);
                        newListA.RemoveAt(i);
                        newListB.RemoveAt(i + 1);
                        if (isDecreasing(newListA.ToArray()) || isDecreasing(newListB.ToArray()))
                        {
                            count++;
                        }

                        break;
                    }
                    else if (i == splitted.Count - 2)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool isIncreasing(string[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                int num1 = int.Parse(nums[i]);
                int num2 = int.Parse(nums[i + 1]);
                int diff = num2 - num1;
                if (diff <= 0 || diff > 3)
                    return false;
            }
            return true;
        }

        private bool isDecreasing(string[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                int num1 = int.Parse(nums[i]);
                int num2 = int.Parse(nums[i + 1]);
                int diff = num2 - num1;
                if (diff >= 0 || diff < -3)
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
