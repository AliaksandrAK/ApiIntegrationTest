using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ApiIntegrationTest
{
    class ParserRegex
    {
        class SerTT
        {
            public string SerTTTest { get; set; }
        }
        class SerializeTest : SerTT
        {
            [DataMember(Order = 1)]
            public string Test { get; set; }

            [ScriptIgnore]
            public DateTime Date { get; set; }
            [DataMember(Order = 0)]
            public string CreatedDate { get; set; }

            public Dictionary<string, object> ToDictionary()
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                PropertyInfo[] infos = GetType().GetProperties();
                foreach (PropertyInfo info in infos)
                {
                    result.Add(info.Name, info.GetValue(this, null));
                }
                result.Remove("Date");
                result.Remove("CreatedDate");
                result.Add("CreatedDate", Date.ToString("yyyyMMdd"));

                return result;
            }

        }

        public static void JsonSerializeTest()
        {
//            string sjson1 =
//                "\"lookups\":[{\"55\":\"5654738\"},{\"49\":\"2000051259\"},{\"50\":\"z76qc3nyec2bmosyrjnek02ywuqt2eu6\"}]";

            string sjson =
    "{\"xtab_data\":{\"columns\":{\"tree\":{\"index\":{\"4\":[4],\"1\":[1],\"3\":[3],\"0\":[0],\"2\":[2]},\"first\":0,\"last\":4,\"children\":[{\"index\":{},\"first\":0,\"last\":0,\"children\":[],\"type\":\"metric\",\"id\":\"0\"},{\"index\":{},\"first\":1,\"last\":1,\"children\":[],\"type\":\"metric\",\"id\":\"1\"},{\"index\":{},\"first\":2,\"last\":2,\"children\":[],\"type\":\"metric\",\"id\":\"2\"},{\"index\":{},\"first\":3,\"last\":3,\"children\":[],\"type\":\"metric\",\"id\":\"3\"},{\"index\":{},\"first\":4,\"last\":4,\"children\":[],\"type\":\"metric\",\"id\":\"4\"}],\"type\":\"root\",\"id\":null},\"lookups\":[{\"4\":\"cutoff_reject2\",\"1\":\"cutoff_approve1\",\"3\":\"cutoff_reject1\",\"0\":\"recommendation\",\"2\":\"cutoff_approve2\"}]},\"data\":[[\"0.4\",\"0.94\",\"0.47\",\"0.07\",\"0.02\"]],\"overall_size\":{\"columns\":5,\"rows\":1},\"rows\":{\"tree\":{\"index\":{\"55\":[0]},\"first\":0,\"last\":0,\"children\":[{\"index\":{\"49\":[0]},\"first\":0,\"last\":0,\"children\":[{\"index\":{\"50\":[0]},\"first\":0,\"last\":0,\"children\":[{\"index\":{},\"first\":0,\"last\":0,\"children\":[],\"type\":\"normal\",\"id\":\"50\"}],\"type\":\"normal\",\"id\":\"49\"}],\"type\":\"normal\",\"id\":\"55\"}],\"type\":\"root\",\"id\":null},\"lookups\":[{\"55\":\"5654738\"},{\"49\":\"2000051259\"},{\"50\":\"z76qc3nyec2bmosyrjnek02ywuqt2eu6\"}]},\"offset\":{\"columns\":0,\"rows\":0},\"size\":{\"columns\":5,\"rows\":1}}}";
            var res = JsonConvert.DeserializeObject<ReportData>(sjson);
            //            var res1 = JsonConvert.DeserializeObject<lookups1>(sjson1);


            string sjson2 =
                @"{""key1"":""value1"",""key2"":""value2""}";

            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(sjson2);

//            string sjson5 =
//                "[{key: \"a\"}, {key: \"b\"}]";
            string sjson6 =
                "[{\"55\":\"5654738\"},{\"49\":\"2000051259\"},{\"50\":\"z76qc3nyec2bmosyrjnek02ywuqt2eu6\"}]";
            var tt = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(sjson6);

            DateTime dt = DateTime.Now;
            string json = JsonConvert.SerializeObject(dt, new JsonSerializerSettings { DateFormatString = "yyyyMMdd", Formatting = Formatting.Indented });
            string test = "\"Discretionary\":\"NULL\",\"OriginalCurrency\":\"USD\",\"CreatedDate\":\" Date(1498455838390)\",\"NegativeFeedbackPercent\":1.43";

            string newDt = dt.ToString("yyyyMMdd") + "\"";
            string oldDt = GetSubStringBetween(test, "\"CreatedDate\":\"", ",");
            test = test.Replace(oldDt, newDt);

            string ss = JsonConvert.SerializeObject(dt);

            SerializeTest testCt = new SerializeTest();
            testCt.Test = "12345";
            testCt.Date = DateTime.Now;
            testCt.CreatedDate = testCt.Date.ToString("yyyyMMdd");

            var jsonNew = new JavaScriptSerializer().Serialize(testCt);
            var jsonNewDic = new JavaScriptSerializer().Serialize(testCt.ToDictionary());

            StringBuilder stb = new StringBuilder();
            var jsSer = new JavaScriptSerializer();
            jsSer.Serialize(testCt, stb);
            string sts = stb.ToString();


            //Get string name of the variable
            var f = typeof(SerializeTest);
            var ggg = f.Name;
            MemberInfo[] a = typeof(SerializeTest).GetMembers();
            foreach (MemberInfo an in a)
            {
                string dfdf = an.Name;
            }

            MemberInfo[] myMemberInfo;
            Type myType = testCt.GetType();
            myMemberInfo = myType.GetMembers();
            for (int i = 0; i < myMemberInfo.Length; i++)
            {
                if (myMemberInfo[i].MemberType == MemberTypes.Property)
                {
                    string sn = myMemberInfo[i].Name;
                }
            }


            MemberInfo[] memberInfo = f.GetMembers();

            //variable name as string using Expression
            Expression<Func<object>> expression = () => testCt.CreatedDate;
            string name = (expression.Body as MemberExpression).Member.Name;

            //check email address
            string regExString = @"^[a-z0-9][a-z_0-9\-\.'&]+@([a-z0-9\-]*\.)+[a-z][a-z]+$";
            string sample = "abv&@sv.com";
            var match = Regex.Match(sample, regExString);
            if (match.Success)
            {
            }
            Regex rgx = new Regex(regExString);
            //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");
            bool bm = rgx.IsMatch(sample);
        }
        public void CheckRegex()
        {
            string urlToysRUsUSA =
                "javascript:var nw = window.open('http://login.uxassembly.com/trubru/login.aspx?u={0}&amp;p={0}', '_blank');";

            string urlKohls =
                "javascript:var nw = window.open('http://kohls.rcsretail.com?StoreId=KOHLS{0}&amp;', '_blank');";

            //setupId="346"
            string urlCashAmerica =
                "javascript:var nw = window.open('http://cls.com/woe/autologin/fwd.asp?svc=fba7f200-75cf-11dc-8879-0019B9159D30&amp;company=casham&amp;store={0}&amp;tn=1&amp;owner=cashamerica346', '_blank');";

            string OLDStoreId = "555";
            string NEWStoreId = "Store12345";
            urlToysRUsUSA = String.Format(urlToysRUsUSA, OLDStoreId);

            string oldStoreA = GetSubStringBetween(urlToysRUsUSA, null, "&");
            string oldStoreB = GetSubStringBetween(urlToysRUsUSA, "", "&");
            string oldStoreC = GetSubStringBetween(urlToysRUsUSA, null);

            if (urlToysRUsUSA.Contains("u="))
            {
                string oldStore = GetSubStringBetween(urlToysRUsUSA, "u=", "&");
                if (!String.IsNullOrEmpty(oldStore)) urlToysRUsUSA = urlToysRUsUSA.Replace(oldStore, NEWStoreId);
            }
            if (urlKohls.Contains("StoreId=KOHLS"))
            {
                string oldStore = GetSubStringBetween(urlKohls, "StoreId=KOHLS", "&");
                if (!String.IsNullOrEmpty(oldStore)) urlKohls = urlKohls.Replace(oldStore, NEWStoreId);
            }
            if (urlCashAmerica.Contains("store="))
            {
                string oldStore = GetSubStringBetween(urlCashAmerica, "store=", "&");
                if (!String.IsNullOrEmpty(oldStore)) urlCashAmerica = urlCashAmerica.Replace(oldStore, NEWStoreId);
            }

            string regExString = @"u=\w+&";
            var match = Regex.Match(urlToysRUsUSA, regExString);
            if (match.Success)
            {
                var group = match.Groups;
                if (group.Count > 1)
                {
                    string oldStore = group[1].Value;
                }
            }

            if (urlToysRUsUSA.Contains("p="))
            {
                Regex regex = new Regex(@"\d+");
                MatchCollection matchesw = regex.Matches(urlToysRUsUSA);
                if (matchesw.Count != 0)
                {
                    string oldStore = matchesw[0].Value;
                    if (!String.IsNullOrEmpty(oldStore)) urlToysRUsUSA = urlToysRUsUSA.Replace(oldStore, NEWStoreId);
                }
            }
        }


        public static string GetSubStringBetween(string content, string startString, string endString = null)
        {
            int start = 0, end = 0;
            if (String.IsNullOrEmpty(content)) return String.Empty;

            if (String.IsNullOrEmpty(startString) && String.IsNullOrEmpty(endString))
            {
                return content;
            }
            if (String.IsNullOrEmpty(startString) && !String.IsNullOrEmpty(endString) && content.Contains(endString))//Get string from begin
            {
                end = content.IndexOf(endString, start, StringComparison.InvariantCulture);
                return content.Substring(start, end - start);
            }
            else if (!String.IsNullOrEmpty(startString) && content.Contains(startString) && String.IsNullOrEmpty(endString)) //Get string to end string
            {
                start = content.IndexOf(startString, 0, StringComparison.InvariantCulture) + startString.Length;
                if (start == -1) return String.Empty;
                return content.Substring(start, content.Length - start);
            }
            else if (!String.IsNullOrEmpty(startString) && !String.IsNullOrEmpty(endString) && content.Contains(startString) && content.Contains(endString))
            {
                start = content.IndexOf(startString, 0, StringComparison.InvariantCulture) + startString.Length;
                end = content.IndexOf(endString, start, StringComparison.InvariantCulture);
                if (start == -1 || end == -1) return String.Empty;
                return content.Substring(start, end - start);
            }
            else
                return String.Empty;
        }
    }
}
