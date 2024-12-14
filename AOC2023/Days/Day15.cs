namespace AOC2023.Days
{
    public class Day15 : Day
    {
        public Day15() { }
        public override object RunA(string[] lines)
        {
            var values = lines[0].Split(',');
            var intermediateValues = new int[values.Length];
            for (int j = 0; j < values.Length; j++)
            {
                var value = values[j];
                intermediateValues[j] = HashMethod(value);
            }
            return intermediateValues.Sum();
        }

        private int HashMethod(string value)
        {
            var currentVal = 0;
            for (int i = 0; i < value.Length; i++)
            {
                currentVal += (int)value[i];
                currentVal *= 17;
                currentVal = 255 & currentVal;
            }
            return currentVal;
        }

        private void AddLabelOrReplace(Dictionary<int, List<string>> dictionary, string label, int focalLength)
        {
            var hashNum = HashMethod(label);

            for (var j = 0; j < dictionary[hashNum].Count; j++)
            {
                var lens = dictionary[hashNum][j];
                var splitOnSpace = lens.Split(' ');
                if (splitOnSpace[0] == label)
                {
                    dictionary[hashNum][j] = $"{label} {focalLength}";
                    return;
                }
            }

            dictionary[hashNum].Add($"{label} {focalLength}");
        }

        private void RemoveLabel(Dictionary<int, List<string>> dictionary, string label)
        {
            var hashNum = HashMethod(label);

            for (var j = 0; j < dictionary[hashNum].Count; j++)
            {
                var lens = dictionary[hashNum][j];
                var splitOnSpace = lens.Split(' ');
                if (splitOnSpace[0] == label)
                {
                    dictionary[hashNum].RemoveAt(j);
                    return;
                }
            }
        }

        public override object RunB(string[] lines)
        {
            var values = lines[0].Split(',');
            var boxDictionary = new Dictionary<int, List<string>>();
            for (int i = 0; i < 256; i++)
            {
                boxDictionary[i] = new List<string>();
            }

            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                var splitOnEq = value.Split('=');
                if (splitOnEq.Length == 2)
                {
                    // Is equal thingy
                    var label = splitOnEq[0];
                    var focalLength = int.Parse(splitOnEq[1]);
                    AddLabelOrReplace(boxDictionary, label, focalLength);
                }
                else
                {
                    // Minus thingy
                    var splitOnMin = value.Split('-');
                    var label = splitOnMin[0];
                    RemoveLabel(boxDictionary, label);
                }
            }

            var total = 0;
            foreach (var (boxNum, value) in boxDictionary)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    var focalLength = int.Parse(value[i].Split(' ')[1]);
                    total += (boxNum + 1) * (i + 1) * focalLength;
                }
            }

            return total;
        }
    }
}
