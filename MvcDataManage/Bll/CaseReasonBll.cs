using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;

namespace MvcDataManage.Bll
{
    public class CaseReasonBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

        //查询出案由的大罪名下拉框
        public List<ww_CaseReason> DropDownList()
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            List<ww_CaseReason> list = db.Query<ww_CaseReason>("select CaseReasonId ,CaseReasonName from ww_CaseReason where CaseReasonId_Paent=0 order by  CaseReasonId  ").ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }

        //查询出案由的小罪名下拉框
        public List<ww_CaseReason> DropDownListMingXi(int CaseReasonId)
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            List<ww_CaseReason> list = db.Query<ww_CaseReason>("select CaseReasonId ,LTRIM(CaseReasonName) as CaseReasonName from ww_CaseReason where CaseReasonId_Paent=@0 order by  CaseReasonId  ", CaseReasonId).ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
         
    }
}