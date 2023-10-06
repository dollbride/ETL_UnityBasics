namespace Boxing
{
    class Dummy { }
    enum State { None, }
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 3;
            object obj1 = 1;    // boxing
            object obj2 = 2.4;
            object obj3 = "Luke";
            object obj4 = new Dummy();
            object obj5 = State.None;

            a = (int)obj1;  // unboxing
        }
    }
}