using System.Data;
using System.Runtime.CompilerServices;

namespace AOC2023.Days2024
{
    public class Day5 : Day
    {
        public override object RunA(string[] lines)
        {
            var dependencies = new Dictionary<int, List<int>>();
            int indexer = 0;
            // Parse dependencies
            foreach (var line in lines)
            {
                if (line == "")
                {
                    indexer++;
                    break;
                }

                var split = line.Split("|");
                if (dependencies.ContainsKey(int.Parse(split[0])))
                {
                    dependencies[int.Parse(split[0])].Add(int.Parse(split[1]));
                }
                else
                {
                    dependencies.Add(int.Parse(split[0]), new List<int>(new[] { int.Parse(split[1]) }));
                }
                indexer++;
            }

            int sum = 0;
            // Parse rule boos
            for (int i = indexer; i < lines.Length; i++)
            {
                var rule = lines[i].Split(",");
                if (!checkDeps(dependencies, rule))
                {
                    // create graph
                    var itemToNode = new Dictionary<int, Node>();
                    foreach (var itemStr in rule)
                    {
                        itemToNode.Add(int.Parse(itemStr), new Node(int.Parse(itemStr)));
                    }

                    foreach (var itemStr in rule)
                    {
                        int item = int.Parse(itemStr);
                        if (dependencies.ContainsKey(item))
                        {
                            var depList = dependencies[item];
                            foreach (var dep in depList)
                            {
                                if (itemToNode.ContainsKey(dep))
                                {
                                    itemToNode[item].Next.Add(itemToNode[dep]);
                                    itemToNode[dep].Previous.Add(itemToNode[item]);
                                }
                            }
                        }
                    }

                    var newOrder = new List<int>();
                    while(newOrder.Count < rule.Length)
                    {
                        foreach (var item in itemToNode.Keys)
                        {
                            if (!itemToNode[item].AlreadyAdded)
                            {
                                if (itemToNode[item].Previous.Count == 0)
                                {
                                    itemToNode[item].AlreadyAdded = true;
                                    newOrder.Add(item);
                                    foreach (var nextItems in itemToNode[item].Next)
                                    {
                                        nextItems.Previous.Remove(itemToNode[item]);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    sum += newOrder[newOrder.Count / 2];
                }
            }

            return sum;
        }


        private bool checkDeps(Dictionary<int, List<int>> dependencies, string[] rule)
        {
            var itemIndex = new Dictionary<int, int>();
            for (int j = 0; j < rule.Length; j++)
            {
                itemIndex.Add(int.Parse(rule[j]), j);
            }

            for (int j = 0; j < rule.Length; j++)
            {
                var currentItem = int.Parse(rule[j]);
                if (dependencies.ContainsKey(currentItem))
                {
                    var depList = dependencies[currentItem];
                    foreach (var dep in depList)
                    {
                        if (itemIndex.ContainsKey(dep))
                        {
                            var depInd = itemIndex[dep];
                            if (depInd < j)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public List<Node> Next { get; set; }
        public List<Node> Previous { get; set; }
        public bool AlreadyAdded { get; set; }

        public Node(int val)
        {
            Value = val;
            Next = new List<Node>();
            Previous = new List<Node>();
            AlreadyAdded = false;
        }
    }
}
