﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;

namespace Collections
{
    #region 포션에 대한 정의
    enum ItemID
    {
        RedPotion = 20, BluePotion = 21
    }
    class SlotData : IComparable<SlotData>
    {
        public bool isEmpty => id == 0 && num == 0;
        public int id;
        public int num;

        public SlotData(int id, int num)
        {
            this.id = id;
            this.num = num;
        }

        public int CompareTo(SlotData? other)
        {
            return this.id == other?.id && this.num == other?.num ? 0 : -1;
        }
    }
    #endregion
    internal class Program
    {
        static void Main(string[] args)
        {
            #region My Dynamic Array -> Inventory

            MyDynamicArray<SlotData> inventory = new MyDynamicArray<SlotData>();

            for (int i = 0; i < 10; i++)    //10개의 SlotData 클래스 생성
            {
                inventory.Add(new SlotData(0, 0));
            }

            inventory[0] = new SlotData((int)ItemID.RedPotion, 40);
            inventory[1] = new SlotData((int)ItemID.BluePotion, 99);
            inventory[2] = new SlotData((int)ItemID.BluePotion, 50);

            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"슬롯 {i} : {(ItemID)inventory[i].id}, {inventory[i].num}개");
            }
            //inventory[2] = new SlotData(0,0); 
            //삭제하고 싶을 땐 RemoveAt으로 슬롯을 날리느니 그냥 0,0을 덮어씌움

            Console.WriteLine("\n파란포션 5개 습득 후 인벤토리 정보:\n");
            //파란 포션 5개 획득
            //1. 파란 포션 5개 들어갈 수 있는 슬롯 찾기
            int availableSlotIndex = inventory.FindIndex(slotData => slotData.isEmpty ||
                                                        (slotData.id == (int)ItemID.BluePotion && slotData.num <= 99 - 5));

            //2. 해당 슬롯의 아이템 개수에다 내가 추가하려는 개수를 더한 만큼의 수정 예상 값(int expected)을 구함
            int expected = inventory[availableSlotIndex].num + 5;

            //3. 새로운 아이템 데이터를 만들어서 슬롯 데이터를 교체
            SlotData newSlotData = new SlotData((int)ItemID.BluePotion, expected);
            inventory[availableSlotIndex] = newSlotData;

            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"슬롯 {i} : {(ItemID)inventory[i].id}, {inventory[i].num}개");
            }
            #endregion

            #region IEnumerator
            //Console.WriteLine($"\nfor-each문");
            //IEnumerator<SlotData> enumerator = inventory.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine($"{(ItemID)enumerator.Current.id}, {enumerator.Current.num}개");
            //}
            //enumerator.Reset();
            //enumerator.Dispose();

            //foreach (var item in inventory)
            //{
            //    Console.WriteLine((ItemID)item.id);
            //    inventory[0] = new SlotData(0, 0);
            //}

            ////둘 이상의 콜렉션 순회
            //MyDynamicArray<SlotData> inventory2 = new MyDynamicArray<SlotData>();
            //for (int i = 0; i < 10; i++)    //10개의 SlotData 클래스 생성
            //{
            //    inventory2.Add(new SlotData(0, 0));
            //}

            //using (IEnumerator<SlotData> enumerator1 = inventory.GetEnumerator())
            //using (IEnumerator<SlotData> enumerator2 = inventory2.GetEnumerator())
            //{
            //    while (enumerator1.MoveNext() && enumerator2.MoveNext())
            //    {
            //        Console.WriteLine($"{(ItemID)enumerator1.Current.id}, {enumerator1.Current.num}개");
            //        Console.WriteLine($"{(ItemID)enumerator2.Current.id}, {enumerator2.Current.num}개");
            //    }
            //    enumerator1.Reset();
            //    enumerator2.Reset();
            //}
            #endregion

            #region ArrayList
            // C#에서 기본적으로 제공하는 동적배열 클래스
            ArrayList arrayList = new ArrayList();
            arrayList.Add("철수");  // 기본 오브젝트 타입으로 들어감
            arrayList.Add(3);
            arrayList.Remove("철수");
            // ㄴ> 오브젝트 타입의 박싱 언박싱에 의해서
            // 기존 철수와 주소가 다르기 때문에 원본 철수는 남아 있..어야 하는데 지워짐
            // 우리가 정의해서 했으면 개념상 남아있었어야 하는데 C#에서는 리무브 함수가 값을 비교해서 지움
            Console.WriteLine(arrayList[0]);

            // C#에서 기본적으로 제공하는 generic 타입의 동적배열 클래스
            List<String> list = new List<String>();
            list.Add("철수");
            list.Remove("철수");  // 이건 지워짐
            list.Find(x => x == "철수");
            list.Add("영희");
            Console.WriteLine(list[0]);

            #endregion

            #region Queue
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(3);  // 삽입
            if (queue.Peek() > 0)  // 탐색. 대기열에 한 개 이상 있으면
                queue.Dequeue();  // 맨 앞에 있는 거 뽑아 쓰기
            #endregion

            #region Stack
            Stack<int> stack = new Stack<int>();
            stack.Push(3);
            if (stack.Peek() > 0)
                stack.Pop();    // 제일 뒤에 있는 거 뽑아 쓰기
            #endregion

            #region Linked List

            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            linkedList.AddFirst(3);
            linkedList.Find(x => x > 0);

            //bool IsBiggerThan0(int x)
            //{
            //    return x > 0;
            //}

            #endregion

            Console.WriteLine($"\n-----My Hashtable below-----\n");

            #region Hashtable

            MyHashtable<string, float> myHashtable = new MyHashtable<string, float>();
            myHashtable.Add("Luke", 90.0f);
            myHashtable.Add("Calm", 80.0f);

            foreach (var item in myHashtable)
            {
                Console.WriteLine($"{item.Key}의 점수 : {item.Value}");
            }

            foreach (var item in myHashtable.Keys)
            {
                Console.WriteLine($"이름 : {item}");
            }

            foreach (var item in myHashtable.Values)
            {
                Console.WriteLine($"점수들 : {item}");
            }

            // C# 제공 해시테이블
            Hashtable hashtable = new Hashtable();
            hashtable.Add("철수", 90.0f);


            // C# 제공 제네릭 타입 해시테이블
            Dictionary<string, float> dictionary = new Dictionary<string, float>();
            dictionary.Add("영희", 30.0f);

            foreach (var item in dictionary)
            {
                Console.WriteLine($"{item.Key}의 점수 : {item.Value}");
            }

            //C# 제공 Hashset
            HashSet<string> keys = new HashSet<string>();
            keys.Add("a");
            keys.Add("b");


            #endregion

            IEnumerator routine = GetMakingToastRoutine2();

            while (routine.MoveNext())
            {
                Console.WriteLine(routine.Current);
            }

            foreach (var item in NumberEnumerationRoutine())
            {
                Console.WriteLine(item);
            }


        }



        static IEnumerator GetMakingToastRoutine()
        {
            return new MakingToastRoutine();
        }

        static IEnumerator GetMakingToastRoutine2()
        {
            yield return "Induction On";
            yield return "Pan ready";
            yield return "Toast ready";
            // struct 따로 정의하고 생성자 만들 필요 없이 yield로 간단히 정의.
            // 컴파일 시 객체를 알아서 만들고 MoveNext()도 알아서 채워준다.
        }

        static IEnumerable NumberEnumerationRoutine()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        struct MakingToastRoutine : IEnumerator
        {
            public object Current => _routine[_step];
            private string[] _routine =
            {
                "Induction On",
                "Pan ready",
                "Put Butter",
                "Put Bread",
                "Wait until bread toasted",
                "Put jam",
                "Induction Off",
                "Toast ready"
            };
            private int _step = -1;

            public MakingToastRoutine()
            {

            }

            public bool MoveNext()
            {
                if (_step >= _routine.Length)
                    return false;
                _step++;
                return _step < _routine.Length;
            }

            public void Reset()
            {
                _step = -1;
            }
        }

    }
}