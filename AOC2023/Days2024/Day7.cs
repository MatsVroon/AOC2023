using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Days2024
{
    public class Day7 : Day
    {
        public override object RunA(string[] lines)
        {
            BigInteger sum = 0;
            foreach (var line in lines)
            {
                var split = line.Split(": ");
                var testValue = BigInteger.Parse(split[0]);
                var equation = split[1].Split().Select(x => BigInteger.Parse(x)).ToList();


                var decTree = createDecisionTree(1, equation, equation[0]);
                var leaves = getLeaves(decTree);
                if (leaves.Any(x => x.CurrentValue == testValue))
                {
                    sum += testValue;
                }
            }


            return sum;
        }

        public DecisionTree createDecisionTree(int index, List<BigInteger> equation, BigInteger thisValue)
        {
            if (index == equation.Count)
            {
                return new DecisionTree() { CurrentValue = thisValue };
            }

            var newNode = new DecisionTree() { CurrentValue = thisValue };
            BigInteger nextValue = equation[index];
            newNode.Plus = createDecisionTree(index + 1, equation, thisValue + nextValue);
            newNode.Times = createDecisionTree(index + 1, equation, thisValue * nextValue);
            newNode.Concat = createDecisionTree(index + 1, equation, concatIntegers(thisValue, nextValue));

            return newNode;
        }

        public BigInteger concatIntegers(BigInteger a, BigInteger b)
        {
            if (b < 10)
            {
                return a * 10 + b;
            }
            else if (b < 100)
            {
                return a * 100 + b;
            }
            else 
            {
                // b < 1000
                return a * 1000 + b;
            }
        }

        public List<DecisionTree> getLeaves(DecisionTree tree)
        {
            if (tree.Plus == null && tree.Times == null && tree.Concat == null)
            {
                return new List<DecisionTree>(new[] { tree });
            }
            else
            {
                return getLeaves(tree.Plus).Concat(getLeaves(tree.Times)).Concat(getLeaves(tree.Concat)).ToList();
            }
        }

        public class DecisionTree
        {
            public BigInteger CurrentValue { get; set; }
            public DecisionTree Plus { get; set; }
            public DecisionTree Times { get; set; }
            public DecisionTree Concat { get; set; }
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
