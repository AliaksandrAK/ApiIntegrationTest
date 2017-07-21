using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegrationTest
{
    class DecisionEngineProcess
    {
        public IEnumerable<DecisionEngineCutoff> ConvertToDecisionEngineCutoff(ReportData reportData)
        {
            List<DecisionEngineCutoff> deCutoffCollection = new List<DecisionEngineCutoff>();

            if (reportData == null ||
                reportData.xtab_data == null ||
                reportData.xtab_data.data == null)
            {
                throw new Exception(string.Format("GoodData Report Result is Empty"));
            }

            if (reportData.xtab_data.rows == null ||
                reportData.xtab_data.rows.lookups.Count() < 3 ||
                reportData.xtab_data.rows.tree.children == null ||
                !reportData.xtab_data.rows.tree.children.Any())
            {
                throw new Exception(string.Format("GoodData Report Result is Empty"));
            }

            foreach (var rootChild in reportData.xtab_data.rows.tree.children)
            {
                DecisionEngineCutoff deCutoff = new DecisionEngineCutoff();
                deCutoff.proposal_id = int.Parse(reportData.xtab_data.rows.lookups[0][rootChild.id]);
                deCutoff.subscriber_id = int.Parse(reportData.xtab_data.rows.lookups[1][rootChild.children[0].id]);
                deCutoff.workspace_id = reportData.xtab_data.rows.lookups[2][rootChild.children[0].children[0].id];

                if (reportData.xtab_data.data[rootChild.first].Count() == 5)
                {
                    var recomendationString = reportData.xtab_data.data[rootChild.first].First();
                    decimal recomendationValue;
                    decimal cutoffApprove1Value;
                    decimal cutoffApprove2Value;
                    decimal cutoffReject1Value;
                    decimal cutoffReject2Value;

                    if (!string.IsNullOrWhiteSpace(recomendationString) &&
                        decimal.TryParse(recomendationString, NumberStyles.Float, CultureInfo.InvariantCulture, out recomendationValue) &&

                        !string.IsNullOrWhiteSpace(reportData.xtab_data.data[rootChild.first][1]) &&
                        decimal.TryParse(reportData.xtab_data.data[rootChild.first][1], NumberStyles.Float, CultureInfo.InvariantCulture, out cutoffApprove1Value) &&

                        !string.IsNullOrWhiteSpace(reportData.xtab_data.data[rootChild.first][2]) &&
                        decimal.TryParse(reportData.xtab_data.data[rootChild.first][2], NumberStyles.Float, CultureInfo.InvariantCulture, out cutoffApprove2Value) &&

                        !string.IsNullOrWhiteSpace(reportData.xtab_data.data[rootChild.first][3]) &&
                        decimal.TryParse(reportData.xtab_data.data[rootChild.first][3], NumberStyles.Float, CultureInfo.InvariantCulture, out cutoffReject1Value) &&

                        !string.IsNullOrWhiteSpace(reportData.xtab_data.data[rootChild.first][4]) &&
                        decimal.TryParse(reportData.xtab_data.data[rootChild.first][4], NumberStyles.Float, CultureInfo.InvariantCulture, out cutoffReject2Value))
                    {
                        deCutoff.recommendation = recomendationValue;
                        deCutoff.cutoff_approve1 = cutoffApprove1Value;
                        deCutoff.cutoff_approve2 = cutoffApprove2Value;
                        deCutoff.cutoff_reject1 = cutoffReject1Value;
                        deCutoff.cutoff_reject2 = cutoffReject2Value;
                    }
                }

                deCutoffCollection.Add(deCutoff);
            }
            return deCutoffCollection;
        }
        public static bool CompareRecommendation(DecisionEngineCutoff obj, DecisionEngineCutoff another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            if (decimal.Compare(obj.recommendation, another.recommendation) == 0 &&
                (obj.cutoff_approve1.HasValue && another.cutoff_approve1.HasValue &&
                 decimal.Compare(obj.cutoff_approve1.Value, another.cutoff_approve1.Value) == 0) &&
                (obj.cutoff_approve2.HasValue && another.cutoff_approve2.HasValue &&
                 decimal.Compare(obj.cutoff_approve2.Value, another.cutoff_approve2.Value) == 0) &&
                (obj.cutoff_reject1.HasValue && another.cutoff_reject1.HasValue &&
                 decimal.Compare(obj.cutoff_reject1.Value, another.cutoff_reject1.Value) == 0) &&
                (obj.cutoff_reject2.HasValue && another.cutoff_reject2.HasValue &&
                 decimal.Compare(obj.cutoff_reject2.Value, another.cutoff_reject2.Value) == 0)
                 )
            {
                return true;
            }

            return false;
        }
    }
}
