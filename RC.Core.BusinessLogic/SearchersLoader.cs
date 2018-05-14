namespace RC.Core.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using RC.Common.SDK;

    /// <summary>
    /// A class responsible for loading all implementations of ISearcher (plugins)
    /// </summary>
    public class SearchersLoader : ISearchersLoader
    {
        /// <summary>
        /// Create the static instance of the SearchersLoader
        /// </summary>
        private static Lazy<SearchersLoader> instance = new Lazy<SearchersLoader>(() => new SearchersLoader(), true);

        /// <summary>
        /// Initializes static members of the <see cref="SearchersLoader"/> class.
        /// </summary>
        private SearchersLoader()
        {
            SearchEngines = LoadSearchers(GetSearchersPath());
        }

        /// <summary>
        /// Gets the instance of SearchersLoader
        /// </summary>
        public static SearchersLoader Value => instance.Value;

        /// <summary>
        /// Gets all the discovered search engines
        /// </summary>
        public List<ISearcher> SearchEngines { get; private set; }

        /// <summary>
        /// Gets the list of region-based search engines available
        /// </summary>
        public List<Tuple<ISearcher, SearcherRegion>> GetSearchEngineRegions()
        {
            return SearchersLoader.Value.SearchEngines.SelectMany(s => s.Regions.Select(r => Tuple.Create(s, r))).ToList();
        }

        /// <summary>
        /// Loads all classes implementing the ISearcher interface
        /// </summary>
        /// <param name="path">Path to search for</param>
        /// <returns>List of all the discovered and loaded searchers</returns>
        public List<ISearcher> LoadSearchers(string path)
        {
            if (Directory.Exists(path))
            {
                var dllFileNames = Directory.GetFiles(path, "*.dll");
                var assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                var pluginType = typeof(ISearcher);
                var pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                var plugins = new List<ISearcher>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    var plugin = (ISearcher)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return null;
        }

        /// <summary>
        /// Gets the SearchEngines path
        /// </summary>
        private string GetSearchersPath()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(exePath, "SearchEngines");
        }
    }
}
