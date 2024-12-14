using System.Text.RegularExpressions;

namespace AOC2023.Days2024
{
    public class Day3: Day
    {
        public override object RunA(string[] lines)
        {
            Int64 sum = 0;
            bool enabled = true;
            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, "(mul\\(([1-9]|[1-9][0-9]{1,2}),([1-9]|[1-9][0-9]{1,2})\\))|(do\\(\\))|(don't\\(\\))");
                foreach (var match in matches.ToList())
                {
                    if (match.Value == "don't()")
                    {
                        enabled = false;
                        continue;
                    }
                    if (match.Value == "do()")
                    {
                        enabled = true;
                        continue;
                    }
                    if (enabled)
                    {
                        int num1 = int.Parse(match.Groups[2].Value);
                        int num2 = int.Parse(match.Groups[3].Value);
                        sum += num1 * num2;
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
