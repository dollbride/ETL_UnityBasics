using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArrayPotions
{
    public struct KeyValuePair<T, K> : IEquatable<KeyValuePair<T, K>> where T : IEquatable<T> where K : IEquatable<K>
    {
        public T? Key;
        public K? Value;

        public KeyValuePair(T? key, K? value)
        {
            Key = key;
            Value = value;
        }

        public bool Equals(KeyValuePair<T, K> other)
        {
            return other.Key.Equals(Key) && other.Value.Equals(Value);
        }
    }

    // TKey랑 TValue가 무슨 타입인지는 메인 프로그램에서 호출할 때 정의함
    internal class MyHashtable<TKey, TValue>
        where TKey : IEquatable<TKey>
        where TValue : IEquatable<TValue>
    {
        public TValue this[TKey abc]
        {
            get
            {
                var bucket = _buckets[Hash(abc)];
                // bucket은 리스트임. 아래랑 같은 거
                // List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

                if (bucket == null) 
                    throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}> : Key {abc} doesn't exist");

                for (int i  = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(abc))
                        return bucket[i].Value;
                }
                throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}> : Key {abc} doesn't exist");
            }
            set
            {
                var bucket = _buckets[Hash(abc)];

                if (bucket == null)
                    throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}> : Key {abc} doesn't exist");

                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(abc))
                        bucket[i] = new KeyValuePair<TKey, TValue>(abc, value);  
                    // 구조체 멤버를 바로 수정하면 안 돼서 구조체 생성자를 통해서 초기화 함
                }
                throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}> : Key {abc} doesn't exist");
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                // i가 세로 인덱스고 j가 jagged array에서의 가로 인덱스
                for (int i = 0; i < _validIndexList.Count; i++)
                {
                    for (int j = 0; j < _buckets[_validIndexList[i]].Count; j++)
                    {
                        keys.Add(_buckets[_validIndexList[i]][j].Key);
                    }
                }
                return keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                for (int i = 0; i < _validIndexList.Count; i++)
                {
                    for (int j = 0; j < _buckets[_validIndexList[i]].Count; j++)
                    {
                        values.Add(_buckets[_validIndexList[i]][j].Value);
                    }
                }
                return values;
            }
        }

        private const int DEFAULT_SIZE = 100;
        // KeyValuePair<TKey, TValue>에 있는 TKey, TValue도 위 클래스에서 지정한 타입과 같은 걸 가리킴
        private List<KeyValuePair<TKey, TValue>>[] _buckets 
            = new List<KeyValuePair<TKey, TValue>>[DEFAULT_SIZE];
        private List<int> _validIndexList = new List<int>();  // 등록된 Key 값이 있는 인덱스 목록

        public void Add(TKey key, TValue value)
        {
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            // 해당 인덱스에 버킷이 없으면 새로 만듦
            if (bucket == null)
            {
                _buckets[index] = new List<KeyValuePair<TKey, TValue>>();
                _validIndexList.Add(index);
            }
            else
            {
                // 버킷이 있으면 해당 버킷에 중복 Key가 있는지 확인
                // 해시테이블은 중복 허용을 안 하니까 true면 예외 던짐
                for(int i = 0; i < bucket.Count;i++) 
                { 
                    if (bucket[i].Key.Equals(key))
                        throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}> : Key {key} doesn't exist");
                }
            }

            bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool TryAdd(TKey key, TValue value)
        {
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            // 해당 인덱스에 버킷이 없으면 새로 만듦
            if (bucket == null)
            {
                _buckets[index] = new List<KeyValuePair<TKey, TValue>>();
                _validIndexList.Add(index);
            }
            else
            {
                // 버킷이 있으면 해당 버킷에 중복 Key가 있는지 확인
                // 해시테이블은 중복 허용을 안 하니까 true면 예외 던짐
                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                        return false;
                }
            }

            bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            if (bucket != null)
            {
                for (int i = 0; i < bucket.Count;i++)
                {
                    if (bucket[i].Key.Equals(key))
                    {
                        value = bucket[i].Value;
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }

        public bool Remove(TKey key)
        {
            // List의 리무브 기능 이용해서 구현하기
            // 1. 해시 인덱스 구해서 버킷 찾음
            // 2. 버킷에서 내가 원하는 key와 동일한 KeyValuePair 있는지 확인
            // 3. 있으면 해당 KeyValuePair를 버킷에서 삭제
            // 4. 삭제했는데 만약 현재 버킷의 아이템 개수가 0개면 유효한 인덱스 리스트에서 해당 인덱스 제거
        }
        // 이뉴머레이터 구현

        public int Hash(TKey key)
        {
            string keyName = key.ToString();
            int result = 0;
            for (int i =  0; i < keyName.Length; i++)
            {
                result += keyName[i];
            }
            result %= DEFAULT_SIZE;
            return result;
        }
    }
}
