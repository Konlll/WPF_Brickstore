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
            legoItems = new();
            OpenFileDialog ofd = new OpenFileDialog();
            cbFields.Items.Clear();
            ofd.Filter = "BSX Files |*.bsx";
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                XDocument xml = XDocument.Load(ofd.FileName);
                foreach(var xmlKey in xml.Descendants("Item").First().Elements().Select(e => e.Name.LocalName))
                {
                    cbFields.Items.Add(xmlKey);
                }
                foreach (var item in xml.Descendants("Item"))
                {
                    LegoItem newItem = new(item.Element("ItemID")?.Value, item.Element("ItemTypeID")?.Value, Convert.ToInt32(item.Element("ColorID")?.Value), item.Element("ItemName")?.Value, item.Element("ItemTypeName")?.Value, item.Element("ColorName")?.Value, Convert.ToInt32(item.Element("CategoryID")?.Value), item.Element("CategoryName")?.Value, item.Element("Status")?.Value, Convert.ToInt32(item.Element("Qty")?.Value), item.Element("Price")?.Value != "0.000" ? Convert.ToDouble(item.Element("Price")?.Value) : 0, item.Element("Condition")?.Value, Convert.ToInt32(item.Element("OrigQty")?.Value));
                    legoItems.Add(newItem);
                }
                txtLegoItems.Text = $"Lego items ({legoItems.Count.ToString()})";
                dgLegoItems.ItemsSource = legoItems.Select(x => new LegoViewModell(x.ItemID, x.ItemName, x.CategoryName, x.ColorName, Convert.ToInt32(x.Qty))).ToList();
            }
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbFilter.SelectedItem != null)
            {
                string selectedItem = cbFilter.SelectedItem.ToString();
                dgLegoItems.ItemsSource = legoItems.Where(item => item.GetType().GetProperty(cbFields.SelectedItem.ToString())?.GetValue(item)?.ToString().ToLower() == selectedItem.ToLower()).Select(x => new LegoViewModell(x.ItemID, x.ItemName, x.CategoryName, x.ColorName, Convert.ToInt32(x.Qty))).ToList();
            }
        }

        private void cbFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgLegoItems.ItemsSource = legoItems.Select(x => new LegoViewModell(x.ItemID, x.ItemName, x.CategoryName, x.ColorName, Convert.ToInt32(x.Qty))).ToList(); ;
            string selectedProperty = cbFields.SelectedItem.ToString();
            List<string> listboxItems = legoItems.Select(x => x.GetType().GetProperty(selectedProperty).GetValue(x).ToString()).ToList();
            cbFilter.ItemsSource = listboxItems.Distinct().Order();
        }
    }
}