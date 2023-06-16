using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal class SemaphoreSlimExamples
    {
        private static SemaphoreSlim semaphore = semaphore = new SemaphoreSlim(0, 3);

        public static async ValueTask Execute()
        {
            //0 tasks can enter semaphore.
            Console.WriteLine("{0} tasks can enter the semaphore.", semaphore.CurrentCount);
            Task[] tasks = new Task[5];

            // Create and start five numbered tasks.
            for (int i = 0; i <= 4; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        await Task.Delay(1000);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
            }

            // Wait for half a second, to allow all the tasks to start and block.
            await Task.Delay(500);

            // Restore the semaphore count to its maximum value.
            Console.Write("Main thread calls Release(3) --> ");

            semaphore.Release(3);

            //3 tasks can enter semaphore.
            Console.WriteLine("{0} tasks can enter the semaphore.", semaphore.CurrentCount);

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
