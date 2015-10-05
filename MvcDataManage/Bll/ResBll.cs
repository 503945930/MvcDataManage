using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DefaultConnection;
using MvcDataManage.Models;
using PetaPoco;


namespace MvcDataManage.Bll
{
  
    public class ResBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

        /// <summary>
        /// 根据柜子的编号查询出物品id，和物品名称
        /// </summary>
        /// <param name="cabinetName"></param>
        /// <returns></returns>
        public dynamic Select(string cabinetName)
        {
            db.BeginTransaction();
            IEnumerable<ww_Re> list = db.Query<ww_Re>("select ResID,ResNmae from ww_Res where CabinetIId in( select CabinetIId from ww_Cabinet where Num='" + cabinetName + "'  ) and BoolTansfe=0");
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 根据物品id,找到物品信息
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic SelectByResID(int ResID)
        {
            db.BeginTransaction();
            List<ww_Re> list = db.Query<ww_Re>("select * from ww_Res where ResID=@0 ", ResID).ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 确定修改的信息
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public dynamic Modify(ww_Re model)
        {
            try
            {
                db.BeginTransaction();
                db.Update<ww_Re>("set ResNmae=@0,ResMess=@1 where ResID=@2", model.ResNmae, model.ResMess, model.ResID);

                db.CompleteTransaction();
                //List<ww_Re>  i= list.ToList();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public dynamic DataAdd(ww_Re modelRe, string Num, ww_Case modelCase, ww_Suspect modelSuspect)
        {
            
          
            //int i =(int) db.Insert(mRe);
            try
            {
               
                db.BeginTransaction();

                ww_Cabinet myCabinet =
                  db.FirstOrDefault<ww_Cabinet>("select CabinetIId from ww_Cabinet where Num='" + Num +
                                                "' ");           

                modelRe.BoolTansfe = false;
                modelRe.CabinetIId = myCabinet.CabinetIId;
               int resid=(int) db.Insert(modelRe);

              
               modelCase.CabinetIId = myCabinet.CabinetIId;
                modelCase.ResID = resid;
                db.Insert(modelCase);


                modelSuspect.ResID = resid;
                db.Insert(modelSuspect);


                //db.Update<ww_Re>("set ResNmae=11,ResMess=22 where ResID=10");
                db.CompleteTransaction();
                return resid;
            }
            catch (Exception)
            {

                db.AbortTransaction();
                return false;
            }
           
           
            //List<ww_Re>  i= list.ToList();
           
        }

        public dynamic QueryByMuti(string queryCondition)
        {
            try
            {
                IEnumerable<QueryModel> list = db.Query<QueryModel>("SELECT    ww_Res.ResID,ww_Res.BoolTansfe,   ww_Res.ResNmae, ww_Suspect.SuspectName, ww_Case.CaseName  " +
                                                                    "FROM     ww_Case INNER JOIN ww_Res ON ww_Case.ResID = ww_Res.ResID INNER JOIN ww_Suspect ON ww_Res.ResID = ww_Suspect.ResID " +
                                                                   "where    ww_Suspect.SuspectName like '%" + queryCondition + "%' or     ww_Res.ResNmae like  '%" + queryCondition + "%'       or    ww_Case.CaseName like  '%" + queryCondition + "%' ");
                return list;
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 查询出已经放了东西的柜子
        /// </summary>
        /// <returns></returns>
        public dynamic SelectNotNullResID()
        {
            db.BeginTransaction();
            List<ww_Re> list = db.Query<ww_Re>("select CabinetIId from ww_Res where BoolTansfe=0 ").ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 查询出所有物证数量
        /// </summary>
        /// <returns></returns>
        public dynamic ReCont()
        {
            db.BeginTransaction();
           int count = db.Query<ww_Re>("select ResID   from ww_Res ").ToList().Count;
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return count;
        }
        /// <summary>
        /// 查询出未物证数量
        /// </summary>
        /// <returns></returns>
        public dynamic ReWeiCont()
        {
            db.BeginTransaction();
            int count = db.Query<ww_Re>("select ResID   from ww_Res where BoolTansfe=0  ").ToList().Count;
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return count;
        }
        /// <summary>
        /// 二维码生成 的数据
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic QrCode(int ResID)
        {
            db.BeginTransaction();
            QrcodeModel list = db.Query<QrcodeModel>("SELECT     ww_Suspect.SuspectName, ww_Suspect.SuspectAge, ww_Suspect.SuspectSex, ww_Suspect.SuspectAdress, ww_Suspect.SuspectIdCard, ww_Res.ResNmae, ww_Case.CaseName,  " +
                                               " ww_Case.CaseNum, ww_Cabinet.Num, ww_Case.CaseReasonId, ww_CaseReason.CaseReasonName  " +
                                               " FROM         ww_Case INNER JOIN  ww_Res ON ww_Case.ResID = ww_Res.ResID INNER JOIN  ww_Suspect ON ww_Res.ResID = ww_Suspect.ResID INNER JOIN " +
                                               "  ww_Cabinet ON ww_Case.CabinetIId = ww_Cabinet.CabinetIId INNER JOIN " +
                                               "ww_CaseReason ON ww_Case.CaseReasonId = ww_CaseReason.CaseReasonId   where ww_Res.ResID=@0 ", ResID).FirstOrDefault();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        public dynamic Pagination(int page , int take )
        {
            Page<QueryModel> m = db.Page<QueryModel>(page, take,
                "SELECT     ww_Case.CaseName, ww_Res.ResNmae, ww_Cabinet.Num, case ww_Res.BoolTansfe when " +
  " 0 then '未移交' else '已移交' end as Tansfe, ww_Case.CaseNum " +
                " FROM    ww_Case INNER JOIN ww_Res ON ww_Case.ResID = ww_Res.ResID INNER JOIN " +
                "  ww_Cabinet ON ww_Res.CabinetIId = ww_Cabinet.CabinetIId ORDER BY ww_Case.CaseName DESC ");
            //List<QueryModel> m = db.Query<QueryModel>("   SELECT     ww_Case.CaseName, ww_Res.ResNmae, ww_Cabinet.Num, ww_Res.BoolTansfe, ww_Case.CaseNum " +
            //    " FROM    ww_Case INNER JOIN ww_Res ON ww_Case.ResID = ww_Res.ResID INNER JOIN " +
            //    "  ww_Cabinet ON ww_Res.CabinetIId = ww_Cabinet.CabinetIId ").ToList();
            return m;
        }
    }
}