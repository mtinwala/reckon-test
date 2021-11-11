using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace DivisorConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var rangeInfo = await GetRangeInfo();
            var divisorInfo = await GetDivisorInfo();

            Console.WriteLine(rangeInfo.lower);
            Console.WriteLine(rangeInfo.upper);

            if (divisorInfo != null)
            {
                foreach (OutputDetail detail in divisorInfo.outputDetails) 
                {
                    Console.WriteLine($"divisor {detail.divisor}, output {detail.output}");
                }
            }
        }

        private static async Task<RangeInfo> GetRangeInfo()
        {
            var streamTask = client.GetStreamAsync("https://join.reckon.com/test1/rangeInfo");
            var rangeInfo = await JsonSerializer.DeserializeAsync<RangeInfo>(await streamTask);
            return rangeInfo;
        }

        private static async Task<DivisorInfo> GetDivisorInfo()
        {
            var streamTask = client.GetStreamAsync("https://join.reckon.com/test1/divisorInfo");
            var divisorInfo = await JsonSerializer.DeserializeAsync<DivisorInfo>(await streamTask);
            return divisorInfo;
        }
    }
}
