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
        public string SloYaml { get; private set; }
        public string ServiceId {
            get => SloValidator.GetString(this.GetType().Name, SloDictionary, "service-id");
        }
        public Dictionary<object, object> SloDictionary { get; private set; }
        public List<SloDataSource> DataSources { get; private set; }
        public List<SloGroup> SloGroups { get; private set; }


        public SloDefinition(string sloJson)
        {
            SloYaml = ConvertServiceTreeJsonToYaml(sloJson);
            ThreadContext<SloParsingContext>.ForThread().Yaml = SloYaml;
            SloDictionary = ParseSloText(SloYaml);

            DataSources = SloDataSource.ParseList(SloValidator.GetList(this.GetType().Name, SloDictionary, "datasources"));
            SloGroups = SloGroup.ParseList(SloValidator.GetList(this.GetType().Name, SloDictionary, "slo-groups"));
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
