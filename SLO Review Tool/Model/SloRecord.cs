using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloReviewTool.Model
{
    class SloDefinition
    {
        public string ServiceId { get; set; }
        public string OrganizationName { get; set; }
        public string ServiceGroupName { get; set; }
        public string TeamGroupName { get; set; }
        public string ServiceName { get; set; }
        public string YamlValue { get; set; }
    }
}
