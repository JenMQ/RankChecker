namespace RC.Core.BusinessLogic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RC.Client.Tests.Mocks;
    using RC.Common.SDK;

    /// <summary>
    /// A class responsible for loading all implementations of ISearcher (plugins)
    /// </summary>
    public class MockedSearchersLoader : ISearchersLoader
    {
        private ISearcher singleSearcher = new MockedSearcher();

        /// <summary>
        /// Gets the list of region-based search engines available
        /// </summary>
        public List<Tuple<ISearcher, SearcherRegion>> GetSearchEngineRegions()
        {
            return new List<Tuple<ISearcher, SearcherRegion>>()
            {
                new Tuple<ISearcher, SearcherRegion>(singleSearcher, singleSearcher.Regions.FirstOrDefault())
            };
        }

        /// <summary>
        /// Loads all classes implementing the ISearcher interface
        /// </summary>
        /// <param name="path">Path to search for</param>
        /// <returns>List of all the discovered and loaded searchers</returns>
        public List<ISearcher> LoadSearchers(string path)
        {
            return new List<ISearcher>() { singleSearcher };
        }
    }
}
