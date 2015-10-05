using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using DefaultConnection;

namespace MvcDataManage.Bll
{
    public class UserBll
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");

       /// <summary>
        /// 验证用户名是否存在
       /// </summary>
       /// <returns></returns>
        public dynamic CheckUser(string userName)
        {
            db.BeginTransaction();
            List<ww_User> list = db.Query<ww_User>("select  * from ww_User where UserName=@0  and BoolDel=0", userName).ToList();
            
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
           return list;
        }
        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic AddUser(ww_User model)
        {
            model.BoolDel = false;          
            try
            {
                db.BeginTransaction();
                db.Insert(model);

                db.CompleteTransaction();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
            //List<ww_Re>  i= list.ToList();
            //return list;
        }
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public dynamic QueryAllUser()
        {
            IEnumerable<ww_User> list = db.Query<ww_User>("select  * from ww_User where BoolDel=0");
            return list;
        }
        /// <summary>
        /// 用id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic QueryById(int id)
        {
            ww_User list = db.Query<ww_User>("select  * from ww_User where BoolDel=0 and UserId=@0",id).FirstOrDefault();
            return list;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic UpdateUser(ww_User model)
        {
            model.BoolDel = false;
            try
            {
                db.BeginTransaction();
                db.Update(model);

                db.CompleteTransaction();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public dynamic CheckUser(string userName, string password)
        {
            db.BeginTransaction();
            List<ww_User> list = db.Query<ww_User>("select  * from ww_User where UserName=@0  and BoolDel=0 and Password=@1", userName.Trim(), password.Trim()).ToList();

            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic DelUser(int id)
        {
            try
            {
                db.BeginTransaction();
                db.Update<ww_User>(" set BoolDel=1 where UserId=@0",id);

                db.CompleteTransaction();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}