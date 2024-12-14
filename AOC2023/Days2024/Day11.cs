using System.Globalization;
using System.Numerics;
using System.Security;

namespace AOC2023.Days2024
{
    public class Day11 : Day
    {
        Dictionary<(BigInteger, int), BigInteger> cache = new Dictionary<(BigInteger, int), BigInteger>();
        public override object RunA(string[] lines)
        {
            var line = lines[0];
            var split = line.Split();
            List<(BigInteger, int)> ints = split.Select(x => (BigInteger.Parse(x), 0)).ToList();

            BigInteger stones = 0;
            var currentIntList = new Stack<(BigInteger, int)>(ints);

            foreach (var (stone, _) in ints)
            {
                stones += getStones(stone, 0);
            }

            return stones;
        }

        public BigInteger getStones(BigInteger value, int iteration)
        {
            if (iteration == 75)
            {
                return 1;
            }

            if (cache.ContainsKey((value, iteration)))
            {
                return cache[(value, iteration)];
            }

            var stringVal = value.ToString();
            if (value == 0)
            {
                var stones = getStones(1, iteration + 1);
                cache.Add((value, iteration), stones);
                return stones;
            }
            else if (stringVal.Length % 2 == 0)
            {
                var stringValA = stringVal.Substring(0, stringVal.Length / 2);
                var stringValB = stringVal.Substring(stringVal.Length / 2);
                var intA = BigInteger.Parse(stringValA);
                var intB = BigInteger.Parse(stringValB);
                var stonesA = getStones(intA, iteration + 1);
                var stonesB = getStones(intB, iteration + 1);
                cache.Add((value, iteration), stonesA + stonesB);
                return stonesA + stonesB;
            }
            else
            {
                var stones = getStones(value * 2024, iteration + 1);
                cache.Add((value, iteration), stones);
                return stones;
            }
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
