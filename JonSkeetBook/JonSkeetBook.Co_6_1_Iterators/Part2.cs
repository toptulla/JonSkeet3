using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// Ленивая фильтрация с использованием блока итератора и предиката
        /// </summary>
        public static void Example3()
        {
            IEnumerable<string> lines = MyFileReader.ReadLines(@"..\..\Part2.cs");
            Predicate<string> predicate = delegate(string line)
            {
                return line.StartsWith("using");
            };
            foreach (string line in LazyLinq.Where(lines, predicate))
            {
                Console.WriteLine(line);
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


    class MyFileReader
    {
        public static IEnumerable<string> ReadLines(string fileName)
        {
            return ReadLines(delegate
            {
                return new StreamReader(fileName, Encoding.UTF8);
            });
        }

        private static IEnumerable<string> ReadLines(Func<StreamReader> provider)
        {
            using (StreamReader tr = provider())
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }


    /*
        Ленивое (отложенное) выполнение!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     * 
        В этом примере реализация разбита на две части: проверка достоверности аргументов и дей-
        ствительная бизнес-логика фильтрации. Это немного неуклюже, но совершенно необходимо для
        разумной обработки ошибок. Предположим, что вы поместили все в один метод. Что произойдет
        при вызове Where<string>(null, null)? Ответ: ничего... или, во всяком случае, желаемое
        исключение не будет сгенерировано. Причина кроется в семантике ленивого выполнения итера-
        торных блоков: код в теле метода не запускается до тех пор, пока метод MoveNext() не будет
        вызван первый раз, как объяснялось в разделе 6.2.2. Обычно проверять предусловия необходимо
        энергичным образом — нет никакого смысла в откладывании генерации исключения, т.к. это
        только усложнит отладку.
    */

    class LazyLinq
    {
        public static IEnumerable<T> Where<T>(IEnumerable<T> source, Predicate<T> predicate)
        {
            if(source == null || predicate == null)
                throw new ArgumentException();

            return WhereImpl(source, predicate);
        }

        private static IEnumerable<T> WhereImpl<T>(IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }
}
