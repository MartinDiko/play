using Kusto.Cloud.Platform.Utils;
using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
using Kusto.Ingest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SloReviewTool.Model
{
    class SloQueryManager
    {
        ICslQueryProvider client_;

        readonly string kustoUrl_ = "https://azurequality.westus2.kusto.windows.net/AzureQuality";
        readonly string kustoDb_ = "AzureQuality";
        readonly string kustoManualReviewTable_ = "SloDefinitionManualReview";

        public SloQueryManager()
        {
            Initialize();
        }

        void Initialize()
        {
            var kcsb = new KustoConnectionStringBuilder(kustoUrl_).WithAadUserPromptAuthentication();
            client_ = KustoClientFactory.CreateCslQueryProvider(kcsb);
        }

        public Tuple<List<SloRecord>, List<SloValidationException>> ExecuteQuery(string query)
        {
            var items = new List<SloRecord>();
            var errors = new List<SloValidationException>();
            var uniqueServiceIds = new SortedSet<string>();

            // Append to the query the join to get the review data
            query += @"| project ServiceId, OrganizationName, ServiceGroupName, TeamGroupName, ServiceName, YamlValue, ServiceIdGuid = toguid(ServiceId) | join kind = leftouter SloDefinitionManualReview on $left.ServiceIdGuid == $right.ServiceId | sort by ReviewDate desc ";

            // "GetSloJsonActionItemReport() | where YamlValue contains ServiceId"
            using (var results = client_.ExecuteQuery(query)) {
                for (int i = 0; results.Read(); i++) {
                    try {
                        var result = ReadSingleResult((IDataRecord)results);

                        // Only add the latest value
                        if (!uniqueServiceIds.Contains(result.ServiceId)) {
                            items.Add(result);
                            uniqueServiceIds.Add(result.ServiceId);
                        }

                    } catch(SloValidationException ex) {
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
            slo.ServiceId = record["ServiceId"] as string;
            slo.OrganizationName = record["OrganizationName"] as string;
            slo.ServiceGroupName = record["ServiceGroupName"] as string;
            slo.TeamGroupName = record["TeamGroupName"] as string;
            slo.ServiceName = record["ServiceName"] as string;
            slo.SetYamlValue(record["YamlValue"] as string);
            if (!record.IsDBNull(record.GetOrdinal("ReviewPassed"))) slo.ReviewPassed = ((sbyte) record.GetValue(record.GetOrdinal("ReviewPassed")) != 0);
            slo.ReviewDetails = record["ReviewDetails"] as string;
            if(!record.IsDBNull(record.GetOrdinal("ReviewDate"))) slo.ReviewDate = record.GetDateTime(record.GetOrdinal("ReviewDate"));
            slo.ReviewedBy = record["ReviewedBy"] as string;

            return slo;
        }

        public async Task PublishManualReviews(IEnumerable<SloManualReview> results)
        {
            var dt = results.ToDataTable();

            var kustoConnectionStringBuilderEngine = new KustoConnectionStringBuilder(kustoUrl_).WithAadUserPromptAuthentication();

            using (var client = KustoIngestFactory.CreateDirectIngestClient(kustoConnectionStringBuilderEngine))
            {
                //Ingest from blobs according to the required properties
                var kustoIngestionProperties = new KustoIngestionProperties(
                    databaseName: kustoDb_,
                    tableName: kustoManualReviewTable_
                );

                var reader = dt.CreateDataReader();
                var result = await client.IngestFromDataReaderAsync(reader, kustoIngestionProperties);
            }
        }

    }
}
