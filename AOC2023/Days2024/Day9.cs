using System.Numerics;

namespace AOC2023.Days2024
{
    public class Day9 : Day
    {
        public override object RunA(string[] lines)
        {
            BigInteger sum = 0;
            char[] line = lines[0].ToCharArray();
            List<int> ints = line.Select(x => x - '0').ToList();
            List<(int, int)> docs = new List<(int, int)>();
            List<int> freeSpace = new List<int>();

            var docnums = 0;
            for (int i = 0; i < ints.Count; i++)
            {
                if (i % 2 == 1)
                {
                    freeSpace.Add(ints[i]);
                }
                else
                {
                    docs.Add((docnums, ints[i]));
                    docnums++;
                }
            }
            freeSpace.Add(0);
            List<(int, int)> iterationList = new List<(int, int)>(docs);

            int indexEnd = docs.Count - 1;
            int indexCurrentDoc = 0;

            while (indexEnd >= 0)
            {
                var (docNum, docSize) = iterationList[indexEnd];

                for (int i = 0; i < docs.Count; i++)
                {
                    if (indexEnd == docs[i].Item1)
                    {
                        indexCurrentDoc = i;
                    }
                }

                for (int i = 0; i < indexCurrentDoc; i++)
                {
                    if (freeSpace[i] >= docSize)
                    {
                           
                        docs.RemoveAt(indexCurrentDoc);
                        docs.Insert(i + 1, (docNum, docSize));


                        if (i == indexCurrentDoc - 1)
                        {
                            var oldSpace = freeSpace[i];
                            freeSpace[i] = 0;
                            freeSpace[indexCurrentDoc] = oldSpace + freeSpace[indexCurrentDoc];
                            // Adjust free space accordingly
                        }
                        else
                        {
                            var oldSpace = freeSpace[i];
                            freeSpace[i] = 0;
                            // Adjust free space accordingly
                            var afterDoc = freeSpace[indexCurrentDoc];
                            var beforeDoc = freeSpace[indexCurrentDoc - 1];
                            freeSpace[indexCurrentDoc - 1] = beforeDoc + docSize + afterDoc;
                            freeSpace.RemoveAt(indexCurrentDoc);
                            freeSpace.Insert(i + 1, oldSpace - docSize);
                        }
                            

                        break;
                    }
                }

                indexEnd--;
            }

            var indexer = 0;
            for (int i = 0; i < docs.Count; i++)
            {
                for (int j = 0; j < docs[i].Item2; j++)
                {
                    sum += indexer * docs[i].Item1;
                    indexer += 1;
                }
                indexer += freeSpace[i];
            }

            return sum;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
