using System;
using System.Collections.Generic;
using System.Text;

namespace ZadatakGIT
{
    public class Stack<T>:System.Collections.Generic.IEnumerable<T>
    {
        T[] _items = new T[0];
        int _size;

        public void Push(T item)
        {
            if (_size == _items.Length)
            {
                int newLength = _size == 0 ? 4 : _size * 2;
                T[] newArray = new T[newLength];
                _items.CopyTo(newArray, 0);
                _items = newArray;
            }
            _items[_size] = item;
            _size++;
        }

        public T Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack je prazan");
            }

            _size--;
            return _items[_size];
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack je prazan");
            }
            return _items[_size - 1];
        }

        public int Count
        {
            get 
            {
                return _size;
            }
        }

        public void Clear()
        {
            _size = 0;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (int i = _size - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
