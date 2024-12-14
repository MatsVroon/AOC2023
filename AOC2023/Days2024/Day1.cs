using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Days2024
{
    public class Day1 : Day
    {
        public override object RunA(string[] lines)
        {
            List<int> listA = new List<int>();
            List<int> listB = new List<int>();
            foreach (string line in lines)
            {
                var split = line.Split("   ");
                listA.Add(int.Parse(split[0]));
                listB.Add(int.Parse(split[1]));
            }
            Dictionary<int, int> listC = new Dictionary<int, int>();
            for (int i = 0; i < listB.Count; i++)
            {
                if (listC.ContainsKey(listB[i]))
                {
                    listC[listB[i]]++;
                }
                else
                {
                    listC.Add(listB[i], 1);
                }
            }

            int papidiepoepidie = 0;
            for(int j = 0; j < listA.Count; j++)
            {
                if (listC.ContainsKey(listA[j])) 
                {
                    papidiepoepidie += listA[j] * listC[listA[j]];
                }
            }
            return papidiepoepidie;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
