using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;

namespace MvcDataManage.Bll
{
    public class PhotoBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

        /// <summary>
        /// 根据物品id，查询出图片信息
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic SelectByResID(int ResID)
        {
            db.BeginTransaction();
            IEnumerable<ww_Photo> list = db.Query<ww_Photo>("select * from ww_Photo where ResID=@0 and BoolDel=0 ", ResID);
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 伪删除
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic Del(int Photoid)
        {
            db.BeginTransaction();
            db.Update<ww_Photo>("SET BoolDel=1  where PhotoId=@0", Photoid);
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return true;
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="ResID"></param>
        /// <returns></returns>
        public dynamic Upload(ww_Photo model)
        {

            //using (var scope = db.GetTransaction())
            //{
            //    // Do transacted updates here

            //    // Commit
            //    scope.Complete();
            //}
            db.BeginTransaction();
            db.Insert(model);
            db.CompleteTransaction();
            ////List<ww_Re>  i= list.ToList();
            return true;
        }
    }
}