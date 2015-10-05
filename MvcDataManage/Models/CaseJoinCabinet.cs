using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDataManage.Models
{
    public class CaseJoinCabinet
    {
        
        public string Num { get; set; }
      
        public int CaseId { get; set; }


        public int? ResID { get; set; }



        public int CabinetIId { get; set; }





        public string CaseName { get; set; }



        public int CaseReasonId { get; set; }
        public int CaseReasonId_Paent { get; set; }
        
        public string CaseNum { get; set; }

        public string CaseReasonName { get; set; }





       
        public string CaseSum { get; set; }
    }
}