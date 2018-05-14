Simple SEO Tool: Rank Checker
By: Jennifer Q.

## Requirements

Design and code some software that receives a string of keywords, and a string URL. This is then processed to return a string of numbers for where the resulting URL is found in Google.

## Technologies Used

Used the following technologies: 
C# .NET
UI: WPF, Microsoft Interactivity

## Design and Implementation Details

Implemented Plugin Pattern for discovering searchers or search engines. This allows seamless integration of new searchers, for instance: Yahoo and Bing. Every searcher must implement the ISearcher interface to be discovered by the application.

Used WPF for the UI Implementation. The UI allows user to:
1. Select the search engine region
2. Select the maximum number of search results per page
3. Enter search keywords
4. Enter URL to search and rank
5. Check and Clear searches
6. Additional feature: Select from recent searches

## Unit Testing

Unit tests are created for the common infrastructure classes, view model and searcher.