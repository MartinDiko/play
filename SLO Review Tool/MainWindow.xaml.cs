using SloReviewTool.Model;
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
        bool queryExecuting = false;

        public MainWindow()
        {
            InitializeComponent();
            queryManager_ = new SloQueryManager();
        }

        Task<List<SloDefinition>> ExecuteQuery(string query)
        {
            return Task.Run(() => {
                return queryManager_.ExecuteQuery(query);
            });

        }

        private async void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            if (queryExecuting) return;  // Do not allow multiple queries at once

            QueryStatus.Text = "Executing Query...";
            var results = await ExecuteQuery(QueryTextBox.Text);

            QueryStatus.Text = string.Format("Query returned {0} record(s)", results.Count);
            ResultsDataGrid.ItemsSource = results;
        }
    }
}
