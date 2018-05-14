namespace RC.Common.Infrastructure
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Windows.Forms;

    /// <summary>
    /// A utility class which provides helper methods to handle and manipulate WebPage
    /// </summary>
    public class WebPageUtility : IWebPageUtility
    {
        /// <summary>
        /// Gets a webpage html based on the provided url and query string values
        /// </summary>
        /// <param name="webpageUrl"></param>
        /// <param name="nameValueCollection"></param>
        /// <returns></returns>
        public string GetWebPageHtml(string webpageUrl, NameValueCollection nameValueCollection)
        {
            WebClient webClient = new WebClient();
            webClient.QueryString.Add(nameValueCollection);
            return webClient.DownloadString(webpageUrl);
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
