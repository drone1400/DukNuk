using System.Collections.ObjectModel;
using System.Windows;
using DukNuk.Wpf.Helpers;
using DukNuk.Wpf.Mvvm;
namespace DukNuk.Wpf.Test.ViewModels {
 

    public class MainWindowViewModel : DukViewModel {

        public MainWindowViewModel() {
            this.InitializeRandomDataGridData();
        }

        public Thickness MyMargin {
            get => this._myMargin;
            set => this.SetField(ref this._myMargin, value);
        }
        private Thickness _myMargin = new Thickness(6);

        
        public List<ListViewTestData> ListViewData {
            get => this._listViewData;
            set => this.SetField(ref this._listViewData, value);
        }
        private List<ListViewTestData> _listViewData = new List<ListViewTestData>() {
            new ListViewTestData() { Name = "Entry 1", Value = "Value 1" },
            new ListViewTestData() { Name = "Entry 2", Value = "Value 2" },
            new ListViewTestData() { Name = "Entry 3", Value = "Value 3" },
            new ListViewTestData() { Name = "Entry 4", Value = "Value 4" },
            new ListViewTestData() { Name = "Entry 5", Value = "Value 5" },
            new ListViewTestData() { Name = "Entry 6", Value = "Value 6" },
            new ListViewTestData() { Name = "Entry 7", Value = "Value 7" },
            new ListViewTestData() { Name = "Entry 8", Value = "Value 8" },
            new ListViewTestData() { Name = "Entry 9", Value = "Value 9" },
            new ListViewTestData() { Name = "Entry 10", Value = "Value 10" },
            new ListViewTestData() { Name = "Entry 11", Value = "Value 11" },
            new ListViewTestData() { Name = "Entry 12", Value = "Value 12" },
            new ListViewTestData() { Name = "Entry 13", Value = "Value 13" },
            new ListViewTestData() { Name = "Entry 14", Value = "Value 14" },
            new ListViewTestData() { Name = "Entry 15", Value = "Value 15" },
            new ListViewTestData() { Name = "Entry 16", Value = "Value 16" },
            new ListViewTestData() { Name = "Entry 17", Value = "Value 17" },
            new ListViewTestData() { Name = "Entry 18", Value = "Value 18" },
            new ListViewTestData() { Name = "Entry 19", Value = "Value 19" },
        };
        
        public ObservableCollection<DataGridTestData> DataGridData {
            get => this._dataGridData;
            set => this.SetField(ref this._dataGridData, value);
        }
        private ObservableCollection<DataGridTestData> _dataGridData = new ObservableCollection<DataGridTestData>();

        public static List<string> DataGridOptions { get; } = new List<string>() { "Option 1", "Option 2", "Option 3" };
        public List<string> DataGridOptions2 { get; } = new List<string>() { "Option 1", "Option 2", "Option 3" };

        public void InitializeRandomDataGridData() {
            List<DataGridTestData> data = new List<DataGridTestData>();
            Random rand = new Random();
            for (int i = 0; i < 25; i++) {
                DataGridTestData t = new DataGridTestData {
                    Name = $"Entry #{i}",
                    Value1 = rand.NextDouble(),
                    Value2 = rand.Next(),
                    Value3 = rand.Next() % 2 == 0,
                    SelectedOption = "Option 1",
                };
                data.Add(t);
            }
            this._dataGridData = new ObservableCollection<DataGridTestData>(data);
        }

        public List<string> AvailableThemes {
            get => this._availableThemes;
            set => this.SetField(ref this._availableThemes, value);
        }
        private List<string> _availableThemes = new List<string>();

        public string SelectedTheme {
            get => this._selectedTheme;
            set {
                if (ThemeManager.Default.CurrentTheme != value && ThemeManager.Default.SetTheme(value)) {
                    this.SetField(ref this._selectedTheme, value);
                }
            }
        }
        private string _selectedTheme = "";

        public void InitializeThemes() {
            var themes = ThemeManager.Default.GetAvailableThemeColors();
            this.AvailableThemes = themes.ToList();
            this._selectedTheme = ThemeManager.Default.CurrentTheme;
            this.OnPropertyChanged(nameof(this.SelectedTheme));
        }

        public void RefreshThemes() {
            var themes = ThemeManager.Default.GetAvailableThemeColors();
            this.AvailableThemes = themes.ToList();
        }
    }
}
