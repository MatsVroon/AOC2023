using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace AOC2023.Days
{
    public class Day2 : Day
    {
        public Day2() { }
        public override object RunA(string[] lines)
        {
            int total = 0;

            foreach (string line in lines)
            {
                total += doTheGame(line);
            }
            return total;
        }

        private int doTheGame(string line)
        {
            var gameIdGameSplit = line.Split(':');
            var gameIdString = gameIdGameSplit[0];
            int gameId = getGameId(gameIdString);
            var game = gameIdGameSplit[1];
            var turns = game.Split(';');
            var red = 1;
            var blue = 1;
            var green = 1;
            foreach (var turn in turns)
            {
                var colors = turn.Split(',');
                foreach (var color in colors)
                {
                    var numbercolor = color.Split(' ');
                    var number = int.Parse(numbercolor[1]);
                    var colorr = numbercolor[2];

                    switch (colorr)
                    {
                        case "red":
                            red = red < number ? number : red;
                            break;
                        case "blue":
                            blue = blue < number ? number : blue;
                            break;
                        case "green":
                            green = green < number ? number : green;
                            break;
                    }
                }
            }

            return red * blue * green;
        }

        private int getGameId(string s)
        {
            var splitted = s.Split(' ');
            return int.Parse(splitted[1]);
        }

        public override object RunB(string[] lines)
        {
            int total = 0;

            foreach (string line in lines)
            {
                total += doTheGame(line);
            }
            return total;
        }
    }
}
