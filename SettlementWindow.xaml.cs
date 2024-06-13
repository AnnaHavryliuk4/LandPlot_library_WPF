using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab4_Library__WPF
{
    /// <summary>
    /// Interaction logic for SettlementWindow.xaml
    /// </summary>
    public partial class SettlementWindow : Window
    {
        public Settlement Settlement { get; private set; }

        public SettlementWindow(Settlement settlement)
        {
            InitializeComponent();
            Settlement = settlement;
            PopulateLandPlotList();   
        }

        private void PopulateLandPlotList()
        {
            LandPlotListBox.Items.Clear();
            foreach (var landPlot in Settlement.LandPlots)
            {
                LandPlotListBox.Items.Add(landPlot.ToString());
            }
        }

        private void AddLandPlot_Click(object sender, RoutedEventArgs e)
        {
            var landPlotWindow = new LandPlotWindow(new LandPlot());
            if (landPlotWindow.ShowDialog() == true)
            {
                Settlement.LandPlots.Add(landPlotWindow.LandPlot);
                PopulateLandPlotList();
            }
        }

        private void EditLandPlot_Click(object sender, RoutedEventArgs e)
        {
            if (LandPlotListBox.SelectedItem != null)
            {
                int selectedIndex = LandPlotListBox.SelectedIndex;
                var landPlotWindow = new LandPlotWindow(Settlement.LandPlots[selectedIndex]);
                if (landPlotWindow.ShowDialog() == true)
                {
                    Settlement.LandPlots[selectedIndex] = landPlotWindow.LandPlot;
                    PopulateLandPlotList();
                }
            }
        }

        private void LandPlotListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditLandPlot.IsEnabled = LandPlotListBox.SelectedItem != null;
        }

        private void SaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = true;
                Close();
            }
        }

        private void CancelAndClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
