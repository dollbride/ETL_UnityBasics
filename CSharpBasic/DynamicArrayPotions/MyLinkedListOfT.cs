using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArrayPotions
{
    internal class MyLinkedListNode<T>
    {
        public T? Value;
        public MyLinkedListNode<T>? Prev;
        public MyLinkedListNode<T>? Next;

        public MyLinkedListNode(T value)
        {
            Value = value;
        }
    }
    internal class MyLinkedList<T> : IEnumerable<T>
    {
        public MyLinkedListNode<T>? First => _first;
        public MyLinkedListNode<T>? Last => _last;
        public int Count => _count;

        private MyLinkedListNode<T>? _first, _last, _tmp;
        private int _count;

        #region 삽입 알고리즘 O(1)
        /// <summary>
        /// 가장 앞에 삽입
        /// </summary>
        /// <param name="value"></param>
        public void AddFirst(T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            // 하나 이상의 노드O => First가 존재한다
            if (_first != null)
            {
                _tmp.Next = _first;  // 방금 만든 노드(_tmp)의 다음 노드가 기존 First 노드
                _first.Prev = _tmp;
            }
            else  // 노드가 하나도 없을 때
            {
                _last = _tmp;
            }

            _first = _tmp;  // 기존 노드가 있건 말건 첫 번째를 갱신함
            _count++;
        }

        /// <summary>
        /// 가장 뒤에 삽입
        /// </summary>
        /// <param name="value"></param>
        public void AddLast(T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (_last != null)
            {
                _tmp.Prev = _last;
                _last.Next = _tmp;
            }
            else  // 노드가 하나도 없을 때
            {
                _first = _tmp;
            }

            _last = _tmp;
            _count++;
        }

        /// <summary>
        /// 특정 노드 앞에 삽입
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public void AddBefore(MyLinkedListNode<T> node, T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            // 기준 노드 앞에 다른 노드가 있을 때
            if (node.Prev != null)
            {
                node.Prev.Next = _tmp;
                _tmp.Prev = node.Prev;
            }
            // 기준 노드 앞에 다른 노드가 없을 때. 즉, AddFisrt인 상황
            else
            {
                _first = _tmp;
            }

            node.Prev = _tmp;
            _tmp.Next = node;
            _count++;
        }

        public void AddAfter(MyLinkedListNode<T> node, T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            // 기준 노드 뒤에 다른 노드가 있을 때
            if (node.Next != null)
            {
                node.Next.Prev = _tmp;
                _tmp.Next = node.Next;
            }
            // 기준 노드 뒤에 다른 노드가 없을 때. 즉, AddLast인 상황
            else
            {
                _last = _tmp;
            }

            node.Next = _tmp;
            _tmp.Prev = node;
            _count++;
        }

        #endregion

        #region 탐색 알고리즘

        // 보통 앞에서부터 찾는 게 Find()
        public MyLinkedListNode<T> Find(Predicate<T> match)
        {
            _tmp = _first;

            while (_tmp != null)
            {
                if (match(_tmp.Value))
                    return _tmp;

                _tmp = _tmp.Next;
            }

            return null;
        }

        public MyLinkedListNode<T> FindLast(Predicate<T> match)
        {
            _tmp = _last;

            while (_tmp != null)
            {
                if (match(_tmp.Value))
                    return _tmp;

                _tmp = _tmp.Prev;
            }

            return null;
        }

        #endregion

        #region 삭제 알고리즘

        public bool Remove(MyLinkedListNode<T> node)
        {
            if (node == null) 
                return false;

            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            // 내가 지우려는 노드의 앞에 아무것도 없을 때(RemoveFirst)
            else
            {
                _first = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            // 내가 지우려는 노드의 뒤에 아무것도 없을 때(RemoveLast)
            else
            {
                _last = node.Prev;
            }
            _count--;
            return true;
        }

        public bool Remove(T value)
        {
            return Remove(Find(x => x.Equals(value))); 
            //오브젝트 타입으로 비교하는 거라 박싱 언박싱이 일어남
            //그래서 where T : IEquatable <T>을 걸어서 오버라이드 하기도 하고
            //또는 클래스에 where T : IComparable <T> 걸어둔 다음
            //Remove(Find(x => x.CompareTo(value) == 0)); 이렇게 써고 됨
        }

        public bool RemoveLast(T value)
        {
            return Remove(FindLast(x => x.Equals(value)));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            public T Current => _node.Value;

            object IEnumerator.Current => _node.Value;

            private MyLinkedList<T> _data;  //책 참조 생성
            private MyLinkedListNode<T>? _node;  // 현재 페이지(노드)
            private MyLinkedListNode<T> _error;


            public Enumerator(MyLinkedList<T> data)
            {
                _data = data;
                _node = _error = new MyLinkedListNode<T>(default);  // 인덱스 -1 대신 노드에서는 null로 시작
            }

            // 책 읽을 때 필요했던 자원들(리소스)을 메모리에서 해제하는 내용을 구현하는 부분
            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            // 다음 페이지로
            public bool MoveNext()
            {
                if (_node == null)
                    return false;

                _node = _node == _error ? _data._first : _node.Next;
                return _node != null;
            }

            // 책 덮기
            public void Reset()
            {
                _node = _error;
            }
        }

        #endregion



    }
}
