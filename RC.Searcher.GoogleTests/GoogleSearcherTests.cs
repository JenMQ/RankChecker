namespace RC.Searcher.Google.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RC.Common.Infrastructure.Tests;
    using RC.Common.Types;

    [TestClass()]
    public class GoogleSearcherTests
    {
        /// <summary>
        /// Tests the Search method of GoogleSearcher: Expects one result
        /// </summary>
        [TestMethod()]
        public void SearchTest_1Result()
        {
            // Mock the WebPageUtility
            var mockedWebPageUtility = new MockedWebPageUtility("..\\..\\SampleData\\LSE_100Results.txt");

            // Create the searcher and pass the mocked utility
            var searcher = new GoogleSearcher(mockedWebPageUtility);
            var results = searcher.Search("Google Australia", new SearchQuery("latest space exploration", "www.nasa.gov"), 100);

            // Check number of results and rank
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(6, results[0].RankPosition);
        }

        /// <summary>
        /// Tests the Search method of GoogleSearcher: Expects 2 results
        /// </summary>
        [TestMethod()]
        public void SearchTest_2Results()
        {
            // Mock the WebPageUtility
            var mockedWebPageUtility = new MockedWebPageUtility("..\\..\\SampleData\\LSE_100Results.txt");

            // Create the searcher and pass the mocked utility
            var searcher = new GoogleSearcher(mockedWebPageUtility);
            var results = searcher.Search("Google Australia", new SearchQuery("latest space exploration", "www.space.com"), 100);

            // Check number of results and rank
            Assert.AreEqual(2, results.Count);
            if (results.Count >= 2)
            {
                Assert.AreEqual(1, results[0].RankPosition);
                Assert.AreEqual(2, results[1].RankPosition);
            }
        }

        /// <summary>
        /// Tests the Search method of GoogleSearcher: Expects 0 results (URL does not exists)
        /// </summary>
        [TestMethod()]
        public void SearchTest_0Result()
        {
            // Mock the WebPageUtility
            var mockedWebPageUtility = new MockedWebPageUtility("..\\..\\SampleData\\LSE_100Results.txt");

            // Create the searcher and pass the mocked utility
            var searcher = new GoogleSearcher(mockedWebPageUtility);
            var results = searcher.Search("Google Australia", new SearchQuery("latest space exploration", "www.randomdomain.com"), 100);

            // Check number of results and rank
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// Tests the Search method of GoogleSearcher: Expects empty results when html cannot be parsed (empty)
        /// </summary>
        [TestMethod()]
        public void SearchTest_EmptyResult()
        {
            // Mock the WebPageUtility
            var mockedWebPageUtility = new MockedWebPageUtility("..\\..\\SampleData\\NonExistentFile.txt");

            // Create the searcher and pass the mocked utility
            var searcher = new GoogleSearcher(mockedWebPageUtility);
            var results = searcher.Search("Google Australia", new SearchQuery("latest space exploration", "www.space.com"), 100);

            // Check number of results and rank
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>
        /// Tests the Search method of GoogleSearcher: Expects empty results when an issues is encountered upon parsing
        /// </summary>
        [TestMethod()]
        public void SearchTest_Invalid()
        {
            // Mock the WebPageUtility
            var mockedWebPageUtility = new MockedWebPageUtility("..\\..\\SampleData\\InvalidFile.txt");

            // Create the searcher and pass the mocked utility
            var searcher = new GoogleSearcher(mockedWebPageUtility);
            var results = searcher.Search("Google Australia", new SearchQuery("latest space exploration", "www.space.com"), 100);

            // Check number of results and rank
            Assert.AreEqual(0, results.Count);
        }
    }
}