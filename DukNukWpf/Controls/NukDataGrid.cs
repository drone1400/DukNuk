using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
namespace DukNuk.Wpf.Controls {
    
    /// <summary>
    /// Experimental custom DataGrid override that automagically sets element styles for data columns...
    /// </summary>
    /// <remarks>
    /// I ended up not using this for now...
    /// </remarks>
    public class NukDataGrid : DataGrid {


        public static readonly DynamicResourceExtension ExtensionCellStretchStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.Cell.StretchContent");
        public static readonly DynamicResourceExtension ExtensionCheckBoxColumnElementStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.CheckBox");
        public static readonly DynamicResourceExtension ExtensionCheckBoxColumnEditingElementStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.CheckBox.Editing");
        public static readonly DynamicResourceExtension ExtensionComboBoxColumnElementStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.ComboBox");
        public static readonly DynamicResourceExtension ExtensionComboBoxColumnEditingElementStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.ComboBox.Editing");
        public static readonly DynamicResourceExtension ExtensionTextBoxColumnElementStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.TextBox");
        public static readonly DynamicResourceExtension ExtensionTextBoxColumnEditingElementStyle = new DynamicResourceExtension( "DukNuk.Style.DataGrid.TextBox.Editing");

        public override void EndInit() {
            base.EndInit();

            this.RefreshColumnElementStyles();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            if (e.Property.Name == nameof(this.Columns)) {
                // columns were changed, so let's try setting all the styles!
                this.RefreshColumnElementStyles();
            }
            
            base.OnPropertyChanged(e);
        }

        protected virtual void RefreshColumnElementStyles() {
            foreach (var c in this.Columns) {
                if (c is DataGridCheckBoxColumn dck) {
                    dck.SetValue(DataGridCheckBoxColumn.ElementStyleProperty, NukDataGrid.ExtensionCheckBoxColumnElementStyle.ProvideValue(null));
                    dck.SetValue(DataGridCheckBoxColumn.EditingElementStyleProperty, NukDataGrid.ExtensionCheckBoxColumnEditingElementStyle.ProvideValue(null));
                }
                else if (c is DataGridComboBoxColumn dco) {
                    dco.SetValue(DataGridTemplateColumn.CellStyleProperty, NukDataGrid.ExtensionCellStretchStyle.ProvideValue(null));
                    dco.SetValue(DataGridComboBoxColumn.ElementStyleProperty, NukDataGrid.ExtensionComboBoxColumnElementStyle.ProvideValue(null));
                    dco.SetValue(DataGridComboBoxColumn.EditingElementStyleProperty, NukDataGrid.ExtensionComboBoxColumnEditingElementStyle.ProvideValue(null));
                }
                else if (c is DataGridTextColumn dto) {
                    dto.SetValue(DataGridTextColumn.CellStyleProperty, NukDataGrid.ExtensionCellStretchStyle.ProvideValue(null));
                    dto.SetValue(DataGridTextColumn.ElementStyleProperty, NukDataGrid.ExtensionTextBoxColumnElementStyle.ProvideValue(null));
                    dto.SetValue(DataGridTextColumn.EditingElementStyleProperty, NukDataGrid.ExtensionTextBoxColumnEditingElementStyle.ProvideValue(null));
                }
                else if (c is DataGridTemplateColumn dt) {
                    dt.SetValue(DataGridTemplateColumn.CellStyleProperty, NukDataGrid.ExtensionCellStretchStyle.ProvideValue(null));
                }
            }
        }
    }
}
