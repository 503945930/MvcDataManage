using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;

namespace MvcDataManage.Bll
{
    public sealed class BllBuilder
    {
        #region 实例化物品类
        /// <summary>
        /// 实例化物品类
        /// </summary>
        /// <returns></returns>
        public static ResBll BuilRes()
        {
            return new ResBll();
        }
        #endregion
        #region 实例化案件类
        /// <summary>
        /// 实例化案件类
        /// </summary>
        /// <returns></returns>
        public static CaseBll BuilCase()
        {
            return new CaseBll();
        }
        #endregion
        #region 实例化犯罪嫌疑人类
        /// <summary>
        /// 实例化犯罪嫌疑人类
        /// </summary>
        /// <returns></returns>
        public static SuspectBll BuilSuspect()
        {
            return new SuspectBll();
        }
        #endregion
        #region 实例化图片类
        /// <summary>
        /// 实例化案件类
        /// </summary>
        /// <returns></returns>
        public static PhotoBll BuilPhoto()
        {
            return new PhotoBll();
        }
        #endregion
        #region 实例化柜子类
        /// <summary>
        /// 实例化柜子类
        /// </summary>
        /// <returns></returns>
        public static CabinetBll BuilCabinet()
        {
            return new CabinetBll();
        }
        #endregion
        #region 实例化移交类
        /// <summary>
        /// 实例化移交类
        /// </summary>
        /// <returns></returns>
        public static TransfeBll BuilTransfe()
        {
            return new TransfeBll();
        }
        #endregion
        #region 实例化用户类
        /// <summary>
        /// 实例化用户类
        /// </summary>
        /// <returns></returns>
        public static UserBll BuilUser()
        {
            return new UserBll();
        }
        #endregion
        #region 实例化日志类
        /// <summary>
        /// 实例化用户类
        /// </summary>
        /// <returns></returns>
        public static LogBll BuilLog()
        {
            return new LogBll();
        }
        #endregion
        #region 实例化案由
        /// <summary>
        /// 实例化案由
        /// </summary>
        /// <returns></returns>
        public static CaseReasonBll BuilCaseReasonBll()
        {
            return new CaseReasonBll();
        }
        #endregion
    }
}