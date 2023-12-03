using System.Globalization;
using System.Xml.Schema;

namespace AOC2023.Days
{
    public class Day3 : Day
    {
        public Day3() { }
        public override object RunA(string[] lines)
        {
            var total = 0;
            //char[,] schematic = new char[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                int x = 0;
                while (x < lines[0].Length)
                {
                    string number = "";
                    int step = 0;
                    bool hasSymbol = false;
                    while (x + step < lines[y].Length && char.IsDigit(lines[y][x + step]))
                    {
                        number += lines[y][x + step].ToString();
                        hasSymbol |= checkSymbol(x + step, y, lines);
                        step++;
                    }
                    if (hasSymbol)
                    {
                        total += int.Parse(number);
                    }
                    x += step + 1;
                }
            }
            return total;
        }

        private bool checkSymbol(int x, int y, string[] lines)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (y + i >= 0 && x + j >= 0 && y + i < lines.Length && x + j < lines[0].Length && lines[y + i][x + j] != '.' && !char.IsLetterOrDigit(lines[y + i][x + j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override object RunB(string[] lines)
        {
            long total = 0;
            int[,] numMatrix = new int[lines.Length, lines[0].Length];
            for (int y = 0; y < lines.Length; y++)
            {
                string numberString = "";
                List<int> indices = new List<int>();
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (char.IsDigit(lines[y][x]))
                    {
                        indices.Add(x);
                        numberString += lines[y][x];
                    }
                    else
                    {
                        foreach (int index in indices)
                        {
                            numMatrix[y, index] = int.Parse(numberString);
                        }

                        numberString = "";
                        indices.Clear();
                    }
                }

                if (indices.Count > 0)
                {
                    foreach (int index in indices)
                    {
                        numMatrix[y, index] = int.Parse(numberString);
                    }
                }
            }

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '*')
                    {
                        List<int> nums = new List<int>();

                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {

                                var currentY = y + i;
                                var currentX = x + j;

                                if (currentY >= 0 && currentX >= 0 && currentY < lines.Length && currentX < lines[0].Length && numMatrix[currentY, currentX] > 0)
                                {
                                    nums.Add(numMatrix[currentY, currentX]);
                                    var currentValue = numMatrix[currentY, currentX];

                                    //79552824
                                    var neighbourLeft = currentX - 1;
                                    var neighbourRight = currentX + 1;
                                    numMatrix[currentY, currentX] = 0;

                                    for (int t = 0; t < lines[y].Length; t++)
                                    {
                                        if (neighbourLeft >= 0 && numMatrix[currentY, neighbourLeft] == currentValue)
                                        {
                                            numMatrix[currentY, neighbourLeft] = 0;
                                            neighbourLeft--;
                                        }

                                        if (neighbourRight < lines[0].Length && numMatrix[currentY, neighbourRight] == currentValue)
                                        {
                                            numMatrix[currentY, neighbourRight] = 0;
                                            neighbourRight++;
                                        }

                                        if ((neighbourRight >= lines[0].Length || numMatrix[currentY, neighbourRight] != currentValue) && (neighbourLeft < 0 || numMatrix[currentY, neighbourLeft] != currentValue))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (nums.Count == 2)
                        {
                            Console.WriteLine("DUO!");
                            long subtotal = 1;
                            foreach (int num in nums)
                            {
                                Console.WriteLine(num);
                                subtotal *= num;
                            }

                            total += subtotal;
                        }
                    }
                }
            }

            return total;
        }
    }
}
