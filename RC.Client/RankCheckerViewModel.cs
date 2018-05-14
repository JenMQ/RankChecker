namespace RC.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using RC.Common.Infrastructure;
    using RC.Common.SDK;
    using RC.Common.Types;
    using RC.Core.BusinessLogic;

    /// <summary>
    /// View Model for the RankCheckerView window
    /// </summary>
    public class RankCheckerViewModel : ViewModelBase
    {
        private const string DefaultResultStatus = "";

        private ISearchersLoader searchersLoader;
        private bool canExecuteCheck = false;
        private bool canExecuteClear = false;
        private bool searchInProgress;
        private int selectedNumberOfResults = 0;
        private string keywords = string.Empty;
        private string urlString = string.Empty;
        private string resultsStatus = DefaultResultStatus;
        private string resultsText;
        private ObservableCollection<SearchQuery> searchQueryCollection;
        private SearchQuery selectedQuery;
        private Tuple<ISearcher, SearcherRegion> selectedSearchEngine;

        /// <summary>
        /// Initializes a new instance of RankCheckerViewModel class.
        /// </summary>
        public RankCheckerViewModel()
            : this(SearchersLoader.Value)
        {
        }

        /// <summary>
        /// Initializes a new instance of RankCheckerViewModel class.
        /// </summary>
        /// <param name="searchersLoader">Searchers loader to use to get all available searchers</param>
        public RankCheckerViewModel(ISearchersLoader searchersLoader)
        {
            ConfigureCommands();

            // Setup available search engines for selection
            this.searchersLoader = searchersLoader;
            SearchEngines = GetSearchEngines();
            SelectedSearchEngine = SearchEngines.FirstOrDefault();

            // Setup number of search results selection
            NumberOfSearchResults = new List<int> { 10, 50, 100 };
            SelectedNumberOfResults = NumberOfSearchResults.FirstOrDefault();

    }

        /// <summary>
        /// Gets or sets the command to execute on click of the Check button
        /// </summary>
        public ICommand CheckCommand { get; private set; }

        /// <summary>
        /// Gets or sets the command to execute on click of the Clear button
        /// </summary>
        public ICommand ClearCommand { get; private set; }

        /// <summary>
        /// Gets or sets the command to execute upon selection of search query
        /// </summary>
        public ICommand SearchQuerySelectionChangedCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Check command can be executed
        /// </summary>
        public bool CanExecuteCheck
        {
            get { return canExecuteCheck; }
            set
            {
                canExecuteCheck = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Clear command can be executed
        /// </summary>
        public bool CanExecuteClear
        {
            get { return canExecuteClear; }
            set
            {
                canExecuteClear = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Keywords to search
        /// </summary>
        public string Keywords
        {
            get { return keywords; }
            set
            {
                keywords = value;
                OnCheckParametersChanged();
            }
        }

        /// <summary>
        /// Gets or sets the URL string to find
        /// </summary>
        public string UrlString
        {
            get { return urlString; }
            set
            {
                urlString = value;
                OnCheckParametersChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Results Text
        /// </summary>
        public string ResultsText
        {
            get { return resultsText; }
            set
            {
                resultsText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Results Status
        /// </summary>
        public string ResultsStatus
        {
            get { return resultsStatus; }
            set
            {
                resultsStatus = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the list of search engines available
        /// </summary>
        public List<int> NumberOfSearchResults { get; private set; }

        /// <summary>
        /// Gets or sets the maximum number of search results
        /// </summary>
        public int SelectedNumberOfResults
        {
            get { return selectedNumberOfResults; }
            set
            {
                selectedNumberOfResults = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Recents Searches Collection
        /// </summary>
        public ObservableCollection<SearchQuery> RecentSearchesCollection
        {
            get
            {
                if (searchQueryCollection == null)
                {
                    searchQueryCollection = new ObservableCollection<SearchQuery>();
                }

                return searchQueryCollection;
            }

            set
            {
                searchQueryCollection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected search query
        /// </summary>
        public SearchQuery SelectedSearchQuery
        {
            get { return selectedQuery; }
            set
            {
                selectedQuery = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the list of search engines available
        /// </summary>
        public List<Tuple<ISearcher, SearcherRegion>> SearchEngines { get; private set; }

        /// <summary>
        /// Gets or sets the selected search engine
        /// </summary>
        public Tuple<ISearcher, SearcherRegion> SelectedSearchEngine
        {
            get { return selectedSearchEngine; }
            set
            {
                selectedSearchEngine = value;
                OnCheckParametersChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether search is in progress
        /// </summary>
        public bool IsSearchInProgress
        {
            get { return searchInProgress; }
            set
            {
                searchInProgress = value;
                OnCheckParametersChanged();
            }
        }

        /// <summary>
        /// Configure commands to be bound to the View
        /// </summary>
        private void ConfigureCommands()
        {
            CheckCommand = new RelayCommand(ExecuteCheckCommand, param => CanExecuteCheck);
            ClearCommand = new RelayCommand(ExecuteClearCommand, param => CanExecuteClear);
            SearchQuerySelectionChangedCommand = new RelayCommand(ExecuteSelectionChangedCommand);
        }

        /// <summary>
        /// Method to call when re-evaluating CanExecuteCheck property and then invoking PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that changed</param>
        private void OnCheckParametersChanged([CallerMemberName] string propertyName = null)
        {
            CanExecuteCheck = SelectedSearchEngine != null && !string.IsNullOrEmpty(Keywords) && !string.IsNullOrEmpty(UrlString);
            CanExecuteClear = SelectedSearchEngine != null && (!string.IsNullOrEmpty(Keywords) || !string.IsNullOrEmpty(UrlString));
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Action to execute upon trigger of the Check Command
        /// </summary>
        /// <param name="parameter">Passed command parameter</param>
        private void ExecuteCheckCommand(object parameter)
        {
            if (IsSearchInProgress)
            {
                return; // Do not allow multiple searches at a time
            }

            IsSearchInProgress = true;
            ResultsStatus = "Search is in progress.";
            ResultsText = string.Empty;

            // Add current search to recent searches
            AddToRecentSearches(Keywords, UrlString);

            var resultsCount = 0;
            try
            {
                var searcher = SelectedSearchEngine.Item1;
                var region = SelectedSearchEngine.Item2;
                var links = searcher.Search(region.Name, new SearchQuery(Keywords, urlString), SelectedNumberOfResults);
                ResultsText = links.Count > 0 ? links.Select(link => link.RankPosition.ToString()).Aggregate((a, b) => string.Format("{0}, {1}", a, b)) : "0";
                resultsCount = links.Count;
            }
            catch(Exception ex)
            {
                ResultsStatus = string.Format("Search failed: {0}", ex.Message);
            }
            finally
            {
                IsSearchInProgress = false;
            }

            ResultsStatus = string.Format("Search completed. Found {0} result{1}.", resultsCount, resultsCount > 1 ? "s" : string.Empty);
        }

        /// <summary>
        /// Action to execute upon trigger of the Clear Command
        /// </summary>
        /// <param name="parameter">Passed command parameter</param>
        private void ExecuteClearCommand(object parameter)
        {
            if (IsSearchInProgress)
            {
                return; // Do not allow clear when search is in progress
            }

            Keywords = string.Empty;
            UrlString = string.Empty;
            ResultsStatus = DefaultResultStatus;
            ResultsText = string.Empty;
            SelectedSearchQuery = null;
        }

        /// <summary>
        /// Action to execute upon trigger of the Search Query Selection Changed Command
        /// </summary>
        /// <param name="parameter">Passed command parameter</param>
        private void ExecuteSelectionChangedCommand(object parameter)
        {
            if (IsSearchInProgress || SelectedSearchQuery == null)
            {
                return; // Do not allow when search is in progress
            }


            // Set Keywords and URL then trigger Check
            Keywords = SelectedSearchQuery.Keywords;
            UrlString = SelectedSearchQuery.UrlString;
            ExecuteCheckCommand(null);
        }

        /// <summary>
        /// Adds the keywords & urlString combination to the list of recent searches
        /// </summary>
        /// <param name="keywords">Keywords</param>
        /// <param name="urlString">Url String to Search</param>
        private void AddToRecentSearches(string keywords, string urlString)
        {
            var existingQuery = RecentSearchesCollection.FirstOrDefault(s => s.Keywords == keywords && s.UrlString == urlString);
            if (existingQuery != null)
            {
                SelectedSearchQuery = existingQuery;
                return; // Do not add if the search query is already available
            }

            var newSearchQuery = new SearchQuery(keywords, urlString);
            RecentSearchesCollection.Insert(0, newSearchQuery);
            SelectedSearchQuery = newSearchQuery;
        }

        /// <summary>
        /// Gets the list of search engines available
        /// </summary>
        public List<Tuple<ISearcher, SearcherRegion>> GetSearchEngines()
        {
            return searchersLoader.GetSearchEngineRegions();
        }
    }
}
