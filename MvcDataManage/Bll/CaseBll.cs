using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcDataManage.Models;

namespace MvcDataManage.Bll
{
    public class CaseBll
    {

        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

        /// <summary>
        /// 根据物品id，查询出案件相关信息
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic SelectByResID(int ResID)
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.CaseId, ww_Case.ResID, ww_Case.CabinetIId AS Expr1, ww_Case.CaseName, ww_Case.CaseNum, ww_Case.CabinetIId, ww_Case.CaseSum, ww_Cabinet.Num, ww_Case.CaseReasonId,  " +
                                                                         "ww_CaseReason.CaseReasonName FROM         ww_Cabinet INNER JOIN  ww_Case ON ww_Cabinet.CabinetIId = ww_Case.CabinetIId INNER JOIN  ww_CaseReason ON ww_Case.CaseReasonId = ww_CaseReason.CaseReasonId" +
                                                                         " where ww_Case.ResID=@0", ResID).ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 绑定修改的案件信息
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public dynamic ModifyByCaseId(int CaseId)
        {
            db.BeginTransaction();
            CaseJoinCabinet list =
                db.Query<CaseJoinCabinet>("  SELECT     ww_Case.*, ww_CaseReason.CaseReasonId_Paent FROM         ww_Case INNER JOIN " +
                                          " ww_CaseReason ON ww_Case.CaseReasonId = ww_CaseReason.CaseReasonId    " +
                                          "    where CaseId=@0", CaseId).FirstOrDefault();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 确定修改的案件信息
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public dynamic Modify(ww_Case model)
        {
            try
            {
                db.BeginTransaction();
                db.Update<ww_Case>("set CaseName=@0,CabinetIId=@1,CaseNum=@2,CaseReasonId=@3,CaseSum=@4 where CaseId=@5", model.CaseName, model.CabinetIId, model.CaseNum, model.CaseReasonId, model.CaseSum, model.CaseId);
                db.Update<ww_Re>("set CabinetIId=@0 where ResID =@1 and BoolTansfe=0 ", model.CabinetIId, model.ResID);
                db.CompleteTransaction();
                //List<ww_Re>  i= list.ToList();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }

    }
}