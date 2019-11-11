using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloReviewTool.Model
{
    public class SloDataSourceAccountId
    {
        public string AccountId { get; set; }

        public SloDataSourceAccountId(string accountId)
        {
            AccountId = accountId;
        }

        public static List<SloDataSourceAccountId> ParseList(List<object> accountIds)
        {
            var list = new List<SloDataSourceAccountId>();

            foreach (string accountId in accountIds)
            {
                list.Add(new SloDataSourceAccountId(accountId));
            }

            return list;
        }
    }
}
