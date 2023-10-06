namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Character character = new Character();
            Warrior warrior1 = new Warrior();
            warrior1.NickName = "지존검사"; // setter 호출 (인자는 "지존검사")
            Console.WriteLine($"{warrior1.NickName} 의 경험치 : {warrior1.Exp}"); 
            // NickName 과 Exp 의 getter 호출

        }
    }
}