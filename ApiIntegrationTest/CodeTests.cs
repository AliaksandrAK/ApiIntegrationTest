using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ApiIntegrationTest
{

    class CodeTests
    {

        public void CheckXMLIntegration()
        {
            string woNumber = "WO_number1";
            DateTime dtCreated = DateTime.Now;


            string TestXml =
               string.Format(@"<?xml version=""1.0""?><DATA2SC PIN=""{1}"" ID=""942842206""><CALL DATETIME=""{3}"" CALLER=""PM XML Interface"" OPERATOR=""BulkPMDispatcher"" CATEGORY=""Maintenance"" SUB=""{0}"" LOC=""{2}"" TRADE=""{4}"" WO_NUM=""{5}"" PO_NUM=""{5}"" STATUS=""OPEN"" PRIORITY=""P7 (Routine &amp; PM)"" CURRENCY=""USD""><PROBLEM>Test</PROBLEM></CALL></DATA2SC>",
            2000001305, "40487", "007", DateTime2String(dtCreated), "TESTS", woNumber);

            string reqFormat;
            string addMsg = "";

            string sResult;
            var trNum = GetTrNumber(TestXml, out reqFormat);
            if (trNum != null && trNum != -1) addMsg = string.Format(" TR_NUM = {0},", trNum.Value);
            sResult = string.Format("Send {0} {1} with GET success: {2}, url = {3},{4} MessageId = {5}, res = {6}",
                        "", reqFormat, "", "", addMsg, "", "");
        }

        public string DateTime2String(DateTime? dt)
        {
            return (dt != null) ? dt.Value.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) : null;
        }
        private static string GetResultStringFromResponse(string text)
        {
            return GetRegexGroupValue(@"string&gt;(?<{0}>.*)&lt;/string&gt;", text);
        }
        static string GetRegexGroupValue(string regExPatternTemplate, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                const string groupName = "resString";
                var reg = new Regex(string.Format(regExPatternTemplate, groupName), RegexOptions.Multiline | RegexOptions.CultureInvariant);
                if (reg.IsMatch(text))
                {
                    var match = reg.Match(text);
                    text = match.Groups[groupName].Success ? match.Groups[groupName].Value : string.Empty;
                }
            }

            return text;
        }
        private static string GetReguestFormat(string request)
        {
            string reqFormat = "JSON";
            if (request.Contains("cXML SYSTEM")) reqFormat = "cXML";
            else if (request.Contains("DATA2SC")) reqFormat = "XML";
            return reqFormat;
        }
        private static int? GetTrNumber(string request, out string requestFormat)
        {
            requestFormat = GetReguestFormat(request);

            string regExString = @"WO_NUM=""(\d+)"" ";
            if (requestFormat == "cXML") regExString = @"orderID=""(\d+)"" ";
            else if (requestFormat == "JSON") regExString = @"""TrNum"":(\d+) ";

            var trNum = -1;

            try
            {
                var match = Regex.Match(request, regExString);
                if (match.Success)
                {
                    var group = match.Groups;
                    if (group.Count > 1) int.TryParse(group[1].Value, out trNum);
                }
            }
            catch (Exception)
            {
                requestFormat = "";
                return null;
            }

            return trNum;
        }
    }
}
