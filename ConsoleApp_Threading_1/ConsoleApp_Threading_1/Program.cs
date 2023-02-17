namespace ConsoleApp_Threading_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);

            thread1.Name = "Thread 1";
            thread2.Name = "Thread 2";

            thread1.Start();
            thread2.Start();
        }

        static void Method1()
        {
            for (int i = 0; i < 2; i++)
            {

            }
        }

        static void Method2()
        {
            for(int i = 0; i < 2; i++)
            {

            }
        }
        

    }
}