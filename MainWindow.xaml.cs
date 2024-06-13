using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Text.Json;

namespace Lab4_Library__WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Settlement> settlements = new List<Settlement>();
        private const string DataFilePath = "data.json";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            PopulateSettlementList();
        }

        private void LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                var jsonString = File.ReadAllText(DataFilePath);
                settlements = JsonSerializer.Deserialize<List<Settlement>>(jsonString) ?? new List<Settlement>();
            }
            else
            {
                settlements = new List<Settlement>();
            }
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(settlements, options);
            File.WriteAllText(DataFilePath, jsonString);
        }

        private void PopulateSettlementList()
        {
            SettlementListBox.Items.Clear();
            foreach (var settlement in settlements)
            {
                SettlementListBox.Items.Add(settlement.ToString());
            }
        }

        private void AddSettlement_Click(object sender, RoutedEventArgs e)
        {
            var settlementWindow = new SettlementWindow(new Settlement());
            if (settlementWindow.ShowDialog() == true)
            {
                settlements.Add(settlementWindow.Settlement);
                PopulateSettlementList();
            }
        }

        private void EditSettlement_Click(object sender, RoutedEventArgs e)
        {
            if (SettlementListBox.SelectedItem != null)
            {
                int selectedIndex = SettlementListBox.SelectedIndex;
                var settlementWindow = new SettlementWindow(settlements[selectedIndex]);
                if (settlementWindow.ShowDialog() == true)
                {
                    settlements[selectedIndex] = settlementWindow.Settlement;
                    PopulateSettlementList();
                }
            }
        }

        private void SettlementListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditLandPlot.IsEnabled = SettlementListBox.SelectedItem != null;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            SaveData();
            base.OnClosing(e);
        }
    }
}

