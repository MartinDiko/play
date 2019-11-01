using System.Collections.Generic;


namespace SloReviewTool.Model
{
    public class SloRecord
    {
        string yamlValue_;
        SloDefinition slo_;

        public string ServiceId { get; set; }
        public string OrganizationName { get; set; }
        public string ServiceGroupName { get; set; }
        public string TeamGroupName { get; set; }
        public string ServiceName { get; set; }
        public string YamlValue {
            get {
                return yamlValue_;
            }
        }

        public void SetYamlValue(string yaml)
        {
            yamlValue_ = FormatYaml(yaml);
            slo_ = new SloDefinition(yamlValue_);
        }

        public SloDefinition GetYamlDefinition()
        {
            return slo_;
        }

        string FormatYaml(string rawText)
        {
            string yaml = rawText.Replace(@"\n", "\n");
            return yaml;
        }
    }
}
