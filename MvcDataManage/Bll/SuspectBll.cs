using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcDataManage.Models;

namespace MvcDataManage.Bll
{
    public class SuspectBll
    {

        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

        /// <summary>
        /// 根据物品id，查询出犯罪嫌疑人相关信息
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic SelectByResID(int ResID)
        {
            db.BeginTransaction();
            IEnumerable<ww_Suspect> list = db.Query<ww_Suspect>("select * from ww_Suspect where ResID=@0 ", ResID);
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 根据id，查询出犯罪嫌疑人相关信息
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic SelectBySuspectId(int SuspectId)
        {
            db.BeginTransaction();
            ww_Suspect list = db.Query<ww_Suspect>("select * from ww_Suspect where SuspectId=@0 ", SuspectId).FirstOrDefault();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 确定修改的信息
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public dynamic Modify(ww_Suspect model)
        {
            try
            {
                db.BeginTransaction();
                db.Update<ww_Suspect>(
                    "set SuspectName=@0,SuspectSex=@1,SuspectAge=@2,SuspectAdress=@3,SuspectIdCard=@4 where SuspectId=@5",
                    model.SuspectName, model.SuspectSex, model.SuspectAge, model.SuspectAdress, model.SuspectIdCard,
                    model.SuspectId);

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