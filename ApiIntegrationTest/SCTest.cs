using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

//To install Json.NET, run the following command in the Package Manager Console 
//Install-Package Newtonsoft.Json -Version 9.0.1 
using Newtonsoft.Json;
//You can use - http://json2csharp.com/# to generate c# class for Deserialisation JSON object
//To get AuthBasic use Client ID:Client Secret in https://www.base64encode.org/


namespace ApiIntegrationTest
{
    public class TestOdataServises
    {
        private string _token;
        private string _urlWoNext;

        //URL Examples
        // https://sb2login.servicechannel.com/oauth/token
        // http://localhost/Authorization/oauth/token
        // http://sb2.servicechannel.com/api2/odata/workorders
        // http://dev1.servicechannel.com/api2/odata/workorders
        //test  ClientID: sc_internal_api_documentation
        //test ClentSecret: E6F9C034-32FF-4B74-A1D8-ECE8F5031352

        public void Authorization()
        {
            WebRequest request = WebRequest.Create(Properties.Settings.Default.ServerNameLogin);

            string data = string.Format("username={0}&password={1}&grant_type=password&impersonateUserInfo[username]=Impersonate TESTUser&impersonateUserInfo[Roles]=Power User",
                        Properties.Settings.Default.UserName, Properties.Settings.Default.Password);
            byte[] postBytes = Encoding.UTF8.GetBytes(data);

            request.Method = "POST";
            request.ContentLength = postBytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            var authInfo = Properties.Settings.Default.ClientId + ":" + Properties.Settings.Default.ClientSecret;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            //or use generated base64encode.org code
            //request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Basic {0}",Properties.Settings.Default.AuthBasic));

            // Get the request stream.
            Stream dataStreamP = request.GetRequestStream();
            // Write the data to the request stream.
            dataStreamP.Write(postBytes, 0, postBytes.Length);
            // Close the Stream object.
            dataStreamP.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string json = reader.ReadToEnd();
            Token newToken = JsonConvert.DeserializeObject<Token>(json);
            _token = newToken.access_token;

            for (int i = 0; i < response.Headers.Count; ++i)
                Console.WriteLine("\nHeader Name:{0}, Header value :{1}", response.Headers.Keys[i], response.Headers[i]);

            reader.Close();
            response.Close();
        }

        public bool GetWorkorders(out RootObject newWO)
        {
            string tempUrl = string.Format("http://{0}/api2/odata/workorders", Properties.Settings.Default.ServerNameApi);
            if (!string.IsNullOrEmpty(_urlWoNext)) tempUrl = _urlWoNext;

             WebRequest request = WebRequest.Create(tempUrl);
            request.Headers.Add(HttpRequestHeader.Authorization, string.Format(" Bearer {0}", _token));
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string json = reader.ReadToEnd();
            newWO = JsonConvert.DeserializeObject<RootObject>(json);

            response.Close();

            //get next Link
            string constNextData = "odata.nextLink\":\"";
            int numMessStart = json.LastIndexOf(constNextData);
            if (numMessStart == -1) return false;
            int numMessEnd = json.IndexOf("\r", numMessStart);
            if (numMessEnd == -1) return false;
            _urlWoNext = json.Substring(numMessStart + constNextData.Length, numMessEnd - numMessStart - constNextData.Length-1);

            if (string.IsNullOrEmpty(_urlWoNext)) return false;
            return true;
        }

        public bool CreateAsset(int numIter)
        {
            string tempUrl = string.Format("http://{0}/api2/assets", Properties.Settings.Default.ServerNameApi);
            WebRequest request = WebRequest.Create(tempUrl);

            request.Headers.Add(HttpRequestHeader.Authorization, string.Format(" Bearer {0}", _token));
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string sTag = "{\"Tag\": \"" + string.Format("test{0}\",", numIter);
                string json = sTag + "\"Manufacturer\": \"Sony\",\"StoreId\": \"007\",\"Trade\": \"TESTS\",\"Type\": \"test\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                httpResponse.Close();
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Authorization();
                    if (CreateAsset(numIter)) return true;
                }
                return false;
            }
            return true;
        }
        public void IncommingXMLTest()
        {
            WebRequest request = WebRequest.Create("http://dev1aspnet.servicechannel.com/xmlWS/service.asmx/XmlResponse");

            string storeID = "ScDemo";
            string customerID = "ScDemo";

            string TestXml = 
               string.Format(@"<?xml version=""1.0""?><DATA2SC PIN=""{1}"" ID=""942842206""><CALL DATETIME=""{3}"" CALLER=""PM XML Interface"" OPERATOR=""BulkPMDispatcher"" CATEGORY=""Maintenance"" SUB=""{0}"" LOC=""{2}"" TRADE=""{4}"" WO_NUM=""{5}"" PO_NUM=""{5}"" STATUS=""OPEN"" PRIORITY=""P7 (Routine &amp; PM)"" CURRENCY=""USD""><PROBLEM>Test</PROBLEM></CALL></DATA2SC>", 
            2000001305, "40487", "2310", "2016/12/20 11:03:39", "FLOOR MAINTENANCE-WOOD", "12942868");

            string TestXmlEnc = HttpUtility.UrlEncode(TestXml);
            string data = string.Format("StoreID={0}&CustomerID={1}&XMLMsg={2}",storeID, customerID, TestXmlEnc);
            byte[] postBytes = Encoding.UTF8.GetBytes(data);

            request.Method = "POST";
            request.ContentLength = postBytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            // Get the request stream.
            Stream dataStreamP = request.GetRequestStream();
            // Write the data to the request stream.
            dataStreamP.Write(postBytes, 0, postBytes.Length);
            // Close the Stream object.
            dataStreamP.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string json = reader.ReadToEnd();
            int recId = -1;
            if (json != null)
            {
                int.TryParse(json, out recId);
            }

        }

    }
}
