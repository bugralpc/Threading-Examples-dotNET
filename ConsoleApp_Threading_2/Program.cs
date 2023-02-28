namespace ConsoleApp_Threading_2
{
    internal class Program
    {
        // Sometimes, it is necessary to wait for one thread to complete before starting another thread or
        // wait for all threads to complete before proceeding further. To achieve this use .Join() method.

        // .Join() method of the Thread class waits for the thread to complete its execution before continuing with the
        // execution of the calling thread. On this case Calling Thread is Main Thread.
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);

            string resultThread = null;

            Thread thread3 = new Thread(() => { resultThread = Method3(); });

            thread1.Start();
            thread1.Join(); // it waits for thread1 to complete the job before starting thread2
            thread2.Start();
            thread2.Join(); // it waits for thread2 to complete the job before starting thread3

            thread3.Start();

            Console.WriteLine("Before Thread 3 Completes");

            thread3.Join(); // You need to use Join()

            Console.WriteLine("--" + resultThread + "--");

            Console.WriteLine("After Thread 3 Completes");

            // Task.WaitAll() method of the Task class waits for all the specified tasks to complete before continuing with
            // the execution of the calling thread (Main Thrad on this scenario). This methods blocks the calling thread
            // until all the specified tasks have completed their execution.

            Task task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Task 1 {0}", i);
                    Thread.Sleep(1000);
                }
            });

            Task task2 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Task 2 {0}", i);
                    Thread.Sleep(500);
                }
            });

            Task.WaitAll(task1, task2);

            Console.WriteLine("After completing task 1 and task 2");


        }

        private static void Method1()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Method1 {0}", i);
                Thread.Sleep(1000);
            }
        }
        private static void Method2()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("Method2 {0}", i);
                Thread.Sleep(1000);
            }
        }

        private static string Method3()
        {
            int result = 0;
            for(int i = 0; i < 5; i++)
            {
                result += i;
                Thread.Sleep(500);
            }

            return result.ToString();
        }
    }

}