namespace RC.Common.SDK
{
    public class SearcherRegion
    {
        /// <summary>
        /// Initializes a new instance of a Searcher Region
        /// </summary>
        public SearcherRegion() : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of a Searcher Region
        /// </summary>
        /// <param name="name">Name of this region based searcher</param>
        /// <param name="urlString">Url String of the searcher</param>
        public SearcherRegion(string name, string urlString)
        {
            Name = name.Trim();
            Url = urlString.Trim();
        }

        /// <summary>Gets the name of the search engine region</summary>
        public string Name { get; private set; }

        /// <summary>Gets the URL of the search engine based on the region</summary>
        public string Url { get; private set; }
    }
}
