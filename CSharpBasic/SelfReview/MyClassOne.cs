namespace SelfReview
{
    internal class MyClass
    {
        public static float x;
        public float y;
        public float z = 2.0f;

        public void MyMethod(int input)
        {
            int a = 10;
            Console.WriteLine($"int a(10) + input : {a + input}");
        }
    }
    internal class MyClassOne
    {
        static void Main(string[] args)
        {
            //MyClass.x = 5.0f;

            //MyClass myClass = new MyClass();
            //myClass.MyMethod(8);

            CollectionReview review = new CollectionReview();
            review.ArrAdd();
            review.ArrRead();
            review.ListColor();
            review.ListColorRead();
            review.Dic();
            review.DicRead();
        }










    }
}