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
        public string Id { get; set; }
        public string Type { get; set; }
        public List<SloDataSourceAccountId> AccountIds { get; set; }

        public SloDataSource(Dictionary<object, object> dataSource)
        {
            Id = SloValidator.GetString(this.GetType().Name, dataSource, "id");
            Type = SloValidator.GetString(this.GetType().Name, dataSource, "type");
        }

        public static List<SloDataSource> ParseList(List<object> dataSources)
        {
            var list = new List<SloDataSource>();

            foreach (Dictionary<object, object> dataSource in dataSources) {
                list.Add(new SloDataSource(dataSource));
            }

            return list;
        }

    }
}
