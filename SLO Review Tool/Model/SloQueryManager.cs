using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Threading;
using System.Windows.Documents;

namespace SloReviewTool.Model
{
    class SloQueryManager
    {
        ICslQueryProvider client_;

        public SloQueryManager()
        {
            Initialize();
        }

        void Initialize()
        {
            var kcsb = new KustoConnectionStringBuilder("https://azurequality.westus2.kusto.windows.net/AzureQuality").WithAadUserPromptAuthentication();
            client_ = KustoClientFactory.CreateCslQueryProvider(kcsb);
        }

        public Tuple<List<SloRecord>, List<SloValidationException>> ExecuteQuery(string query)
        {
            var items = new List<SloRecord>();
            var errors = new List<SloValidationException>();

            // "GetSloJsonActionItemReport() | where YamlValue contains ServiceId"
            var results = client_.ExecuteQuery(query);
            using (results)
            {
                for (int i = 0; results.Read(); i++)
                {

                    try
                    {
                        items.Add(ReadSingleResult((IDataRecord)results));
                    }
                    catch (SloValidationException ex)
                    {
                        errors.Add(ex);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Schema violation: {ex.Message}");
                    }
                }
            }

            return Tuple.Create(items, errors);
        }

        SloRecord ReadSingleResult(IDataRecord record)
        {
            var slo = new SloRecord();
            ThreadContext<SloParsingContext>.Set(new SloParsingContext(slo));
            slo.ServiceId = record.GetString(record.GetOrdinal("ServiceId"));
            slo.OrganizationName = record["OrganizationName"] as string;
            slo.ServiceGroupName = record["ServiceGroupName"] as string;
            slo.TeamGroupName = record["TeamGroupName"] as string;
            slo.ServiceName = record["ServiceName"] as string;
            slo.ServiceId = record["ServiceId"] as string;
            slo.SetYamlValue(record["YamlValue"] as string);

            return slo;
        }

    }
}
