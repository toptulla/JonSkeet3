using System;
using System.Collections;
using System.Collections.Generic;

namespace JonSkeetBook.Co_6_1_Iterators
{
    /*
        Итератор - поведенческий паттерн проектирования (один из базовых шаблонов LINQ).

        Для упрощения коммуникации между объектами.

        Позволяет получать доступ ко всем элментам последовательности, не заботясь о том, что это за последовательность.
        
        Этот паттер хорошо подходит для реализации конвейера данных, когда элемент данных попадает в конвейер и проходит
        через множество различных преобразований или фильтров, перед выходом с другого конца конвейера.

        В .NET паттерн итератора инкапсулируется набором интерфейсов - IEnumerator и IEnumerable, а также их обобщенными
        эквивалентами.
        Если тип реализует IEnumerable, значит он допускает перебор с помощью итераций. Вызов метода GetEnumerator вернет
        реализацию интерфейса IEnumerator, которая и является итератором.

        Можно считать итератор чем-то вроде курсора БД - позиция в пределах последовательности.

        С последовательностью одновременно могут работать несколько итераторов.
    */

    class Program
    {
        static void Main()
        {
            Action<string> action = Console.WriteLine;

            var strings = new[] { "a", "b", "c", "d", "e", "f" };
            IEnumerator enumerator = strings.GetEnumerator();
            try
            {
                string s;
                while (enumerator.MoveNext())
                {
                    s = (string)enumerator.Current;
                    action(s);
                }
            }
            finally
            {

            }

            Console.ReadLine();
        }
    }
}
