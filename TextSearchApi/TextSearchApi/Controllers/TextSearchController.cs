using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TextSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextSearchController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private const int RETRY_COUNT = 5;

        private readonly ILogger<TextSearchController> _logger;

        public TextSearchController(ILogger<TextSearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<TextSearchResponse> GetAsync()
        {
            var content = await GetTextInfo();
            var subTextParam = await GetSubtextToSearch();
            TextSearchResponse response = new TextSearchResponse();
            response.candidate = "Murtaza Tinwala";

            if (content != null && 
                !string.IsNullOrEmpty(content.text) && 
                subTextParam != null && 
                subTextParam.subTexts != null ) 
            {
                response.text = content.text;
                List<SearchResult> searchResults = new List<SearchResult>();

                foreach (string subtext in subTextParam.subTexts)
                {
                    if (!string.IsNullOrEmpty(subtext)) 
                    {
                        SearchResult searchResult = new SearchResult();
                        searchResult.subtext = subtext;
                        int[] occurrences = StringHelper.FindOccurrences(content.text, subtext);
                        if (occurrences.Length > 0)
                        {
                            searchResult.result = string.Join(",", occurrences);
                        }
                        else
                        {
                            searchResult.result = "<No Output>";
                        }
                        searchResults.Add(searchResult);
                    }
                }

                response.results = searchResults.ToArray();
            }

            return response;
        }

        private static async Task<ContentToSearch> GetTextInfo()
        {
            int attempts = RETRY_COUNT;
            ContentToSearch contentToSearch = null;

            while (true)
            {
                try
                {
                    var streamTask = client.GetStreamAsync("https://join.reckon.com/test2/textToSearch");
                    contentToSearch = await JsonSerializer.DeserializeAsync<ContentToSearch>(await streamTask);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    if (--attempts == 0)
                        throw;
                    Thread.Sleep(1000);
                }
            }

            return contentToSearch;
        }

        private static async Task<SubTextParam> GetSubtextToSearch()
        {
            int attempts = RETRY_COUNT;
            SubTextParam subtextInfo = null;

            while (true)
            {
                try
                {
                    var streamTask = client.GetStreamAsync("https://join.reckon.com/test2/subTexts");
                    subtextInfo = await JsonSerializer.DeserializeAsync<SubTextParam>(await streamTask);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    if (--attempts == 0)
                        throw;
                    Thread.Sleep(1000);
                }
            }

            return subtextInfo;
        }

    }
}
