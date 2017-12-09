using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonSkeetBook.Co_6_1_Iterators
{
    /// <summary>
    /// Реальные примеры использования итераторов
    /// </summary>
    static class Part2
    {
        /// <summary>
        /// Итерация по датам в расписании
        /// </summary>
        public static void Example1()
        {
            var timeTable = new TimeTable
            {
                StartDate = new DateTime(2018, 1, 1),
                EndDate = new DateTime(2018, 1, 9),
            };

            // Не хочется такое писать каждый раз
            for (DateTime day = timeTable.StartDate;
                day <= timeTable.EndDate;
                day = day.AddDays(1))
            {
                Console.WriteLine(day);
            }

            Console.WriteLine();

            // Так намного лучше
            foreach (DateTime day in timeTable.Range)
            {
                Console.WriteLine(day);
            }
        }
    }

    class TimeTable
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<DateTime> Range
        {
            get
            {
                for (DateTime day = StartDate;
                    day <= EndDate;
                    day = day.AddDays(1))
                {
                    yield return day;
                }
            }
        }
    }


}
