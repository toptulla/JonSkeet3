using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JonSkeetBook.Co_6_1_Iterators
{
    /*
        Итератор - поведенческий паттерн проектирования (один из базовых шаблонов LINQ).

        Для упрощения коммуникации между объектами.

        Позволяет получать доступ ко всем элментам последовательности, не заботясь о том, что это за последовательность.
        
        Этот паттерн хорошо подходит для реализации конвейера данных, когда элемент данных попадает в конвейер и проходит
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
            var c = new MyCollection<int>(new[] { 1, 2, 3, 4 }, 3);
            foreach (var item in c)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }

    class MyCollection<T> : IEnumerable<T>
    {
        private T[] _values;
        private int _startIndex;

        public MyCollection(T[] values, int startIndex)
        {
            _values = values;
            _startIndex = startIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyCollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyCollectionEnumerator(this);
        }

        private class MyCollectionEnumerator : IEnumerator<T>
        {
            private MyCollection<T> _myCollection;
            private int _index;

            public MyCollectionEnumerator(MyCollection<T> myCollection)
            {
                _myCollection = myCollection;
                _index = -1;
            }

            public T Current
            {
                get
                {
                    int index = _index + _myCollection._startIndex;
                    index = index % _myCollection._values.Length;
                    return _myCollection._values[index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    int index = _index + _myCollection._startIndex;
                    index = index % _myCollection._values.Length;
                    return _myCollection._values[index];
                }
            }

            public void Dispose()
            {
                Console.WriteLine("Dispose");
            }

            public bool MoveNext()
            {
                if (_index != _myCollection._values.Length)
                {
                    _index++;
                }
                return _index < _myCollection._values.Length;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
