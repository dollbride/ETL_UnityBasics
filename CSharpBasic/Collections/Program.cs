namespace Collections
{
    enum ItemID
    {
        RedPotion = 20, BluePotion = 21
    }
    class SlotData
    {
        public bool isEmpty => id == 0 && num == 0;
        public int id;
        public int num;

        public SlotData(int id, int num)
        {
            this.id = id;
            this.num = num;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDynamicArray inventory = new MyDynamicArray();
            
            for (int i = 0; i < 10; i++)
            {
                inventory.Add(new SlotData(0, 0));
                
            }

            inventory[0] = new SlotData((int)ItemID.RedPotion, 40);
            inventory[1] = new SlotData((int)ItemID.BluePotion, 99);
            inventory[2] = new SlotData((int)ItemID.BluePotion, 50);

            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"슬롯 {i} : [{(ItemID)((SlotData)inventory[i]).id}], [{((SlotData)inventory[i]).num}]");
            }
            Console.WriteLine("\n파란포션 5개 습득 후 인벤토리 정보:\n");
            //파란 포션 5개 획득
            //1. 파란 포션 5개 들어갈 수 있는 슬롯 찾기
            int availableSlotIndex = inventory.FindIndex(slotData => ((SlotData)slotData).isEmpty ||
                  (((SlotData)slotData).id == (int)ItemID.BluePotion && ((SlotData)slotData).num <= 99 - 5));

            //2. 해당 슬롯의 아이템 개수에다 내가 추가하려는 개수를 더한 만큼의 수정 예상 값(int expected)을 구함
            int expected = ((SlotData)inventory[availableSlotIndex]).num + 5;

            //3. 새로운 아이템 데이터를 만들어서 슬롯 데이터를 교체
            SlotData newSlotData = new SlotData((int)ItemID.BluePotion, expected);
            inventory[availableSlotIndex] = newSlotData;

            for (int i =0; i < inventory.Count; i++)
            {
                Console.WriteLine($"슬롯 {i} : [{(ItemID)((SlotData)inventory[i]).id}], [{((SlotData)inventory[i]).num}]");
            }
        }
    }
}