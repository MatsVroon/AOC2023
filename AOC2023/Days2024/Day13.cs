using System.Globalization;
using System.Numerics;
using System.Security;
using LpSolveDotNet;

namespace AOC2023.Days2024
{
    public class Day13 : Day
    {
        public override object RunA(string[] lines)
        {
            var groups = new List<List<int[]>>();
            groups.Add(new List<int[]>());
            var groupsCounter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    groupsCounter++;
                    groups.Add(new List<int[]>());
                }
                else
                {
                    var splitColon = lines[i].Split(": ");
                    var splitComma = splitColon[1].Split(", ");
                    var ints = new[] { 0, 0 };
                    if (splitComma[0].Contains('+'))
                    {
                        ints = splitComma.Select(x => int.Parse(x.Split("+")[1])).ToArray();
                    }
                    else
                    {
                        ints = splitComma.Select(x => int.Parse(x.Split("=")[1])).ToArray();
                    }

                    groups[groupsCounter].Add(ints);
                }
            }


            BigInteger sum = 0;
            var sum2 = 0;
            LpSolve.Init();
            
            for (int i = 0; i < groups.Count; i++)
            {
                //sum += UseBruteForce(groups[i]);
                sum += UseSolver(groups[i]);
            }
            return sum;
        }

        public BigInteger UseBruteForce(List<int[]> group)
        {
            Dictionary<BigInteger, (BigInteger, BigInteger)> valueToPairs = new Dictionary<BigInteger, (BigInteger, BigInteger)>();
            BigInteger newRhs1 = group[2][0];// + 10000000000000;
            BigInteger newRhs2 = group[2][1];// + 10000000000000;
            BigInteger upperboundB1 = newRhs1 / group[1][0];
            BigInteger upperboundB2 = newRhs2 / group[1][1];
            BigInteger upperboundB = upperboundB1 > upperboundB2 ? upperboundB2 : upperboundB1;

            for (BigInteger b = upperboundB; b >= 0; b--)
            {
                double testA = (double)(newRhs1 - b * group[1][0]) / (double)group[0][0];
                if (Math.Abs(testA % 1) <= (Double.Epsilon * 100))
                {
                    BigInteger testAInt = (BigInteger)testA;

                    if (b * group[1][1] + testAInt * group[0][1] == newRhs2)
                    {
                        return 3 * testAInt + b;
                    }
                }
            }
            return 0;
        }
        public BigInteger UseSolver(List<int[]> group)
        {
            var sum = 0;
            using (var instance = LpSolve.make_lp(0, 2))
            {
                var vals = new double[] { 0, 0, 0 };
                var vars = new double[] { 0, 0, 0 };
                double newRhs1 = group[2][0] + 10000000000000;
                double newRhs2 = group[2][1] + 10000000000000;
                instance.set_obj_fn(new[] { (double)0, (double)3, (double)1 });
                instance.add_constraint(new[] { (double)0, (double)group[0][0], (double)group[1][0] }, lpsolve_constr_types.EQ, newRhs1);
                instance.add_constraint(new[] { (double)0, (double)group[0][1], (double)group[1][1] }, lpsolve_constr_types.EQ, newRhs2);
                instance.set_int(1, true);
                instance.set_int(2, true);
                instance.set_int(0, true);

                instance.print_lp();

                var result = instance.solve();

                if (result != lpsolve_return.INFEASIBLE)
                {
                    instance.get_primal_solution(vals);
                    instance.get_variables(vars);

                    instance.print_solution(2);

                    if (vars[1] * group[1][1] + vars[0] * group[0][1] == newRhs2 && vars[1] * group[1][0] + vars[0] * group[0][0] == newRhs1)
                    {
                        return (BigInteger)(3 * vars[0] + vars[1]);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return sum;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
