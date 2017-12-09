using System;
using System.Collections;
using System.Collections.Generic;

namespace JonSkeetBook.Co_6_1_Iterators
{
    class CircleBuffer<T> : IEnumerable<T>
    {
        private readonly T[] _values;
        private readonly int _startIndex;

        public CircleBuffer(T[] values, int startIndex)
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
            private readonly CircleBuffer<T> _myCollection;
            private int _index;

            public MyCollectionEnumerator(CircleBuffer<T> myCollection)
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

            object IEnumerator.Current => Current;
            
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

            /// <summary>
            /// Этот метод будет вызван только в том случе, если итератор
            /// реализует IDisposable. (IEnumerator<T> включает интерфейс IDisposable)
            /// </summary>
            public void Dispose()
            {
                Console.WriteLine("Dispose");
            }
        }
    }


    public class MyCollection : IEnumerable
    {
        private readonly object[] _values;

        public MyCollection(object[] values)
        {
            _values = values;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        public class MyEnumerator : IEnumerator, IDisposable
        {
            private readonly MyCollection _collection;
            private int _index;
            private readonly int _initCollectionLength;

            public MyEnumerator(MyCollection collection)
            {
                _collection = collection;
                _index = -1;
                _initCollectionLength = _collection._values.Length;
            }

            public bool MoveNext()
            {
                if (_initCollectionLength != _collection._values.Length)
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");

                if (_index != _initCollectionLength)
                    _index++;

                return _index < _initCollectionLength;
            }

            public void Reset()
            {
                _index = -1;
            }

            public object Current
            {
                get
                {
                    if (_initCollectionLength != _collection._values.Length)
                        throw new InvalidOperationException("The collection was modified after the enumerator was created.");

                    return _collection._values[_index];
                }
            }

            /// <summary>
            /// Этот метод будет вызван только в том случе, если итератор
            /// реализует IDisposable
            /// </summary>
            public void Dispose()
            {
                Console.WriteLine("Dispose");
            }
        }
    }

    /// <summary>
    /// Для того, чтобы мы могли использовать foreach вовсе не обязательно реализовывать
    /// IEnumerable и IEnumerator, достаточно просто в коллекции реализовать метод
    /// GetEnumerator(), который вернет сущность, которая содержит свойство Current и
    /// метод MoveNext().
    /// </summary>
    public class WowCollection
    {
        private readonly int[] _values;

        public WowCollection(int[] values)
        {
            _values = values;
        }

        public WowEnumerator GetEnumerator()
        {
            return new WowEnumerator(this);
        }

        public struct WowEnumerator : IDisposable
        {
            private readonly WowCollection _wowCollection;
            private int _index;

            public WowEnumerator(WowCollection wowCollection)
            {
                _wowCollection = wowCollection;
                _index = -1;
            }

            public int Current => _wowCollection._values[_index];

            public bool MoveNext()
            {
                if (_index != _wowCollection._values.Length)
                    _index++;

                return _index < _wowCollection._values.Length;
            }

            public void Reset()
            {
                _index = -1;
            }

            /// <summary>
            /// Этот метод будет вызван только в том случе, если итератор
            /// реализует IDisposable
            /// </summary>
            public void Dispose()
            {

            }
        }
    }

    public class WowSharp2Iterable<T> : IEnumerable<T>
    {
        private readonly T[] _values;

        public WowSharp2Iterable(T[] values)
        {
            _values = values;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _values.Length; i++)
            {
                yield return _values[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}