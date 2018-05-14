namespace RC.Client.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RC.Core.BusinessLogic.Tests;
    using System.Threading.Tasks;

    [TestClass()]
    public class RankCheckerViewModelTests
    {
        [TestMethod()]
        [TestCategory("Client")]
        public void RankCheckerVMTest_ExecuteCheck()
        {
            var mockedSearchersLoader = new MockedSearchersLoader();
            var viewModel = new RankCheckerViewModel(mockedSearchersLoader);

            // Setup & Execute Check command
            viewModel.Keywords = "hello world";
            viewModel.UrlString = "www.somedomain.com";
            viewModel.CheckCommand.Execute(null);

            // Assert after execute
            Assert.AreEqual(false, viewModel.IsSearchInProgress);
            Assert.AreEqual("0", viewModel.ResultsText);
            Assert.AreEqual("Search completed. Found 0 result.", viewModel.ResultsStatus);
            Assert.IsTrue(viewModel.RecentSearchesCollection.Count == 1);
            Assert.IsTrue(viewModel.RecentSearchesCollection[0].Keywords == viewModel.Keywords);
            Assert.IsTrue(viewModel.RecentSearchesCollection[0].UrlString == viewModel.UrlString);
            Assert.IsNotNull(viewModel.SelectedSearchQuery);
            Assert.IsTrue(viewModel.SelectedSearchQuery.Keywords == viewModel.Keywords);
            Assert.IsTrue(viewModel.SelectedSearchQuery.UrlString == viewModel.UrlString);
        }

        [TestMethod()]
        [TestCategory("Client")]
        public void RankCheckerVMTest_ExecuteClear()
        {
            var mockedSearchersLoader = new MockedSearchersLoader();
            var viewModel = new RankCheckerViewModel(mockedSearchersLoader);

            // Setup & Execute Clear command
            viewModel.Keywords = "hello world";
            viewModel.UrlString = "www.somedomain.com";
            viewModel.ClearCommand.Execute(null);

            // Assert after execute
            Assert.AreEqual(string.Empty, viewModel.Keywords);
            Assert.AreEqual(string.Empty, viewModel.UrlString);
            Assert.AreEqual(string.Empty, viewModel.ResultsText);
            Assert.AreEqual(string.Empty, viewModel.ResultsStatus);
            Assert.AreEqual(false, viewModel.IsSearchInProgress);
            Assert.IsNull(viewModel.SelectedSearchQuery);
        }

        [TestMethod()]
        [TestCategory("Client")]
        public void RankCheckerVMTest_CanCheck()
        {
            var mockedSearchersLoader = new MockedSearchersLoader();
            var viewModel = new RankCheckerViewModel(mockedSearchersLoader);

            // With values: valid
            viewModel.Keywords = "hello world";
            viewModel.UrlString = "www.somedomain.com";
            Assert.AreEqual(true, viewModel.CanExecuteCheck);

            // Empty Keywords: invalid
            viewModel.Keywords = string.Empty;
            viewModel.UrlString = "www.somedomain.com";
            Assert.AreEqual(false, viewModel.CanExecuteCheck);

            // Empty Url: invalid
            viewModel.Keywords = "hello world";
            viewModel.UrlString = string.Empty;
            Assert.AreEqual(false, viewModel.CanExecuteCheck);

            // Both Empty: invalid
            viewModel.Keywords = string.Empty;
            viewModel.UrlString = string.Empty;
            Assert.AreEqual(false, viewModel.CanExecuteCheck);
        }

        [TestMethod()]
        [TestCategory("Client")]
        public void RankCheckerVMTest_CanClear()
        {
            var mockedSearchersLoader = new MockedSearchersLoader();
            var viewModel = new RankCheckerViewModel(mockedSearchersLoader);

            // With values: valid
            viewModel.Keywords = "hello world";
            viewModel.UrlString = "www.somedomain.com";
            Assert.AreEqual(true, viewModel.CanExecuteClear);

            // Empty Keywords: valid
            viewModel.Keywords = string.Empty;
            viewModel.UrlString = "www.somedomain.com";
            Assert.AreEqual(true, viewModel.CanExecuteClear);

            // Empty Url: valid
            viewModel.Keywords = "hello world";
            viewModel.UrlString = string.Empty;
            Assert.AreEqual(true, viewModel.CanExecuteClear);

            // Both Empty: invalid
            viewModel.Keywords = string.Empty;
            viewModel.UrlString = string.Empty;
            Assert.AreEqual(false, viewModel.CanExecuteClear);
        }

        [TestMethod()]
        [TestCategory("Client")]
        public void RankCheckerVMTest_TestAllProperties()
        {
            var mockedSearchersLoader = new MockedSearchersLoader();
            bool isTriggered;
            var viewModel = new RankCheckerViewModel(mockedSearchersLoader);
            viewModel.PropertyChanged += (sender, args) => { isTriggered = true; };

            isTriggered = false;
            viewModel.Keywords = "hello world";
            Assert.AreEqual("hello world", viewModel.Keywords);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.UrlString = "www.somedomain.com";
            Assert.AreEqual("www.somedomain.com", viewModel.UrlString);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.IsSearchInProgress = true;
            Assert.IsTrue(viewModel.IsSearchInProgress);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.ResultsText = "1";
            Assert.AreEqual("1", viewModel.ResultsText);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.ResultsStatus = "Done";
            Assert.AreEqual("Done", viewModel.ResultsStatus);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.SelectedNumberOfResults = 10;
            Assert.AreEqual(10, viewModel.SelectedNumberOfResults);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.CanExecuteCheck = true;
            Assert.IsTrue(viewModel.CanExecuteCheck);
            Assert.IsTrue(isTriggered);

            isTriggered = false;
            viewModel.CanExecuteClear = true;
            Assert.IsTrue(viewModel.CanExecuteClear);
            Assert.IsTrue(isTriggered);
        }
    }
}