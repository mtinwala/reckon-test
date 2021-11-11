using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DivisorConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const int RETRY_COUNT = 5;

        static async Task Main(string[] args)
        {
            var rangeInfo = await GetRangeInfo();
            var divisorInfo = await GetDivisorInfo();
            
            if (rangeInfo != null && divisorInfo != null)
            {
                for (int i = rangeInfo.lower; i <= rangeInfo.upper; i++) 
                {
                    string result = string.Empty;
                    foreach (OutputDetail detail in divisorInfo.outputDetails)
                    {
                        if (i % detail.divisor == 0)
                            result += detail.output;
                    }
                    Console.WriteLine($"{i}:{result}");
                }
            }
        }

        private static async Task<RangeInfo> GetRangeInfo()
        {
            int attempts = RETRY_COUNT;
            RangeInfo rangeInfo = null;

            while (true)
            {
                try
                {
                    var streamTask = client.GetStreamAsync("https://join.reckon.com/test1/rangeInfo");
                    rangeInfo = await JsonSerializer.DeserializeAsync<RangeInfo>(await streamTask);
                    break;
                }
                catch
                {
                    if (--attempts == 0)
                        throw;
                    Thread.Sleep(1000);
                }
            }
            
            return rangeInfo;
        }

        private static async Task<DivisorInfo> GetDivisorInfo()
        {
            int attempts = RETRY_COUNT;
            DivisorInfo divisorInfo = null;

            while (true)
            {
                try
                {
                    var streamTask = client.GetStreamAsync("https://join.reckon.com/test1/divisorInfo");
                    divisorInfo = await JsonSerializer.DeserializeAsync<DivisorInfo>(await streamTask);
                    break;
                }
                catch
                {
                    if (--attempts == 0)
                        throw;
                    Thread.Sleep(1000);
                }
            }

            return divisorInfo;
        }

        
    }
}
