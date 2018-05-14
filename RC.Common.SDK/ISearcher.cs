namespace RC.Common.SDK
{
    using System.Collections.Generic;
    using RC.Common.Types;

    /// <summary>
    /// An interface that must be implemented by Search Engines that needs to do a search (Google, Yahoo, Bing)
    /// </summary>
    public interface ISearcher
    {
        /// <summary>Gets the name of the search engine</summary>
        string Name { get; }

        /// <summary>Gets the list of supported regions</summary>
        List<SearcherRegion> Regions { get; }

        /// <summary>
        /// Find search results using the keywords provided which contains the provided URL
        /// </summary>
        /// <param name="regionString">Region to use for the search</param>
        /// <param name="searchQuery">Query containing details: Keywords to search, URL to find from the list of results</param>
        /// <param name="maxResultsCount">Maximum number of search results to find.</param>
        /// <returns>List of search results where the URL is found</returns>
        List<SearchResult> Search(string regionString, SearchQuery searchQuery, int maxResultsCount = 10);
    }
}
