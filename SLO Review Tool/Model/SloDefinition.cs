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
        public string SloText { get; private set; }
        public string ServiceId {
            get => SloDictionary["service-id"].ToString();
        }
        public Dictionary<object, object> SloDictionary { get; private set; }
        public List<SloDataSource> DataSources { get; private set; }
        public List<SloGroup> SloGroups { get; private set; }


        public SloDefinition(string jsonText)
        {
            SloText = ConvertServiceTreeJsonToYaml(jsonText);
            SloDictionary = ParseSloText(SloText);
            DataSources = SloDataSource.ParseList(SloDictionary["datasources"] as List<object>);
            SloGroups = SloGroup.ParseList(SloDictionary["slo-groups"] as List<object>);
        }

        Dictionary<object, object> ParseSloText(string sloText)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            return deserializer.Deserialize<Dictionary<object, object>>(sloText);
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
