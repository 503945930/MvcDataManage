using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDataManage.Models
{
    public class UserOperateModel
    {
        public int UserOperateId { get; set; }
        public int OperateId { get; set; }
        public string OperateName { get; set; }     
        public int UserId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Department { get; set; }
        public string ReallName { get; set; }
        public string Dtime { get; set; }
        public string IP { get; set; }
    }
}