using System.Diagnostics;

namespace AOC2023.Days
{
    public class Day6 : Day
    {
        public Day6() { }
        public override object RunA(string[] lines)
        {
            var timeLine = lines[0];
            var timeSplitted = timeLine.Split(' ');
            var timeGames = timeSplitted.Skip(1).Where(x => x != "").Select(s => int.Parse(s)).ToArray();
            var distanceLine = lines[1];
            var distanceSplitted = distanceLine.Split(' ');
            var distanceGames = distanceSplitted.Skip(1).Where(x => x != "").Select(s => int.Parse(s)).ToArray();

            int total = 1;
            for (int i = 0; i < timeGames.Count(); i++)
            {
                int waysOfWinning = 0;
                for (int speed = 1; speed < timeGames[i]; speed++)
                {
                    var timeLeft = timeGames[i] - speed;
                    var distanceGame = timeLeft * speed;
                    if (distanceGame > distanceGames[i])
                    {
                        waysOfWinning++;
                    }
                }

                total = waysOfWinning > 0 ? total * waysOfWinning : total;
            }

            return total;
        }

        public override object RunB(string[] lines)
        {
            var timeLine = lines[0];
            var timeSplitted = timeLine.Split(':');
            long time = long.Parse(String.Concat(timeSplitted[1].Where(c => !Char.IsWhiteSpace(c))));
            var distanceLine = lines[1];
            var distanceSplitted = distanceLine.Split(':');
            long distance = long.Parse(String.Concat(distanceSplitted[1].Where(c => !Char.IsWhiteSpace(c))));

            var abcAnswer = abcFormula(1, -time, distance);

            var bottom = Math.Ceiling(abcAnswer.x2);
            var top = Math.Floor(abcAnswer.x1);
            var answer = top - bottom + 1;
            return answer;
        }

        private abcAnswer abcFormula(double valA, double valB, double valC)
        {
            var D = Math.Pow(valB, 2) - 4 * valA * valC;
            if (D < 0)
            {
                return new abcAnswer() { amountOfSol = 0 };
            }
            var denominator = 2 * valA;
            var sqrtD = Math.Sqrt(D);
            var x1 = (-valB + sqrtD) / denominator;
            if (D == 0) 
            { 
                return new abcAnswer() { amountOfSol = 1, x1 = x1 };
            }

            var x2 = (-valB - sqrtD) / denominator;
            return new abcAnswer() { amountOfSol = 2, x1 = x1, x2 = x2 };
        }

    }

    public class abcAnswer
    {
        public int amountOfSol { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
    }
}
