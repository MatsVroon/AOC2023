namespace AOC2023
{
    public class Printer
    {
        public static void Print(object obj)
        {
            Console.WriteLine(obj.ToString());
            PrintToFile(obj);
        }

        public static void PrintToFile(object obj)
        {
            var dateNow = DateTime.Now.ToShortDateString().Replace(':', '-');
            var timeNow = DateTime.Now.ToLongTimeString().Replace(':', '-');
            using (StreamWriter sw = new StreamWriter($"../../../Output/dayX_{dateNow}_{timeNow}.txt"))
            {
                sw.Write(obj.ToString());
            }
        }
    }
}
