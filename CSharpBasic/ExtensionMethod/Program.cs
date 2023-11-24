using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExtensionMethod
{
    // 확장 메서드
    // 어떤 객체 참조를 대상으로 기능을 확장할 때 사용하는 함수
    // static 클래스 내에서 static 매서드를 만들고 기능을 확장해야 하는 객체참조를 파라미터로 받는다.

    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "Luke Cho";
            name.WordCound();

            List<int> numbers = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14,
            };

            IEnumerable<string> filtered =
            from number in numbers
            where number > 5
            orderby number descending
            select $"number : {number}";

            foreach (string number in filtered )
            {
                Console.WriteLine(number);
            }

            IEnumerable<string> filtered2 =
            numbers.Where(x => x > 5)
                .OrderByDescending(x => x)
                .Select(x => $"number : {x}");

        }
    }
}
