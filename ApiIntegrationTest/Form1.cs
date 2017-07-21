using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ApiIntegrationTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            DecisionEngineTest();

            ParserRegex.JsonSerializeTest();
        }

        void DecisionEngineTest()
        {
            SqlDecisionEngineRepository sqlQ = new SqlDecisionEngineRepository();

            for (int p = 0; p < 1000; p++)
            {
                List<DecisionEngineCutoff> deA = new List<DecisionEngineCutoff>();
                for (int i = 1; i < 251; i++)
                {
                    DecisionEngineCutoff de1 = new DecisionEngineCutoff
                    {
                        proposal_id = 1,
                        subscriber_id = 2000001305,
                        workspace_id = "w2epgxz8w237gjmqdhxyk5u596mrloj0",
                        recommendation = 0.89m,
                        cutoff_approve1 = 1.00m,
                        cutoff_approve2 = 0.69m,
                        cutoff_reject1 = 1.00m,
                        cutoff_reject2 = 0.68m
                    };
                    de1.proposal_id = (p*251 + i);
                    deA.Add(de1);
                }
                sqlQ.UpdateProposalRecommendationCutoff(deA);
            }

            string sjson1_5 = "{\"xtab_data\":{\"columns\":{\"tree\":{\"index\":{\"4\":[4],\"1\":[1],\"3\":[3],\"0\":[0],\"2\":[2]},\"first\":0,\"last\":4,\"children\":[{\"index\":{},\"first\":0,\"last\":0,\"children\":[],\"type\":\"metric\",\"id\":\"0\"},{\"index\":{},\"first\":1,\"last\":1,\"children\":[],\"type\":\"metric\",\"id\":\"1\"},{\"index\":{},\"first\":2,\"last\":2,\"children\":[],\"type\":\"metric\",\"id\":\"2\"},{\"index\":{},\"first\":3,\"last\":3,\"children\":[],\"type\":\"metric\",\"id\":\"3\"},{\"index\":{},\"first\":4,\"last\":4,\"children\":[],\"type\":\"metric\",\"id\":\"4\"}],\"type\":\"root\",\"id\":null},\"lookups\":[{\"4\":\"cutoff_reject2\",\"1\":\"cutoff_approve1\",\"3\":\"cutoff_reject1\",\"0\":\"recommendation\",\"2\":\"cutoff_approve2\"}]},\"data\":[[\"0.81\",\"0.92\",\"0.45\",\"0.07\",\"0.03\"],[\"0.88\",\"0.92\",\"0.45\",\"0.07\",\"0.03\"],[\"0.88\",\"0.92\",\"0.45\",\"0.07\",\"0.03\"],[\"0.45\",\"0.51\",\"0.51\",\"0.51\",\"0.51\"],[\"0.47\",\"0.51\",\"0.51\",\"0.51\",\"0.51\"]],\"overall_size\":{\"columns\":5,\"rows\":5},\"rows\":{\"tree\":{\"index\":{\"123\":[3],\"134\":[2],\"148\":[0],\"140\":[1],\"126\":[4]},\"first\":0,\"last\":4,\"children\":[{\"index\":{\"129\":[0]},\"first\":0,\"last\":0,\"children\":[{\"index\":{\"83\":[0]},\"first\":0,\"last\":0,\"children\":[{\"index\":{},\"first\":0,\"last\":0,\"children\":[],\"type\":\"normal\",\"id\":\"83\"}],\"type\":\"normal\",\"id\":\"129\"}],\"type\":\"normal\",\"id\":\"148\"},{\"index\":{\"129\":[0]},\"first\":1,\"last\":1,\"children\":[{\"index\":{\"83\":[0]},\"first\":1,\"last\":1,\"children\":[{\"index\":{},\"first\":1,\"last\":1,\"children\":[],\"type\":\"normal\",\"id\":\"83\"}],\"type\":\"normal\",\"id\":\"129\"}],\"type\":\"normal\",\"id\":\"140\"},{\"index\":{\"129\":[0]},\"first\":2,\"last\":2,\"children\":[{\"index\":{\"83\":[0]},\"first\":2,\"last\":2,\"children\":[{\"index\":{},\"first\":2,\"last\":2,\"children\":[],\"type\":\"normal\",\"id\":\"83\"}],\"type\":\"normal\",\"id\":\"129\"}],\"type\":\"normal\",\"id\":\"134\"},{\"index\":{\"124\":[0]},\"first\":3,\"last\":3,\"children\":[{\"index\":{\"125\":[0]},\"first\":3,\"last\":3,\"children\":[{\"index\":{},\"first\":3,\"last\":3,\"children\":[],\"type\":\"normal\",\"id\":\"125\"}],\"type\":\"normal\",\"id\":\"124\"}],\"type\":\"normal\",\"id\":\"123\"},{\"index\":{\"124\":[0]},\"first\":4,\"last\":4,\"children\":[{\"index\":{\"125\":[0]},\"first\":4,\"last\":4,\"children\":[{\"index\":{},\"first\":4,\"last\":4,\"children\":[],\"type\":\"normal\",\"id\":\"125\"}],\"type\":\"normal\",\"id\":\"124\"}],\"type\":\"normal\",\"id\":\"126\"}],\"type\":\"root\",\"id\":null},\"lookups\":[{\"123\":\"5785442\",\"134\":\"5698410\",\"148\":\"5698406\",\"140\":\"5698408\",\"126\":\"5788100\"},{\"124\":\"2014916932\",\"129\":\"2000000482\"},{\"83\":\"wdr14s9mh218mcp2im5oatyhx3rky4f3\",\"125\":\"h3lybctesoqtm6g5q84qrh3ol37n4263\"}]},\"offset\":{\"columns\":0,\"rows\":0},\"size\":{\"columns\":5,\"rows\":5}}}";
            var res1_5 = JsonConvert.DeserializeObject<ReportData>(sjson1_5);
            DecisionEngineProcess clTstF = new DecisionEngineProcess();
            clTstF.ConvertToDecisionEngineCutoff(res1_5);
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            TestOdataServises clTst2 = new TestOdataServises();
            clTst2.IncommingXMLTest();

            clTst2.Authorization();
            RootObject newWO;
           if (clTst2.GetWorkorders(out newWO))
            {
                //                DataTable dataTable = ConvertToDatatable(newWO.value);
                XmlSerializer xmlSerializer = new XmlSerializer(newWO.GetType());
                string s = JsonConvert.SerializeObject(newWO);
                int numItem = 1;
                foreach (Value elem in newWO.value)
                {
                    TreeNode node2 = new TreeNode(elem.Id.ToString());
                    TreeNode node3 = new TreeNode(elem.CallDate);
                    TreeNode[] array = new TreeNode[] { node2, node3 };
                    TreeNode treeNode = new TreeNode(string.Format("WO:{0}", numItem), array);
                    resultView.Nodes.Add(treeNode);
                    numItem++;
                }
            }

            //clTst2.ApiGetAuthorizationCode();

            Cursor.Current = Cursors.Default;
        }
        static DataTable ConvertToDatatable(List<Value> list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Number");
            dt.Columns.Add("CallDate");
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["ID"] = item.Id;
                row["Number"] = item.Number;
                row["CallDate"] = item.CallDate;

                dt.Rows.Add(row);
            }

            return dt;
        }

        private void GetAllWo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            TestOdataServises clTst2 = new TestOdataServises();
            clTst2.Authorization();
            int numPackage = 0;
            RootObject newWO;

            //to get all workorders
            while (clTst2.GetWorkorders(out newWO))
            {
                numPackage++;
                foreach (Value elem in newWO.value)
                {
                    if (WoFind.Text.CompareTo(elem.Id.ToString())==0)
                    {
                        resultView.Nodes.Add(string.Format("We have the WO:{0}", WoFind.Text));
                        Console.WriteLine("We have the WO");
                    }
                }
            }
            resultView.Nodes.Add(string.Format("NumberPackages:{0}", numPackage));

            Console.WriteLine(numPackage);

            Cursor.Current = Cursors.Default;

        }

        private void btnAdTest_Click(object sender, EventArgs e)
        {
            //            CodeTests testForSourceCode = new CodeTests();
            //            testForSourceCode.CheckRegex();

            Cursor.Current = Cursors.WaitCursor;
            TreeNode treeNode = new TreeNode(string.Format("START: {0}",DateTime.Now.ToString()));
            resultView.Nodes.Add(treeNode);
            TestOdataServises clTst2 = new TestOdataServises();
            clTst2.Authorization();
            for (int i = 25000; i < 30000; i++)
            {
                if(clTst2.CreateAsset(i)) Console.WriteLine(i);
//                System.Threading.Thread.Sleep(1000); //sleep time between sending Asset
            }
            TreeNode treeNode1 = new TreeNode(string.Format("END: {0}", DateTime.Now.ToString()));
            resultView.Nodes.Add(treeNode1);

            Cursor.Current = Cursors.Default;
        }
    }
}
