using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SloReviewTool.Model
{
    public class SloDefinition
    {
        Dictionary<string, object> sloDictionary_;
        List<SloDataSource> dataSources_ = new List<SloDataSource>();

        public SloDefinition(string jsonText)
        {
            SloText = ConvertServiceTreeJsonToYaml(jsonText);
            sloDictionary_ = ParseSloText(SloText);
            PopulateData();
        }

        public string SloText { get; private set; }

        public string ServiceId {
            get {
                return sloDictionary_["service-id"].ToString();
            }
        }

        public Dictionary<string, object> SloDictionary {
            get => sloDictionary_;
        }

        public List<SloDataSource> DataSources {
            get => dataSources_;
        }

        Dictionary<string, object> ParseSloText(string sloText)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            return deserializer.Deserialize<Dictionary<string, object>>(sloText);
        }

        void PopulateData()
        {
            ParseDataSources(sloDictionary_["datasources"] as List<object>);
        }

        void ParseDataSources(List<object> dataSources)
        {
            foreach (var dataSource in dataSources) {
                try {
                    dataSources_.Add(new SloDataSource(dataSource as Dictionary<object, object>));
                } catch(Exception ex) {
                    Debug.WriteLine($"Caught exception parsing datasource: {ex.Message}");
                }
            }
        }

        string ConvertServiceTreeJsonToYaml(string rawText)
        {
            rawText = rawText.Replace(@"\n", "\n");

            // For now, parse JSON to get SLO yaml.  Service Tree should be fixed.
            var json = JObject.Parse(rawText);
            var yaml = json["ServiceLevelObjectives"].ToString();

            return yaml;
        }
    }
}
