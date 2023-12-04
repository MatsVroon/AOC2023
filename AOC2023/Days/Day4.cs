namespace AOC2023.Days
{
    public class Day4 : Day
    {
        public Day4() { }
        public override object RunA(string[] lines)
        {
            int total = 0;
            foreach (string line in lines)
            {
                var gameIdGameSplit = line.Split(':');
                var gameIdString = gameIdGameSplit[0];
                var gameString = gameIdGameSplit[1];
                var numbersSplit= gameString.Split('|');
                var winningNumbersString = numbersSplit[0];
                var gameNumbersString = numbersSplit[1];

                var winningNumbersSet = new HashSet<int>();
                foreach (string posNum in winningNumbersString.Split(' ')) 
                {
                    if (posNum != "")
                    {
                        winningNumbersSet.Add(int.Parse(posNum));
                    }
                }

                int amountOfWinningNumbers = 0;
                foreach (string posGameNum in gameNumbersString.Split(' '))
                {
                    if (posGameNum != "")
                    {
                        if (winningNumbersSet.Contains(int.Parse(posGameNum)))
                        {
                            var power = amountOfWinningNumbers - 1 < 0 ? 0 : (amountOfWinningNumbers - 1);
                            total += (int)Math.Pow(2, power);
                            amountOfWinningNumbers++;
                        }
                    }
                }
            }
            return total;
        }

        public override object RunB(string[] lines)
        {
            int total = 0;
            int[] copies = Enumerable.Repeat(1, lines.Length).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var gameIdGameSplit = line.Split(':');
                var gameIdString = gameIdGameSplit[0];
                var gameString = gameIdGameSplit[1];
                var numbersSplit = gameString.Split('|');
                var winningNumbersString = numbersSplit[0];
                var gameNumbersString = numbersSplit[1];

                var winningNumbersSet = new HashSet<int>();
                foreach (string posNum in winningNumbersString.Split(' '))
                {
                    if (posNum != "")
                    {
                        winningNumbersSet.Add(int.Parse(posNum));
                    }
                }

                int amountOfWinningNumbers = 0;
                foreach (string posGameNum in gameNumbersString.Split(' '))
                {
                    if (posGameNum != "")
                    {
                        if (winningNumbersSet.Contains(int.Parse(posGameNum)))
                        {
                            amountOfWinningNumbers++;
                        }
                    }
                }

                for (int j = 1; j <= amountOfWinningNumbers; j++)
                {
                    copies[i + j] += copies[i];
                }
            }

            total = copies.Sum();
            return total;
        }
    }
}
