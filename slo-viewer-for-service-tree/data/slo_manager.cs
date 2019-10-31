using Kusto.Data;
using Kusto.Data.Net.Client;

namespace slo_viewer_for_service_tree.data
{
    class slo_manager
    {
        public void ConnectToKusto()
        {
            var kcsb = new KustoConnectionStringBuilder("https://azurequality.westus2.kusto.windows.net/AzureQuality").WithAadUserPromptAuthentication();
            using (var client = KustoClientFactory.CreateCslAdminProvider(kcsb))
            {
                var reader = client.ExecuteQuery("GetSloJsonActionItemReport() where YamlValue contains ServiceId");
            }
        }
    }
}
