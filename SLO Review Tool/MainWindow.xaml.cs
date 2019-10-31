using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
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

namespace SLO_Review_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var kcsb = new KustoConnectionStringBuilder("https://azurequality.westus2.kusto.windows.net/AzureQuality").WithAadUserPromptAuthentication();
            using (var client = KustoClientFactory.CreateCslQueryProvider(kcsb))
            {
                client.ExecuteQuery("GetSloJsonActionItemReport() | where YamlValue contains ServiceId");
            }
        }
    }
}
