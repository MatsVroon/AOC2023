using AOC2023.Days;
using AOC2023;
using System.Globalization;
using System;
using System.Reflection;

namespace AOC2023
{
    public abstract class Day
    {
        public abstract object RunA(string[] lines);
        public abstract object RunB(string[] lines);

        public object Run(char ab, string[] lines)
        {
            var capitalChar = char.ToUpper(ab);
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod($"Run{capitalChar}");
            if (methodInfo == null) throw new Exception("Can't find function RunA or RunB");
            return methodInfo.Invoke(this, new object[] { lines });
        }
    }
}
