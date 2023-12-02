using System.Runtime.CompilerServices;

namespace AOC2023.Days
{
    public class Day1 : Day
    {
        public Day1() { }
        public override object RunA(string[] lines)
        {
            var total = 0;
            foreach (string line in lines)
            { 
                var frst = line.First(x => int.TryParse(x.ToString(), out int value));
                var lst = line.Last(x => int.TryParse(x.ToString(), out int value));
                var numstr = new string(new char[] { frst, lst });
                total += int.Parse(numstr);
            }
            return total;
        }
        public override object RunB(string[] lines)
        {
            var total = 0;
            foreach (string line in lines)
            {
                char front = 'i';
                for (int i = 0; i < line.Length; i++)
                {
                    char valFront = testForNumber(i, line);
                    if (valFront != 'n')
                    {
                        front = valFront;
                        break;
                    }
                }

                char back = 'i';
                for (int i = line.Length-1; i >= 0; i--)
                {
                    char valBack = testForNumber(i, line);
                    if (valBack != 'n')
                    {
                        back = valBack;
                        break;
                    }
                }
                total += int.Parse(new string(new char[] { front, back }));
            }
            return total;
        }

        private char testForNumber(int i, string line)
        {
            if (int.TryParse(line[i].ToString(), out int result))
            {
                return line[i];
            }

            if (checkWord(line, i, "one")) return '1';
            if (checkWord(line, i, "two")) return '2';
            if (checkWord(line, i, "three")) return '3';
            if (checkWord(line, i, "four")) return '4';
            if (checkWord(line, i, "five")) return '5';
            if (checkWord(line, i, "six")) return '6';
            if (checkWord(line, i, "seven")) return '7';
            if (checkWord(line, i, "eight")) return '8';
            if (checkWord(line, i, "nine")) return '9';
            if (checkWord(line, i, "zero")) return '0';

            return 'n';
        }

        private bool checkWord(string line, int i, string word)
        {
            for (int j = 0; j < word.Length; j++)
            {
                if ((i + j >= line.Length) || (line[i + j] != word[j])) 
                    return false;
            }
            return true;
        }
    }
}
