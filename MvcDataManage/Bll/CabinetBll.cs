using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using DefaultConnection;
using MvcDataManage.Models;

namespace MvcDataManage.Bll
{
    public class CabinetBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

        /// <summary>
        /// 查询出空柜子，
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic DropDownList()
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            //IEnumerable<ww_Cabinet> list = db.Query<ww_Cabinet>("select CabinetIId ,Num from ww_Cabinet where  CabinetIId not in (select CabinetIId from ww_Res where BoolTansfe=0) ");
            IEnumerable<ww_Cabinet> list = db.Query<ww_Cabinet>("select CabinetIId ,Num from ww_Cabinet  ");
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        ///根据id, 查询出柜子，
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic SelectByCabinetIId(int CabinetIId)
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            IEnumerable<ww_Cabinet> list = db.Query<ww_Cabinet>("select CabinetIId ,Num from ww_Cabinet where  CabinetIId =@0 ", CabinetIId);
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
    }
}