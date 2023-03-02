namespace ConsoleApp_AutoResetEvent_1
{

    // AutoResetEvent is used for send signals between two threads.
    // Both threads can share the same AutoResetEvent object.

    // Thread can enter into a wait state by calling WaitOne() method of AutoResetEvent object.
    // When second thread calls the Set() method, it unblocks the waiting thread.

    // ARE maintains a boolean variable in memory. If the boolean variable is false it blocks thread, if the boolean is true
    // it unblocks the thread.


    // WaitOne() method, this method blocks the current thread and wait for the signal by other thread.
    // WaitOne method returns true if it receives the signal else returns false.

    // Set() method sent the signal to the waiting thread to proceed its work.
    internal class Program
    {
        private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        private static string output = "string output: Before the task is completed";
        static void Main(string[] args)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                SomeTaskWork();
            });

            Console.WriteLine("Main Thread => " + output);

            // Put the current thread into waiting state until it receives the signal from other thread.
            // autoResetEvent object puts the thread it is in to wait state.
            autoResetEvent.WaitOne();

            Console.WriteLine("Main Thread => Main Thread got signal from task autoResetEvent.Set() so it can continue");
            Console.WriteLine("Main Thread => " + output);

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Main Thread => Main Thread is running on Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Main Thread => Main Thread is a Thread-Pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("Main Thread => {0}", i);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Main Thread => " + "string output: Before the Child Thread is completed");

            Thread thread = new Thread(() =>
            {
                SomeThreadWork();
            });
            
            thread.Start();

            autoResetEvent.WaitOne();
            Console.WriteLine("Main Thread => Main Thread got signal from thread autoResetEvent.Set() so it can continue");
            Console.WriteLine("Main Thread => " + output);
        }

        private static void SomeTaskWork()
        {
            for(int i = 0; i < 2; i++)
            {
                Console.WriteLine("Task => Task is running on Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Task => Task thread is a Thread-Pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("Task => {0}", i);
                Thread.Sleep(1000);
            }

            output = "string output: " + "The task is completed on Thread-Pool thread";
            autoResetEvent.Set();
        }

        private static void SomeThreadWork()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Thread => Thread is running on Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Thread => Thread is a Thread-Pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("Thread => Thread does some work");
                Thread.Sleep(1000);
            }

            output = "string output: " + "The thread is completed on not Thread-Pool thread";
            autoResetEvent.Set();
        }
    }
}