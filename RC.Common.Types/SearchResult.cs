namespace RC.Common.Types
{
    /// <summary>
    /// A class representing a Search Result item
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Initializes a new instance of the Search Result
        /// </summary>
        public SearchResult() : this(string.Empty, 0, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of a Search Result
        /// </summary>
        /// <param name="name">Name of the Search Result</param>
        /// <param name="rankPosition">Rank of the Search Result</param>
        /// <param name="urlString">URL associated to the Search Result</param>
        public SearchResult(string name, int rankPosition, string urlString)
        {
            Name = name;
            RankPosition = rankPosition;
            UrlString = urlString;
        }

        /// <summary>
        /// Gets or sets the URL String of the Search Result
        /// </summary>
        public string UrlString { get; private set; }

        /// <summary>
        /// Gets or sets the Name of the Search Result
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating the rank position of the search result
        /// </summary>
        public int RankPosition { get; private set; }
    }
}
