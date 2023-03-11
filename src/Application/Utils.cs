
namespace Application;

public static class Utils
{
    public static IEnumerable<DateTime> WeekDays(DateTime date, int dateCount)
    {
        DateTime[] dates = new DateTime[dateCount];
        int count = 0;
        int increment = 0;
        do{
            var calcDate = date.AddDays(-count);
            
            if(calcDate.DayOfWeek == DayOfWeek.Sunday || calcDate.DayOfWeek == DayOfWeek.Saturday){
                count++;
                continue;
            }
            count++;
            if(increment > (dateCount-1))
            {
                //Console.WriteLine("Increment breached");
                break;
            }

            //Console.WriteLine($"appending to position: {increment}");
            dates[increment] = calcDate;
            increment+=1;
        }while(dates[dateCount-1] != null);

        //Console.WriteLine($"Returning Dates");
        return dates.AsEnumerable();
    }
}