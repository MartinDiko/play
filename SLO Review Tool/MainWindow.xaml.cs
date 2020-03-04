using SloReviewTool;
using SloReviewTool.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace SloReviewTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SloQueryManager queryManager_;
        public string PlaceholderText { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            queryManager_ = new SloQueryManager();

        }

        Task<Tuple<List<SloRecord>, List<SloValidationException>>> ExecuteQueryAsync(string query)
        {
            string updateQuery;
            if (GuidEx.IsGuid(query))
            {
                updateQuery = $"GetSloJsonActionItemReport() | where ServiceId == '{query}'";
                query = updateQuery;

            }
            else if (!GuidEx.IsGuid(query))
            {
                updateQuery = $"GetSloJsonActionItemReport() | where ServiceName contains '{query}'";
                query = updateQuery;
            }
            updateQuery = "";

            return Task.Run(() => {
                return queryManager_.ExecuteQuery(query);
            });

        }

        private async void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            QueryButton.IsEnabled = false;
            QueryStatus.Content = "Executing Query...";

            try
            {

                var results = await ExecuteQueryAsync(QueryTextBox.Text);

                QueryStatus.Content = $"Query returned {results.Item1.Count + results.Item2.Count} record(s), {results.Item2.Count} failed to parse";
                ListCollectionView sloRecords = new ListCollectionView(results.Item1);
                sloRecords.GroupDescriptions.Add(new PropertyGroupDescription("ServiceGroupName"));
                ResultsDataGrid.ItemsSource = sloRecords;
                ErrorListView.ItemsSource = results.Item2;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            QueryButton.IsEnabled = true;
        }

        private void ResultsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ResultsDataGrid.CurrentItem == null) return;
            var sloView = new SloView(ResultsDataGrid.CurrentItem as SloRecord);
            sloView.Show();
        }

        private void ResultsDataGrid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ResultsDataGrid.CurrentItem == null) return;
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftAlt))
            {
                var url = @"https://servicetree.msftcloudes.com/main.html#/ServiceModel/Home/" + (ResultsDataGrid.CurrentItem as SloRecord).ServiceId;
                System.Diagnostics.Process.Start(url);
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            var sloValidator = new SloValidatorView();
            sloValidator.Show();
        }

        public static class GuidEx
        {
            public static bool IsGuid(string value)
            {
                Guid x;
                return Guid.TryParse(value, out x);
            }
        }
    }
}
