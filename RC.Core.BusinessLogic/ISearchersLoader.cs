namespace RC.Core.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using RC.Common.SDK;

    /// <summary>
    /// A interfacee for loading all implementations of ISearcher (plugins)
    /// </summary>
    public interface ISearchersLoader
    {
        /// <summary>
        /// Gets the list of region-based search engines available
        /// </summary>
        List<Tuple<ISearcher, SearcherRegion>> GetSearchEngineRegions();

        /// <summary>
        /// Loads all classes implementing the ISearcher interface
        /// </summary>
        /// <param name="path">Path to search for</param>
        /// <returns>List of all the discovered and loaded searchers</returns>
        List<ISearcher> LoadSearchers(string path);
    }
}
