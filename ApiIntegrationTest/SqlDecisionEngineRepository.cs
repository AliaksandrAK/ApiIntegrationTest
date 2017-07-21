using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ApiIntegrationTest
{
    class SqlDecisionEngineRepository
    {
        static string connectionString = @"Data Source=dev1dbvip.servicechannel.com;Initial Catalog=dbGoodData;Integrated Security=False;Connect Timeout=180;User ID=akusonski;Password=ak347ASK4";
        public void UpdateProposalRecommendationCutoff(IEnumerable<DecisionEngineCutoff> propRec)
        {
            string sql = "SELECT * FROM gooddata_decision_engine_recommendation_cutoff with(nolock)";
            //set ProposalsId
            string propId = string.Format(" Where proposal_id={0}", propRec.FirstOrDefault().proposal_id.ToString());
            foreach (DecisionEngineCutoff propIn in propRec)
            {
                if(!ReferenceEquals(propIn, propRec.FirstOrDefault())) 
                    propId += string.Format(" OR proposal_id={0}", propIn.proposal_id);
            }
            sql += propId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                var ret = adapter.Fill(ds);

                DataTable dt = ds.Tables[0];

                foreach (DecisionEngineCutoff propIn in propRec)
                {
                    var dtItem = dt.AsEnumerable().Where(p => p.Field<Int32>("proposal_id") == propIn.proposal_id).FirstOrDefault();
                    if (dtItem != null)
                    {
                        if (decimal.Compare((decimal)dtItem["recommendation"], propIn.recommendation) != 0 ||
                            (propIn.cutoff_approve1.HasValue &&
                             decimal.Compare((decimal)dtItem["cutoff_approve1"], propIn.cutoff_approve1.Value) != 0) ||
                            (propIn.cutoff_approve2.HasValue &&
                             decimal.Compare((decimal)dtItem["cutoff_approve2"], propIn.cutoff_approve2.Value) != 0) ||
                            (propIn.cutoff_reject1.HasValue &&
                             decimal.Compare((decimal)dtItem["cutoff_reject1"], propIn.cutoff_reject1.Value)!= 0) ||
                            (propIn.cutoff_reject2.HasValue &&
                             decimal.Compare((decimal)dtItem["cutoff_reject2"], propIn.cutoff_reject2.Value) != 0)
                            )
                        {
                            dtItem["recommendation"] = propIn.recommendation;
                            dtItem["cutoff_approve1"] = propIn.cutoff_approve1;
                            dtItem["cutoff_approve2"] = propIn.cutoff_approve2;
                            dtItem["cutoff_reject1"] = propIn.cutoff_reject1;
                            dtItem["cutoff_reject2"] = propIn.cutoff_reject2;
                        }
                    }
                    else
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["proposal_id"] = propIn.proposal_id;
                        newRow["subscriber_id"] = propIn.subscriber_id;
                        newRow["workspace_id"] = propIn.workspace_id;
                        newRow["recommendation"] = propIn.recommendation;
                        newRow["cutoff_approve1"] = propIn.cutoff_approve1;
                        newRow["cutoff_approve2"] = propIn.cutoff_approve2;
                        newRow["cutoff_reject1"] = propIn.cutoff_reject1;
                        newRow["cutoff_reject2"] = propIn.cutoff_reject2;
                        dt.Rows.Add(newRow);
                    }
                }

                //Delete items for test
//                for (int i = dt.Rows.Count - 1; i >= 0; i--)
//                {
//                    DataRow dr = dt.Rows[i];
//                    if ((int)dr["proposal_id"] < 2000)dr.Delete();
//                }

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dt);
/*
                ds.Clear();
                adapter.Fill(ds);

                foreach (DataColumn column in dt.Columns)
                    Console.Write("\t{0}", column.ColumnName);
                Console.WriteLine();
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        Console.Write("\t{0}", cell);
                    Console.WriteLine();
                }
                */
            }
        }
    }
}
