namespace Collections
{
    internal class MyDynamicArray
    {
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }
        public int Count => _count;
        public int Capacity => _items.Length;

        private int _count;
        private const int DEFAULT_SIZE = 1;
        private object[] _items = new object[DEFAULT_SIZE];

        public void Add(object item)
        {
            if (_count >= _items.Length)
            {
                object[] tmp = new object[_count * 2];
                Array.Copy(_items, tmp, _count);
                _items = tmp;
            }
            _items[_count++] = item;
        }
        
        public object Find(Predicate<object> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }
       
        public int FindIndex(Predicate<object> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        public bool Contains(object item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i] == item)
                    return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
        }

        public bool Remove(object item)
        {
            int index = FindIndex(x => x == item);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }
    }
}
