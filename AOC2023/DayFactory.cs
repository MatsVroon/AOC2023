using System.Reflection;

namespace AOC2023
{
    public class DayFactory
    {
        public static Day CreateDayObject(int day) 
        {
            var dayType = Type.GetType($"AOC2023.Days.Day{day}");
            if (dayType == null) throw new Exception($"Day{day} type doesn't exist");

            var dayXObject = Activator.CreateInstance(dayType);
            if (dayXObject == null) throw new Exception($"Day{day} object doesn't exists");

            return (Day)dayXObject;
        }

        public static Day CreateDayObject()
        {
            var day = DateTime.Now.Day;
            return CreateDayObject(day);
        }
    }
}
