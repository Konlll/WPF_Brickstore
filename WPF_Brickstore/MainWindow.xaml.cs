using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<LegoItem> legoItems = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            cbFields.Items.Clear();
            ofd.Filter = "BSX Files |*.bsx";
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                XDocument xml = XDocument.Load(ofd.FileName);
                cbFields.Items.Add("Any");
                cbFields.SelectedIndex = 0;
                foreach(var xmlKey in xml.Descendants("Item").First().Elements().Select(e => e.Name.LocalName))
                {
                    cbFields.Items.Add(xmlKey);
                }
                foreach (var item in xml.Descendants("Item"))
                {
                    LegoItem newItem = new(item.Element("ItemID")?.Value, item.Element("ItemTypeID")?.Value, Convert.ToInt32(item.Element("ColorID")?.Value), item.Element("ItemName")?.Value, item.Element("ItemTypeName")?.Value, item.Element("ColorName")?.Value, Convert.ToInt32(item.Element("CategoryID")?.Value), item.Element("CategoryName")?.Value, item.Element("Status")?.Value, Convert.ToInt32(item.Element("Qty")?.Value), item.Element("Price")?.Value != "0.000" ? Convert.ToDouble(item.Element("Price")?.Value) : 0, item.Element("Condition")?.Value, Convert.ToInt32(item.Element("OrigQty")?.Value));
                    legoItems.Add(newItem);
                }
                dgLegoItems.ItemsSource = legoItems.Select(x => new LegoViewModell(x.ItemID, x.ItemName, x.CategoryName, x.ColorName, Convert.ToInt32(x.Qty))).ToList();
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtFilter.Text != "")
            {
                if (cbFields.SelectedItem == "Any")
                {
                    dgLegoItems.ItemsSource = legoItems.Where(item => item.GetType().GetProperties().Any(prop => prop.GetValue(item)?.ToString().ToLower().Contains(txtFilter.Text.ToString().ToLower()) == true)).Select(x => new LegoViewModell(x.ItemID, x.ItemName, x.CategoryName, x.ColorName, Convert.ToInt32(x.Qty))).ToList();
                }
                else
                {
                    string selectedItem = cbFields.SelectedItem.ToString();
                    dgLegoItems.ItemsSource = legoItems.Where(item => item.GetType().GetProperty(selectedItem)?.GetValue(item)?.ToString().ToLower()?.Contains(txtFilter.Text.ToString().ToLower()) == true).Select(x => new LegoViewModell(x.ItemID, x.ItemName, x.CategoryName, x.ColorName, Convert.ToInt32(x.Qty))).ToList();
                }
            }
        }
    }
}