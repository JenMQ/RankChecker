namespace RC.Common.Infrastructure.Tests
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// A utility class which provides helper methods to handle and manipulate WebPage
    /// </summary>
    public class MockedWebPageUtility : IWebPageUtility
    {
        /// <summary>
        /// Downloaded Webpage HTML string
        /// </summary>
        private string downloadedWebPageHtml = string.Empty;

        /// <summary>
        /// Initializes a new instance of MockedWebPageUtility class
        /// </summary>
        /// <param name="dataPath">Path to get the sample data from</param>
        public MockedWebPageUtility(string dataPath)
        {
            if (File.Exists(dataPath))
            {
                downloadedWebPageHtml = File.ReadAllText(dataPath);
            }
            else
            {
                downloadedWebPageHtml = string.Empty;
            }
        }

        /// <summary>
        /// Gets a webpage html based on the provided url and query string values
        /// </summary>
        /// <param name="webpageUrl"></param>
        /// <param name="nameValueCollection"></param>
        /// <returns></returns>
        public string GetWebPageHtml(string webpageUrl, NameValueCollection nameValueCollection)
        {
            return downloadedWebPageHtml;
        }

        /// <summary>
        /// Gets the HtmlDocument based on the provided webpage html
        /// </summary>
        /// <param name="webPageHtml">Webpage html</param>
        /// <returns>Html Document</returns>
        public HtmlDocument GetHtmlDocument(string webPageHtml)
        {
            // Load the WebPage HTML string using web browser
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.DocumentText = webPageHtml;
            browser.Document.OpenNew(true);
            browser.Document.Write(webPageHtml);
            browser.Refresh();

            return browser.Document;
        }
    }
}
