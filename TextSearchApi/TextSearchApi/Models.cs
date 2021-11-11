using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextSearchApi
{
    public class TextSearchResponse
    {
        public string candidate { get; set; }
        public string text { get; set; }
        public SearchResult[] results { get; set; }
    }

    public class SearchResult
    { 
        public string subtext { get; set; }
        /// <summary>
        /// Comma separated index positions
        /// </summary>
        public string result { get; set; }
    }

    public class SubTextParam
    {
        public string[] subTexts { get; set; }
    }

    public class ContentToSearch
    {
        public string text { get; set; }
    }
}
