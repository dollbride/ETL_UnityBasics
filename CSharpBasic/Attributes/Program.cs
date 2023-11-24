using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Attributes
{
    internal class Program
    {
        internal class A
        {
            [Obsolete("웬만하면 쓰지 마시오")]  // 더이상 사용되지 않는다는 의미의 attribute
            public void OldMethod() => Console.WriteLine("This is old");
            public void NewMethod() => Console.WriteLine("This is new");
        }

        internal class B
        {
            [Conditional("DEBUG")]  // #define 전처리기에서 정의된 문자열일 때만 구현하는 특성
            public void Log([CallerMemberName] string caller = default) 
                => Console.WriteLine($"I'm B, called by {caller}");
        }

        static void Main(string[] args)
        {
            A a = new A();
            a.OldMethod();
            a.NewMethod();

            B b = new B();
            b.Log();

            LegoStore legoStore = new LegoStore();
            legoStore.PropertyChanged += (sender, args) =>
            {
                Console.WriteLine($"{args.PropertyName} of {sender} has changed..!");

                switch (args.PropertyName)
                {
                    case "CreatorTotal":
                        Console.WriteLine($"Luke는 LegoStore로 달려가기 시작했다...!");
                        break;
                    case "StarwarsTotal":
                        {
                            if(legoStore.StarwarsTotal < 3)
                                Console.WriteLine($"Luke : 아.. 하나라도 남아있다면 좋겠다");
                            else
                                Console.WriteLine($"Luke : 스타워즈는 다음에 사지 뭐");
                        }
                        break;
                    case "CityTotal":
                        Console.WriteLine("아 City만 구독 취소하고 싶네");
                        break;
                    default:
                        break;
                }
            };
            legoStore.StarwarsTotal = 3;
            legoStore.CreatorTotal = 3;
            legoStore.CityTotal = 3;
        }
    }
}
