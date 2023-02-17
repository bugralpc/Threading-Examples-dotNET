namespace ConsoleApp_Threading_1
{
    // Example of multi-thread operation.
    // Put debug points on Method1, Method2, Method3
    // Run on Debug, open Debug->Windows->GPU Threads and Parallel Stacks, then Step Over through to the program

    // On parallel stacks, you can see instant stack information which is in main thread or worker thread,
    // it also shows the call stack.
    
    // On GPU Threads, you can see the active threads on your program.
    // Main Thread whose Id is 1 is top of the list, and you can see also Thread 1 and Thread 2

    // Momentarily, the indicator shows either the main thread, the thread 1 or the thread 2

    // When thread 1 or thread 2 completes their tasks (Method1 and Method2) they will be terminated.
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

            Method3();
        }

        static void Method1()
        {
            Thread thread = Thread.CurrentThread;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Thread 1 Id: " + thread.ManagedThreadId);
            }
        }

        static void Method2()
        {
            Thread thread = Thread.CurrentThread;

            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine("Thread 2 Id: " + thread.ManagedThreadId);
            }
        }

        static void Method3()
        {
            Thread thread = Thread.CurrentThread;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main Thread Id: " + thread.ManagedThreadId);
            }
        }
    }
}