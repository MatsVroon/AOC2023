namespace AOC2023
{
    public class Parser
    {
        public static string[] ParseInput(int day, char ab = 'a')
        {
            return File.ReadAllLines($"{Directory.GetCurrentDirectory()}/Input/inputDay{day}{ab}.txt");
        }
        public static string[] ParseInput(char ab = 'a')
        {
            int day = DateTime.Now.Day;
            return ParseInput(day, ab);
        }
    }
}
