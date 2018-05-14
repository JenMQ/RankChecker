namespace RC.Searcher.Google
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Forms;
    using RC.Common.Infrastructure;
    using RC.Common.SDK;
    using RC.Common.Types;

    /// <summary>
    /// A class running search using Google Search Engine
    /// </summary>
    public class GoogleSearcher : ISearcher
    {
        /// <summary>
        /// String format of google search string
        /// </summary>
        private const string SearchUrlStringFormat = "{0}/search";

        /// <summary>
        /// Webpage Utility Helper
        /// </summary>
        public readonly IWebPageUtility webPageUtilityHelper;

        /// <summary>
        /// List of supported regions
        /// </summary>
        private List<SearcherRegion> supportedRegions;

        /// <summary>
        /// Initializes a new instance of GoogleSearcher class
        /// </summary>
        public GoogleSearcher()
        {
            webPageUtilityHelper = new WebPageUtility();
        }

        /// <summary>
        /// Initializes a new instance of GoogleSearcher class
        /// </summary>
        /// <param name="webpageUtility">Webpage utility to use</param>
        public GoogleSearcher(IWebPageUtility webpageUtility)
        {
            if (webpageUtility != null)
            {
                webPageUtilityHelper = webpageUtility;
            }
        }

        /// <summary>Gets the name of the search engine</summary>
        public string Name => "Google";

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
            var region = Regions.FirstOrDefault(r => r.Name == regionString);
            string googleSearchUrlString = string.Format(SearchUrlStringFormat, region.Url);

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("num", maxResultsCount.ToString()); // number of search results
            nameValueCollection.Add("q", searchQuery.Keywords); // query strings to search

            var webPageHtml = webPageUtilityHelper.GetWebPageHtml(googleSearchUrlString, nameValueCollection);
            var allLinks = GetResultLinksFromHtmlWebPage(webPageHtml);

            // Filter and return the search results containing the url string provided
            return allLinks.Where(link => link.UrlString.Contains(searchQuery.UrlString)).ToList();
        }

        /// <summary>
        /// Gets the search result links found from the provided HTML Web Page
        /// </summary>
        /// <param name="webPageHtml">HTML Webpage in string format</param>
        /// <returns>List of links found from the HTML Webpage</returns>
        private List<SearchResult> GetResultLinksFromHtmlWebPage(string webPageHtml)
        {
            var list = new List<SearchResult>();

            // Load the WebPage HTML string using web browser
            var htmlDocument = webPageUtilityHelper.GetHtmlDocument(webPageHtml);

            // Every Search Result is enclosed with an div class rc (based on current searches), find all H3s from the HTML
            var resultsDivContainer = htmlDocument.GetElementsByTagName("div");
            var resultPosition = 1;
            foreach (HtmlElement divContainer in resultsDivContainer)
            {
                if (divContainer.GetAttribute("className") != "g")
                {
                    continue;
                }

                var resultsContainer = divContainer.GetElementsByTagName("h3");
                foreach (HtmlElement container in resultsContainer)
                {
                    if (container.GetAttribute("className") != "r")
                    {
                        continue;
                    }

                    // Within the H3, find all Hyperlinks represented by <a> tag
                    var anchorTags = container.GetElementsByTagName("a");
                    foreach (HtmlElement tag in anchorTags)
                    {
                        var hrefValue = tag.GetAttribute("href");
                        var hrefValueInCaps = hrefValue.ToString().ToUpper();
                        var tempHref = hrefValue.ToUpper();

                        // Filter out links from Google and ensure the link contains '/url?q='
                        if (!hrefValueInCaps.Contains("GOOGLE") && hrefValue.ToString().Contains("/url?q=") && (hrefValueInCaps.Contains("HTTP://") || hrefValueInCaps.Contains("HTTPS://")))
                        {
                            var anchorTag = new SearchResult(tag.InnerText, resultPosition++, hrefValue);
                            list.Add(anchorTag);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the supported regions of this searcher
        /// </summary>
        /// <returns>List of supported regions</returns>
        private List<SearcherRegion> GetRegions()
        {
            /// Initialize regions if not yet set
            if (supportedRegions == null)
            {
                supportedRegions = new List<SearcherRegion>
                {
                    new SearcherRegion("Google Australia", "http://www.google.com.au"),
                    new SearcherRegion("Google Philippines", "http://www.google.com.ph"),
                    new SearcherRegion("Google Singapore", "http://www.google.com.sg"),
                    new SearcherRegion("Google Global", "http://www.google.com"),
                };
            }

            return supportedRegions;
        }
    }
}