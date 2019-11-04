using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SloReviewTool.Model
{
    public class SloDataSource
    {
        public SloDataSource(Dictionary<object, object> dataSource)
        {
            Id = dataSource["id"] as string;
            Type = dataSource["type"] as string;
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public List<SloDataSourceAccountId> AccountIds { get; set; }
    }
}
