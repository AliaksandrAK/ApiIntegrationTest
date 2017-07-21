﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//it generated using http://json2csharp.com/#
namespace ApiIntegrationTest
{
    internal class WorkOrder
    {
    }

    public class Status
    {
        public string Primary { get; set; }
        public string Extended { get; set; }
    }

    public class Count
    {
        public int Total { get; set; }
        public int Own { get; set; }
    }

    public class Attachments
    {
        public Count Count { get; set; }
    }

    public class Last
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string NoteData { get; set; }
        public string DateCreated { get; set; }
        public bool ActionRequired { get; set; }
        public string MailedTo { get; set; }
        public string CreatedBy { get; set; }
    }

    public class Count2
    {
        public int Total { get; set; }
        public int Own { get; set; }
    }

    public class Notes
    {
        public Last Last { get; set; }
        public Count2 Count { get; set; }
    }

    public class Value
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string PurchaseNumber { get; set; }
        public Status Status { get; set; }
        public string Caller { get; set; }
        public string CreatedBy { get; set; }
        public string CallDate { get; set; }
        public string Priority { get; set; }
        public string Trade { get; set; }
        public string ApprovalCode { get; set; }
        public string ScheduledDate { get; set; }
        public string CompletedDate { get; set; }
        public string ExpirationDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public object Nte { get; set; }
        public object Tax { get; set; }
        public object Tax2 { get; set; }
        public object Price { get; set; }
        public string ProblemCode { get; set; }
        public string Resolution { get; set; }
        public Attachments Attachments { get; set; }
        public Notes Notes { get; set; }
        public int PostedId { get; set; }
        public bool IsCheckInDenied { get; set; }
        public object CheckInDeniedReason { get; set; }
        public int CheckInRange { get; set; }
        public object RecallWorkOrder { get; set; }
        public bool IsExpired { get; set; }
        public bool IsEnabledForMobile { get; set; }
    }
    //Generated by http://json2csharp.com/ from Response Body of http://localhost/api2/swagger/ui/index#!/Workorders/Workorders
    public class RootObject
    {
        public string context { get; set; }
        public List<Value> value { get; set; }
        public string nextLink { get; set; }
    }

//    HTTP/1.1 200 OK
//      <Content-Type: application/json
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }

    }
    public class XMLResponce
    {
        public string gt { get; set; }
        public string lt { get; set; }
    }



    /////
    public class ReportData
    {
        public Xtab_Data xtab_data { get; set; }

        public Taskstate taskState { get; set; }
    }

    public class Xtab_Data
    {
        public Columns columns { get; set; }
        public List<List<string>> data { get; set; }
        public Overall_Size overall_size { get; set; }
        public Rows rows { get; set; }
        public Offset offset { get; set; }
        public Size size { get; set; }
    }

    public class Columns
    {
        public ReportResultTree tree { get; set; }
        public Lookup[] lookups { get; set; }
    }

    public class ReportResultTree
    {
        public Index index { get; set; }
        public int first { get; set; }
        public int last { get; set; }
        public Child[] children { get; set; }
        public string type { get; set; }
        public object id { get; set; }
    }

    public class Index
    {
        public Child[] children { get; set; }

    }

    public class Child
    {
        public Index index { get; set; }
        public int first { get; set; }
        public int last { get; set; }
        public Child[] children { get; set; }
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Index1
    {
    }

    public class Element
    {
        public string[] id { get; set; }
    }
    public class Lookup
    {
        public List<Element> lookups { get; set; }
    }
    public class lookups1
    {
        public string[] lookups { get; set; }
    }

    public class Overall_Size
    {
        public int columns { get; set; }
        public int rows { get; set; }
    }

    public class Rows
    {
        public ReportResultTree tree { get; set; }
        public List<Dictionary<string, string>> lookups { get; set; }
    }

    public class Offset
    {
        public int columns { get; set; }
        public int rows { get; set; }
    }

    public class Size
    {
        public int columns { get; set; }
        public int rows { get; set; }
    }

    public class Taskstate
    {
        public string msg { get; set; }
        public string status { get; set; }
    }
}