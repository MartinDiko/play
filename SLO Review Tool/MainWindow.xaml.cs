using SloReviewTool;
using SloReviewTool.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;


namespace SloReviewTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SloQueryManager queryManager_;

        public MainWindow()
        {
            InitializeComponent();
            queryManager_ = new SloQueryManager();
        }

        Task<List<SloRecord>> ExecuteQueryAsync(string query)
        {
            return Task.Run(() => {
                return queryManager_.ExecuteQuery(query);
            });

        }

        private async void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            QueryButton.IsEnabled = false;
            QueryStatus.Content = "Executing Query...";

            try {
                var results = await ExecuteQueryAsync(QueryTextBox.Text);

                QueryStatus.Content = string.Format("Query returned {0} record(s)", results.Count);
                ResultsDataGrid.ItemsSource = results;

            } catch(Exception ex) {
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
    }
}
