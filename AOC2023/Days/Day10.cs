namespace AOC2023.Days
{
    public class Day10 : Day
    {
        public Day10() { }
        public override object RunA(string[] lines)
        {
            var coordinates = (0, 0);

            for (int Y = 0; Y < lines.Length; Y++)
            {
                for (int X = 0; X < lines[Y].Length; X++)
                {
                    if (lines[Y][X] == 'S')
                    {
                        coordinates = (X, Y);
                    }
                }
            }

            var searchOptions = new (Directions, int, int)[]
            {
                (Directions.Down,   0, -1),
                (Directions.Up,     0, 1),
                (Directions.Right, -1, 0),
                (Directions.Left,   1, 0)
            };

            var polygonPoints = new HashSet<(int, int)>();
            (Directions, int, int) currentCoordinate = (Directions.Up, coordinates.Item1, coordinates.Item2);
            foreach (var searchOption in searchOptions)
            {
                var checkX = coordinates.Item1 + searchOption.Item2;
                var checkY = coordinates.Item2 + searchOption.Item3;

                if (checkX < 0 || checkX >= lines[0].Length || checkY < 0 || checkY >= lines.Length)
                {
                    continue;
                }
                var currentChar = lines[checkY][checkX];

                if (searchOption.Item1 == Directions.Down && (currentChar == '|' || currentChar == '7' || currentChar == 'F'))
                {
                    currentCoordinate = (Directions.Down, checkX, checkY);
                    polygonPoints.Add((checkX, checkY));
                    break;
                }

                if (searchOption.Item1 == Directions.Up && (currentChar == '|' || currentChar == 'L' || currentChar == 'J'))
                {
                    currentCoordinate = (Directions.Up, checkX, checkY);
                    polygonPoints.Add((checkX, checkY));
                    break;
                }

                if (searchOption.Item1 == Directions.Left && (currentChar == '-' || currentChar == '7' || currentChar == 'J'))
                {
                    currentCoordinate = (Directions.Left, checkX, checkY);
                    polygonPoints.Add((checkX, checkY));
                    break;
                }

                if (searchOption.Item1 == Directions.Right && (currentChar == '-' || currentChar == 'L' || currentChar == 'F'))
                {
                    currentCoordinate = (Directions.Right, checkX, checkY);
                    polygonPoints.Add((checkX, checkY));
                    break;
                }
            }

            while (currentCoordinate.Item2 != coordinates.Item1 || currentCoordinate.Item3 != coordinates.Item2)
            {
                (Directions, int, int) neXtCoordinate = (Directions.Up, coordinates.Item1, coordinates.Item2);
                var currentX = currentCoordinate.Item2;
                var currentY = currentCoordinate.Item3;
                var currentChar = lines[currentY][currentX];
                var prevDir = currentCoordinate.Item1;

                switch (currentChar)
                {
                    case 'J':
                        if (prevDir == Directions.Left)
                        {
                            neXtCoordinate = (Directions.Down, currentX, currentY - 1);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        else
                        {
                            neXtCoordinate = (Directions.Right, currentX - 1, currentY);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        break;
                    case '|':
                        if (prevDir == Directions.Down)
                        {
                            neXtCoordinate = (Directions.Down, currentX, currentY - 1);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        else
                        {
                            neXtCoordinate = (Directions.Up, currentX, currentY + 1);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        break;
                    case '-':
                        if (prevDir == Directions.Left)
                        {
                            neXtCoordinate = (Directions.Left, currentX + 1, currentY);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        else
                        {
                            neXtCoordinate = (Directions.Right, currentX - 1, currentY);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        break;
                    case 'L':
                        if (prevDir == Directions.Right)
                        {
                            neXtCoordinate = (Directions.Down, currentX, currentY - 1);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        else
                        {
                            neXtCoordinate = (Directions.Left, currentX + 1, currentY);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        break;
                    case '7':
                        if (prevDir == Directions.Down)
                        {
                            neXtCoordinate = (Directions.Right, currentX - 1, currentY);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        else
                        {
                            neXtCoordinate = (Directions.Up, currentX, currentY + 1);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        break;
                    case 'F':
                        if (prevDir == Directions.Down)
                        {
                            neXtCoordinate = (Directions.Left, currentX + 1, currentY);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        else
                        {
                            neXtCoordinate = (Directions.Up, currentX, currentY + 1);
                            polygonPoints.Add((neXtCoordinate.Item2, neXtCoordinate.Item3));
                        }
                        break;
                }
                currentCoordinate = neXtCoordinate;
            }

            var surface = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                var insidePolygon = false;
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (insidePolygon && !polygonPoints.Contains((x, y)))
                    {
                        surface++;
                    }

                    if (polygonPoints.Contains((x, y)))
                    {
                        var curChar = lines[y][x];
                        var fEventuallyJ = FEventuallyJ(x, y, lines);
                        var lEventually7 = LEventually7(x, y, lines);
                        if (curChar == '|' || fEventuallyJ > 0 || lEventually7 > 0)
                        {
                            insidePolygon = !insidePolygon;
                        }
                    }
                }
            }

            return surface;
        }

        private int FEventuallyJ(int startX, int startY, string[] lines)
        {
            if (lines[startY][startX] != 'F')
            {
                return -1;
            }

            int i = 1; 
            while (lines[startY][startX + i] != 'J')
            {
                if (lines[startY][startX + i] != '-')
                {
                    return -1;
                }
                i++;
            }
            return i;
        }

        private int LEventually7(int startX, int startY, string[] lines)
        {
            if (lines[startY][startX] != 'L')
            {
                return -1;
            }

            int i = 1;
            while (lines[startY][startX + i] != '7')
            {
                if (lines[startY][startX + i] != '-')
                {
                    return -1;
                }
                i++;
            }
            return i;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }

    }

    public enum Directions
    {
        Up, Down, Right, Left
    }
}
