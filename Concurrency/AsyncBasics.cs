using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrency
{
    public static class AsyncBasics
    {
        public static void Start()
        {
            var task=DownloadStringWithRetries(@"http:\\www.wp.pl");


            Console.WriteLine("End main thread");
            Loading();
            Console.WriteLine(task.Result);
            Console.ReadKey();
        }

        private static async void Loading()
        {
            while (true)
            {
                await Task.Delay(200);

                Console.Write(".");
            }
        }

        private static async Task<string> DownloadStringWithRetries(string uri)
        {
            using (var client = new HttpClient())
            {
                // Retry after 1 second, then after 2 seconds, then 4.
                var nextDelay = TimeSpan.FromSeconds(1);
                for (int i = 0; i != 3; ++i)
                {
                    try
                    {
                        Thread.Sleep(3000);
                        return await client.GetStringAsync(uri);
                    }
                    catch
                    {
                    }
                    await Task.Delay(nextDelay);
                    nextDelay = nextDelay + nextDelay;
                } // Try one last time, allowing the error to propogate. 
                return await client.GetStringAsync(uri);
            }
        }
    }
}
