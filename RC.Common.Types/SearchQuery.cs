namespace RC.Common.Types
{
    /// <summary>
    /// A class representing a search query containing information about the keywords and the URL to Search
    /// </summary>
    public class SearchQuery
    {
        /// <summary>
        /// Initializes a new instance of a Search Query
        /// </summary>
        public SearchQuery() : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of a Search Query
        /// </summary>
        /// <param name="keywords">Keywords to search</param>
        /// <param name="urlString">Url String to find</param>
        public SearchQuery(string keywords, string urlString)
        {
            Keywords = keywords.Trim();
            UrlString = urlString.Trim();
        }

        /// <summary>
        /// Gets or sets the Keywords to search
        /// </summary>
        public string Keywords { get; private set; }

        /// <summary>
        /// Gets or sets the URL string to find
        /// </summary>
        public string UrlString { get; private set; }
    }
}