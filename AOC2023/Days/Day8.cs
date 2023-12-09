using System.Runtime.CompilerServices;

namespace AOC2023.Days
{
    public class Day8 : Day
    {
        public Day8() { }
        public override object RunA(string[] lines)
        {
            var pathString = lines[0];
            var dictionary = new Dictionary<string, (string, string)>();
            var currents = new List<string>();
            for (int i = 2; i < lines.Length; i++)
            {
                var splitOnSpace = lines[i].Split(' ');
                var source = splitOnSpace[0];
                var leftWithSymbols = splitOnSpace[2];
                var left = new string(leftWithSymbols.Skip(1).SkipLast(1).ToArray());
                var rightWithSymbols = splitOnSpace[3];
                var right = new string(rightWithSymbols.SkipLast(1).ToArray());

                dictionary.Add(source, (left, right));

                if (source[2] == 'A') currents.Add(source);
            }


            var currentFirstZ = currents.Select(current =>
            {
                long indexer = 0;
                var relativeIndexer = 0;
                var currentIterator = current;
                // Find first z occurence

                while (currentIterator[2] != 'Z')
                {
                    var nextAction = pathString[relativeIndexer];
                    var tuple = dictionary[currentIterator];
                    if (nextAction == 'L')
                    {
                        currentIterator = tuple.Item1;
                    }
                    else
                    {
                        currentIterator = tuple.Item2;
                    }

                    indexer++;
                    relativeIndexer++;

                    if (relativeIndexer == pathString.Length) relativeIndexer = 0;
                }

                return (long)indexer;
            }).ToArray();

            long result = currentFirstZ.Aggregate((long)1, (acc, x) => LCM(acc, x));
            return result;
        }
        private long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        private long GCD(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
