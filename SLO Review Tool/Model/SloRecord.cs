using System;
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
        public string ReviewDetails { get; set; }
        public bool ReviewPassed { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewedBy { get; set; }

        public void SetYamlValue(string yaml)
        {
            YamlValue = yaml;
            slo_ = SloDefinition.CreateFromServiceTreeJson(YamlValue);
        }

        public SloDefinition SloDefinition
        {
            get { return slo_; }
        }
    }
}
