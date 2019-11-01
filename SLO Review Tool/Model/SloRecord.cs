using System.Collections.Generic;


namespace SloReviewTool.Model
{
    public class SloRecord
    {
        SloDefinition slo_;

        public string ServiceId { get; set; }
        public string OrganizationName { get; set; }
        public string ServiceGroupName { get; set; }
        public string TeamGroupName { get; set; }
        public string ServiceName { get; set; }
        public string YamlValue { get; private set; }

        public void SetYamlValue(string yaml)
        {
            YamlValue = FormatYaml(yaml);
            slo_ = new SloDefinition(YamlValue);
        }

        public SloDefinition GetSloDefinition()
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
