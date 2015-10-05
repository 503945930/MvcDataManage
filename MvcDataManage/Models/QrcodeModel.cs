using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDataManage.Models
{
    public class QrcodeModel
    {
        public string ResNmae { get; set; }
        public string SuspectName { get; set; }
        public string SuspectAge { get; set; }
        public string SuspectSex { get; set; }
        public string SuspectAdress { get; set; }
        public string SuspectIdCard { get; set; }    
        public string CaseName { get; set; }
        public string CaseNum { get; set; }
        public string CaseReasonName { get; set; }
        public string Num { get; set; }
        public bool BoolTansfe { get; set; }
    }
}