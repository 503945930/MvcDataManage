using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;

namespace MvcDataManage.Bll
{
    public class TransfeBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 添加，并设置为转移
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Add(ww_Transfe model)
        {
            db.BeginTransaction();
            db.Insert(model);
            db.Update<ww_Re>("set BoolTansfe=1 where ResID=@0", model.ResID);
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return true;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        public dynamic QueryById(int resId)
        {
            try
            {
             List<ww_Transfe> list=   db.Query<ww_Transfe>("SELECT  ww_Transfe.*  FROM   ww_Transfe where ResID=@0", resId).ToList();
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}