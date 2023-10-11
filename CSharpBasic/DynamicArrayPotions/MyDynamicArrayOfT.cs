using System.Collections;

namespace DynamicArrayPotions
{
    internal class MyDynamicArray<T> : IEnumerable<T>
        // where 제한자 : 타입을 제한하는 한정자 (T에 넣을 타입은 IComparable<T>로 공변 가능해야 한다)
        // IComparable에서 구현하고 있는 모든 기능들을 쓸 수 있다.
        where T : IComparable<T>
    {
        //시간복잡도 O(1) : 딱 한 번만 연산
        public T this[int index]
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
        private T[] _items = new T[DEFAULT_SIZE];

        // 시간복잡도: O(N), 빅오 노테이션은 최악의 경우만 고려하니까.
        // 평상시에는 아이템을 가장 마지막 인덱스에 추가하면 되지만
        // 최악의 경우는 공간이 모자랄 경우이기 때문에 더 큰 배열을 만들어서
        // 아이템들을 복제해야 하므로 자료개수에 비례한 연산이 필요하다.
        // 공간복잡도: O(N) 
        public void Add(T item)
        {
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, tmp, _count);
                _items = tmp;
            }
            _items[_count++] = item;
        }
        // 매치 조건 탐색
        // O(N) : 최악의 경우 아이템을 못 찾게 되면
        // 처음부터 끝까지 순회해야 하므로 자료 개수에 비례한 연산이 필요함.
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                //Default 비교연산 (C# 기본제공 비교연산자 쓸 때)
                if (Comparer<T>.Default.Compare(_items[i], item) == 0)
                    return true;

                //IComparable 비교연산 (내가 비교연산 내용을 직접 구현해서 쓸 때)
                if (item.CompareTo(_items[i]) == 0)
                    return true;
            }
            return false;
        }

        // 인덱스 삭제 시간 복잡도: O(N): N-1인데 상수 생략
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

        // 삭제 시간 복잡도: O(N): 2N인데 상수 생략
        public bool Remove(T item)
        {
            int index = FindIndex(x => item.CompareTo(x) == 0);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        // 책 읽어주는 자
        public struct Enumerator : IEnumerator<T>
        {
            public T Current => _data[_index];

            object IEnumerator.Current => throw new NotImplementedException();

            private MyDynamicArray<T> _data; // 책 참조 생성
            private int _index; // 책의 현재 페이지

            public Enumerator(MyDynamicArray<T> data)
            {
                _data = data;
                _index = -1; // 0을 쓰면 첫 페이지부터 열어서 주는 것.
                // -1을 써서 책 표지 덮은 상태로 시작
            }

            // 책 읽을 때 필요했던 자원들(리소스)을 메모리에서 해제하는 내용을 구현하는 부분
            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            // 다음 페이지로
            public bool MoveNext()
            {
                // 넘길 수 있는 다음 장이 존재한다면 다음 장으로 넘기고 true 반환
                if (_index < _data._count - 1)
                {
                    _index++;
                    return true;
                }

                return false;    
            }

            // 책 덮기
            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
