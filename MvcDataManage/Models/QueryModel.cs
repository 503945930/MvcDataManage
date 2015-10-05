using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDataManage.Models
{
    public class QueryModel
    {
        public int ResID { get; set; }
        public bool BoolTansfe { get; set; }
        public string ResNmae { get; set; }
        public string SuspectName { get; set; }
        public string CaseName { get; set; }
        public string CaseNum { get; set; }
        public string Num { get; set; }
        public string Tansfe { get; set; }
    }
}