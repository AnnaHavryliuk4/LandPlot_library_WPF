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
    /// Interaction logic for LandPlotWindow.xaml
    /// </summary>
    public partial class LandPlotWindow : Window
    {
        public LandPlot LandPlot { get; set; }

        private bool _isSaved = false;
        private LandPlot _originalLandPlot;

        public LandPlotWindow(LandPlot landPlot)
        {
            InitializeComponent();
            LandPlot = landPlot;
            _originalLandPlot = CloneLandPlot(LandPlot);
            DataContext = LandPlot;

            if (LandPlot != null)
            {
                if (LandPlot.Owner != null)
                {
                    OwnerFirstNameTextBox.Text = LandPlot.Owner.FirstName;
                    OwnerLastNameTextBox.Text = LandPlot.Owner.LastName;
                    OwnerBirthDatePicker.SelectedDate = LandPlot.Owner.BirthDate;
                }

                if (LandPlot.Description != null)
                {
                    SoilTypeTextBox.Text = LandPlot.Description.SoilType;
                    GroundwaterLevelTextBox.Text = LandPlot.Description.GroundwaterLevel.ToString();
                }

                MarketValueTextBox.Text = LandPlot.MarketValue.ToString();

                PurposeComboBox.SelectedIndex = (int)LandPlot.Purpose;
            }
        }

        private LandPlot CloneLandPlot(LandPlot original)
        {
            return new LandPlot
            {
                Owner = new Owner(
                    original.Owner?.FirstName,
                    original.Owner?.LastName,
                    original.Owner?.BirthDate ?? DateTime.Now
                ),
                Description = new Description(
                    original.Description?.GroundwaterLevel ?? 0,
                    original.Description?.SoilType,
                    original.Description?.GeodeticReference ?? new List<(double, double)>()
                ),
                Purpose = original.Purpose,
                MarketValue = original.MarketValue
            };
        }

        private bool IsDataChanged()
        {
            return !_originalLandPlot.Equals(LandPlot);
        }

        private bool IsValidString(string input)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(input, @"\d");
        }

        private void SaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(GroundwaterLevelTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid integer for Groundwater Level.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(MarketValueTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid number for Market Value.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidString(OwnerFirstNameTextBox.Text) || !IsValidString(OwnerLastNameTextBox.Text) || !IsValidString(SoilTypeTextBox.Text))
            {
                MessageBox.Show("Please enter valid text without digits for the name fields and soil type.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LandPlot.Owner = new Owner(
                OwnerFirstNameTextBox.Text,
                OwnerLastNameTextBox.Text,
                OwnerBirthDatePicker.SelectedDate ?? DateTime.Now
            );

            LandPlot.Description = new Description(
                int.Parse(GroundwaterLevelTextBox.Text),
                SoilTypeTextBox.Text,
                new List<(double, double)> { (0, 0), (1, 1) }
            );

            LandPlot.Purpose = (Purpose)PurposeComboBox.SelectedIndex;

            LandPlot.MarketValue = double.Parse(MarketValueTextBox.Text);

            var result = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _isSaved = true;
                DialogResult = true;
                Close();
            }
        }

        private void CancelAndClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!_isSaved && IsDataChanged())
            {
                var result = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveAndClose_Click(this, new RoutedEventArgs());
                }
                else if (result == MessageBoxResult.No)
                {
                    DialogResult = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }

            base.OnClosing(e);
        }
    }
}
