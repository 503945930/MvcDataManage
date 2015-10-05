using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcDataManage.Models;
using PetaPoco;

namespace MvcDataManage.Bll
{
    public class LogBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public dynamic SelectLog(int page, int take)
        {
            Page<UserOperateModel> m = db.Page<UserOperateModel>(page, take,
                "SELECT     ww_Role.RoleName, ww_User.UserId, ww_User.UserName," +
                " ww_User.Department, ww_UserOperate.Dtime, ww_UserOperate.OperateId ,ww_UserOperate.IP , ww_Operate.OperateName " +
        " FROM      ww_UserOperate INNER JOIN " +
                " ww_User ON ww_UserOperate.UserId = ww_User.UserId INNER JOIN " +
                "  ww_Role ON ww_User.RoleId = ww_Role.RoleId INNER JOIN  ww_Operate ON ww_UserOperate.OperateId = ww_Operate.OperateId ORDER BY ww_UserOperate.UserOperateId DESC ");
            //List<QueryModel> m = db.Query<QueryModel>("   SELECT     ww_Case.CaseName, ww_Res.ResNmae, ww_Cabinet.Num, ww_Res.BoolTansfe, ww_Case.CaseNum " +
            //    " FROM    ww_Case INNER JOIN ww_Res ON ww_Case.ResID = ww_Res.ResID INNER JOIN " +
            //    "  ww_Cabinet ON ww_Res.CabinetIId = ww_Cabinet.CabinetIId ").ToList();
            return m;
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic AddLog(ww_UserOperate model)
        {
            try
            {

                db.BeginTransaction();
                db.Insert(model);
                db.CompleteTransaction();

                return true;

            }
            catch (Exception)
            {

                db.AbortTransaction();
                return false;
            }
            
        }
    }

}