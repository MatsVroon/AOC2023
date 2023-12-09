using System.Collections.Generic;

namespace AOC2023.Days
{
    public class Day9 : Day
    {
        public Day9() { }
        public override object RunA(string[] lines)
        {
            var total = 0;
            foreach (string line in lines)
            {
                var numbers = line.Split(' ').Select(x => int.Parse(x)).ToList();
                var listOfLists = new List<List<int>>();
                listOfLists.Add(numbers);
                var currentDiffList = numbers;
                while (currentDiffList.Any(x => x != 0))
                {
                    var nextDiffList = new List<int>();
                    for (int i = 0; i < currentDiffList.Count - 1; i++)
                    {
                        var diff = currentDiffList[i + 1] - currentDiffList[i];
                        nextDiffList.Add(diff);
                    }
                    listOfLists.Add(nextDiffList);
                    currentDiffList = nextDiffList;
                }

                total += AddZeroToListOfLists(listOfLists);
            }

            return total;
        }

        private int AddZeroToListOfLists(List<List<int>> listOfLists)
        {
            var previous = 0;
            var indexer = listOfLists.Count - 2;
            while (indexer >= 0)
            {
                var currentList = listOfLists[indexer];
                var numberRight = currentList[0];
                previous = numberRight - previous;
                indexer--;
            }

            return previous;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }

    public class DiffList
    {
        public List<int> List { get; set; }
        public DiffList? ChildList { get; set; }

        public DiffList(List<int> list)
        {
            List = list;
            if (list.All(x => x == 0))
            {
                ChildList = null;
            }
            else
            {
                var childList = new List<int>(list.Count - 1);
                for (int i = 0; i < list.Count - 1; i++)
                {
                    var diff = Math.Abs(list[i] - list[i + 1]);
                    childList.Add(diff);
                }
                ChildList = new DiffList(list);
            }
        }
    }
}
