using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloReviewTool.Model
{
    public class SloDefinition
    {
        Dictionary<string, object> slo_;

        public SloDefinition(string jsonText)
        {
            SloText = ConvertServiceTreeJsonToYaml(jsonText);
            slo_ = ParseSloText(SloText);
        }

        public string SloText { get; private set; }

        public string ServiceId {
            get {
                return slo_["service-id"].ToString();
            }
        }

        Dictionary<string, object> ParseSloText(string sloText)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            return deserializer.Deserialize<Dictionary<string, object>>(sloText);
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
