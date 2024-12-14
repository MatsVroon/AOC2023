using System.Runtime.Versioning;

namespace AOC2023.Days
{
    public class Day14 : Day
    {
        public Day14() { }
        public override object RunA(string[] lines)
        {
            var total = 0;
            for (int x = 0; x < lines[0].Length; x++)
            {
                var pointsNorthEdge = lines.Length;
                var northEdge = 0;
                for (int y = 0; y < lines.Length; y++)
                {
                    var currentChar = lines[y][x];

                    if (currentChar == 'O')
                    {
                        total += pointsNorthEdge;
                        northEdge++;
                        pointsNorthEdge--;
                    }

                    if (currentChar == '#')
                    {
                        northEdge = y + 1;
                        pointsNorthEdge = lines.Length - y - 1;
                    }
                }
            }

            return total;
        }

        private char[,] moveRocksNorth(char[,] lines)
        {
            //for (int i = 0; i < lines.Length; i++) { for (int j = 0; j < lines[0].Length; j++) { newLines[i,j] = '.' } }

            for (int x = 0; x < lines.GetLength(1); x++)
            {
                var northEdge = 0;
                for (int y = 0; y < lines.GetLength(0); y++)
                {
                    var currentChar = lines[y,x];

                    if (currentChar == 'O')
                    {
                        lines[y, x] = '.';
                        lines[northEdge, x] = currentChar;
                        northEdge++;
                    }

                    if (currentChar == '#')
                    {
                        lines[y, x] = '#';
                        northEdge = y + 1;
                    }
                }
            }
            return lines;
        }

        private char[,] rotateMatrixClockwise(char[,] lines)
        {
            var newMatrix = new char[lines.GetLength(1), lines.GetLength(0)];
            for (int y = 0; y < lines.GetLength(0); y++)
            {
                for (int x = 0; x < lines.GetLength(1); x++)
                {
                    var getlength = lines.GetLength(0);
                    newMatrix[x, lines.GetLength(0) - 1 - y] = lines[y, x];
                }
            }
            return newMatrix;
        }

        private char[,] doCycle(char[,] lines)
        {
            var current = lines;
            for (int i = 0; i < 4; i++)
            {
                var moveUp = moveRocksNorth(current);
                current = rotateMatrixClockwise(moveUp);
            }

            return current;
        }

        private int calculateWeight(char[,] lines)
        {
            var total = 0;
            for (int y = 0; y < lines.GetLength(0); y++)
            {
                for (int x = 0; x < lines.GetLength(1); x++)
                {
                    if (lines[y, x] == 'O')
                    {
                        total += lines.GetLength(0) - y;
                    }
                }
            }
            return total;
        }

        private string getMatrixInString(char[,] lines)
        {
            string accum = "";
            for (int y = 0; y < lines.GetLength(0); y++)
            {
                for (int x = 0; x < lines.GetLength(1); x++)
                {
                    if (lines[y, x] == 'O')
                    {
                        accum += $"({x},{y})";
                    }
                }
            }
            return accum;
        }


        public override object RunB(string[] liness)
        {
            char[,] lines = new char[liness.Length, liness[0].Length];
            for (int y = 0; y < liness.Length; y++)
            {
                for (int x= 0; x < liness[0].Length; x++)
                {
                    lines[y, x] = liness[y][x];
                }
            }

            var current = lines;
            var seenStates = new HashSet<string>();
            seenStates.Add(getMatrixInString(current));
            var cycle = false;
            int i = 0;
            while (!cycle)
            {
                current = doCycle(current);
                if (seenStates.Contains(getMatrixInString(current)))
                {
                    cycle = true;
                    Console.WriteLine("CYCLE ABOVE, FIND THE STRING BELOW AS STARTER");
                    Console.WriteLine($"Cycle {i}: {calculateWeight(current)}");
                }
                else
                {
                    Console.WriteLine($"Cycle {i}: {calculateWeight(current)}");
                    seenStates.Add(getMatrixInString(current));
                }
                i++;
            }

            return "DONE";
            //return calculateWeight(current);
        }
    }
}
