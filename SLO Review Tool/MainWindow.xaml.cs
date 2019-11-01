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

        public MainWindow()
        {
            InitializeComponent();
            queryManager_ = new SloQueryManager();
        }

        Task<List<SloDefinition>> ExecuteQueryAsync(string query)
        {
            return Task.Run(() => {
                return queryManager_.ExecuteQuery(query);
            });

        }

        private async void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            QueryButton.IsEnabled = false;
       
            QueryStatus.Text = "Executing Query...";
            var results = await ExecuteQueryAsync(QueryTextBox.Text);

            QueryStatus.Text = string.Format("Query returned {0} record(s)", results.Count);
            ResultsDataGrid.ItemsSource = results;

            QueryButton.IsEnabled = true;
        }
    }
}
