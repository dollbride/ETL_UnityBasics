namespace DynamicType
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var a = 1;      // 컴파일 시 a 변수는 int 타입으로 결정
            dynamic b = 2;  // 런타임 중 대입 값에 따라 타입 결정
        }
    }
}
