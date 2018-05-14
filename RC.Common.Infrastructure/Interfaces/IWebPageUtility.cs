namespace RC.Common.Infrastructure
{
    using System.Collections.Specialized;
    using System.Windows.Forms;

    /// <summary>
    /// A interface for webpage utility which provides helper methods to handle and manipulate WebPage
    /// </summary>
    public interface IWebPageUtility
    {
        /// <summary>
        /// Gets a webpage html based on the provided url and query string values
        /// </summary>
        /// <param name="webpageUrl"></param>
        /// <param name="nameValueCollection"></param>
        /// <returns></returns>
        string GetWebPageHtml(string webpageUrl, NameValueCollection nameValueCollection);

        /// <summary>
        /// Gets the HtmlDocument based on the provided webpage html
        /// </summary>
        /// <param name="webPageHtml">Webpage html</param>
        /// <returns>Html Document</returns>
        HtmlDocument GetHtmlDocument(string webPageHtml);
    }
}
