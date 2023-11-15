using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
namespace DukNuk.Wpf.Helpers {
    public class ThemeManager : INotifyPropertyChanged, INotifyPropertyChanging {

        public event EventHandler? ThemeResourcesChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        
        protected virtual void OnThemeResourceChanged() {
            this.ThemeResourcesChanged?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null) {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private ResourceDictionary _themeStyles;
        private ResourceDictionary _themeSizes;
        private ResourceDictionary _themeColors;
        private Dictionary<string, ResourceDictionary> _themeColorsDefinitions;

        public string CurrentTheme {
            get => this._currentTheme;
        }
        private string _currentTheme = "";

        public static readonly string[] DefaultColorResources = new string[] {
            @"pack://application:,,,/DukNukWpf;component/Themes/ThemeColors/Scruntastic.xaml",
            @"pack://application:,,,/DukNukWpf;component/Themes/ThemeColors/DefaultBlue.xaml",
            @"pack://application:,,,/DukNukWpf;component/Themes/ThemeColors/GreenGlowie.xaml",
        };

        public static readonly string DefaultStyles = @"pack://application:,,,/DukNukWpf;component/Themes/Generic.xaml";
        public static readonly string DefaultSizes = @"pack://application:,,,/DukNukWpf;component/Themes/ThemeSizes.xaml";

        static ThemeManager() {
            // i think we need this to initialize static memebers?...
        }

        public static ThemeManager Default => ThemeManager.__default ??= new ThemeManager();
        private static ThemeManager? __default = null;
        
        public ThemeManager(
            string? styleResourcesOverride = null, 
                string? sizeResourcesOverride = null, 
                string[]? colorResourcesOverride = null) {

            var wtf = ThemeManager.DefaultStyles;
            
            this._themeStyles = new ResourceDictionary() {
                Source = new Uri(styleResourcesOverride ?? ThemeManager.DefaultStyles),
            };

            this._themeSizes = new ResourceDictionary() {
                Source = new Uri(sizeResourcesOverride ?? ThemeManager.DefaultSizes)
            };

            this._themeColorsDefinitions = new Dictionary<string, ResourceDictionary>();

            foreach (string uri in colorResourcesOverride ?? ThemeManager.DefaultColorResources) {
                this.LoadThemeColorsFromUri(uri);
            }

            string name = this._themeColorsDefinitions.First().Key;

            this._themeColors = new ResourceDictionary() {
                MergedDictionaries = { this._themeColorsDefinitions[name], },
            };
        }

        /// <summary>
        /// tries to load theme color resources from a given URI
        /// </summary>
        /// <param name="uri">ResourceDictionary Source URI</param>
        public void LoadThemeColorsFromUri(string uri) {
            ResourceDictionary rd = new ResourceDictionary() {
                Source = new Uri(uri),
            };
            this.LoadThemeColors(rd);
        }
        
        /// <summary>
        /// tries to load theme color resources from a given URI
        /// </summary>
        /// <param name="uri">ResourceDictionary Source URI</param>
        public void LoadThemeColorsFromUri(Uri uri) {
            ResourceDictionary rd = new ResourceDictionary() {
                Source = uri
            };
            this.LoadThemeColors(rd);
        }

        /// <summary>
        /// tries to load theme color resources from a given file
        /// </summary>
        /// <param name="fileName">Full path to file</param>
        public void LoadThemeColorsFromFile(string fileName) {
            if (File.Exists(fileName)) {
                ResourceDictionary rd = new ResourceDictionary() {
                    Source = new Uri(fileName, UriKind.Absolute),
                };
                this.LoadThemeColors(rd);
            }
        }
        
        /// <summary>
        /// tries to load theme color resources from *.xaml files in a given directory
        /// </summary>
        /// <param name="directoryName">Full path to directory</param>
        /// <param name="searchSubDirs">if true, searches subdirectories for *.xaml files</param>
        public void LoadThemeColorsFromDirectory(string directoryName, bool searchSubDirs = true) {
            if (Directory.Exists(directoryName)) {
                DirectoryInfo di = new DirectoryInfo(directoryName);
                var fi = di.GetFileSystemInfos("*.xaml", searchSubDirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                foreach (var f in fi) {
                    this.LoadThemeColorsFromFile(f.FullName);
                }
            }
        }

        /// <summary>
        /// Adds theme color resources from a resource dictionary to the theme manager's own dictionaries.
        /// It will only add resources if the given resource dictionary contains a valid "DukNuk.Theme.Name" string key.
        /// Resources from the dictionary will override existing values in the theme manager's own dictionaries, effectively
        /// this allows you to override only certain resources if you so desire.
        /// </summary>
        /// <param name="rd">Resource dictionary</param>
        public void LoadThemeColors(ResourceDictionary rd) {
            if (rd.Contains("DukNuk.Theme.Name") && rd["DukNuk.Theme.Name"].ToString() is string name) {
                // if theme name is not already defined, create a new dictionary for it
                if (this._themeColorsDefinitions.ContainsKey(name) == false) {
                    this._themeColorsDefinitions.Add(name, new ResourceDictionary());
                }
                
                foreach (var xkey in rd.Keys) {
                    if (this._themeColorsDefinitions[name].Contains(xkey)) {
                        // if key already defined in dictionary, replace it
                        this._themeColorsDefinitions[name][xkey] = rd[xkey];
                    } else {
                        // if key is undefined, add it
                        this._themeColorsDefinitions[name].Add(xkey, rd[xkey]);
                    }
                }
                
                if (this.CurrentTheme == name) {
                    this.OnThemeResourceChanged();
                }
            }
        }

        /// <summary>
        /// Loads the necessary theme resources in the App's Resources
        /// </summary>
        /// <param name="initialThemeName">Name of the initial theme to load as defined by the DukNuk.Theme.Name resource.</param>
        public void InitializeAppResources(string? initialThemeName = null) {
            // remove existing theme data?...
            this.FreeFromAppResources();
            
            if (initialThemeName == null ||
                this._themeColorsDefinitions.ContainsKey(initialThemeName) == false) {
                initialThemeName = this._themeColorsDefinitions.First().Key;
            } 
            
            this._themeColors.Clear();
            this._themeColors.MergedDictionaries.Add(this._themeColorsDefinitions[initialThemeName]);
            this._currentTheme = initialThemeName;
            
            Application.Current.Resources.MergedDictionaries.Insert(0, this._themeColors);
            Application.Current.Resources.MergedDictionaries.Insert(0, this._themeSizes);
            Application.Current.Resources.MergedDictionaries.Insert(0, this._themeStyles);
        }

        /// <summary>
        /// Removes a resource dictionary from the App's MergedDictionaries
        /// </summary>
        /// <param name="source">Resource Dictionary source to remove</param>
        public void RemoveDictionaryFromAppResources(string source) {
            ResourceDictionary? found = null;
            foreach (ResourceDictionary rd in Application.Current.Resources.MergedDictionaries) {
                if (rd.Source.ToString() == source) {
                    found = rd;
                    break;
                }
            }
            if (found != null) {
                Application.Current.Resources.MergedDictionaries.Remove(found);
            }
        }
        
        /// <summary>
        /// Removes theme manager resources from App's MergedDictionaries
        /// </summary>
        public void FreeFromAppResources() {

            try {
                Application.Current.Resources.MergedDictionaries.Remove(this._themeStyles);
            } catch (Exception) {
                // ignored
            }
            try {
                Application.Current.Resources.MergedDictionaries.Remove(this._themeSizes);
            } catch (Exception) {
                // ignored
            }
            try {
                Application.Current.Resources.MergedDictionaries.Remove(this._themeColors);
            } catch (Exception) {
                // ignored
            }
        }
        
        public bool SetTheme(string themeName, bool forceReload = false) {
            if (this._themeColorsDefinitions.ContainsKey(themeName)) {
                if (this._currentTheme != themeName || forceReload) {
                    this.OnPropertyChanging(nameof(this.CurrentTheme));
                    
                    // NOTE, for some reason removing the old dictionary and adding the new one doesn't seem to work right, so i do the clear/add thing instead....
                    this._themeColors.Clear();
                    this._themeColors.MergedDictionaries.Add(this._themeColorsDefinitions[themeName]);
                    this._currentTheme = themeName;
                    
                    this.OnPropertyChanged(nameof(this.CurrentTheme));
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Helper function for getting a <see cref="Color"/> from a theme resource.../>
        /// </summary>
        /// <param name="resourceKey">Resource key</param>
        /// <param name="gradientIndex">Index if resource is a GradientBrush</param>
        /// <returns><see cref="Color"/></returns>
        /// <exception cref="ArgumentException">Throws if resource is not found or color can not be extracted from resource...</exception>
        public Color GetColorFromThemeResource(string resourceKey, int gradientIndex = 0) {
            if (this._themeStyles.Contains(resourceKey)) {
                if (this._themeStyles[resourceKey] is SolidColorBrush scb) return scb.Color;
                if (this._themeStyles[resourceKey] is DropShadowEffect dse) return dse.Color;
                if (this._themeStyles[resourceKey] is GradientBrush gb) {
                    if (gradientIndex < 0) {
                        gradientIndex = 0;
                    }
                    if (gradientIndex >= gb.GradientStops.Count) {
                        gradientIndex = gb.GradientStops.Count - 1;
                    }
                    return gb.GradientStops[gradientIndex].Color;
                }
                
                if (this._themeStyles[resourceKey] is Color c) return c;

                throw new ArgumentException($"Color can not be extracted from Resource with key \"{resourceKey}\"!");
            }

            throw new ArgumentException($"Resource Key \"{resourceKey}\" not found!");
        }

        /// <summary>
        /// Get the currently loaded theme colors
        /// </summary>
        /// <returns>Array of theme names</returns>
        public string[] GetAvailableThemeColors() {
            return this._themeColorsDefinitions.Keys.ToArray();
        }
    }
}
