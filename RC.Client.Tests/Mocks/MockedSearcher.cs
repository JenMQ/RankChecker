namespace RC.Client.Tests.Mocks
{
    using System.Collections.Generic;
    using RC.Common.Infrastructure;
    using RC.Common.SDK;
    using RC.Common.Types;

    /// <summary>
    /// A class running search using Google Search Engine
    /// </summary>
    public class MockedSearcher : ISearcher
    {
        /// <summary>
        /// Initializes a new instance of MockedSearcher class
        /// </summary>
        public MockedSearcher()
        {
        }

        /// <summary>
        /// Initializes a new instance of MockedSearcher class
        /// </summary>
        /// <param name="webpageUtility">Webpage utility to use</param>
        public MockedSearcher(IWebPageUtility webpageUtility)
        {
        }

        /// <summary>Gets the name of the search engine</summary>
        public string Name => "Mocked Google";

        /// <summary>Gets the list of supported regions</summary>
        public List<SearcherRegion> Regions => GetRegions();

        /// <summary>
        /// Find search results using the keywords provided which contains the provided URL
        /// </summary>
        /// <param name="regionString">Region to use for the search</param>
        /// <param name="searchQuery">Query containing details: Keywords to search, URL to find from the list of results</param>
        /// <param name="maxResultsCount">Maximum number of search results to find.</param>
        /// <returns>List of search results where the URL is found</returns>
        public List<SearchResult> Search(string regionString, SearchQuery searchQuery, int maxResultsCount = 10)
        {
            return new List<SearchResult>();
        }

        /// <summary>
        /// Gets the supported regions of this searcher
        /// </summary>
        /// <returns>List of supported regions</returns>
        private List<SearcherRegion> GetRegions()
        {
            return new List<SearcherRegion>
            {
                new SearcherRegion("Google Australia", "http://www.google.com.au"),
            };
        }
    }
}
