using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using DefaultConnection;
using MvcDataManage.Bll;
using MvcDataManage.Filter;
using MvcDataManage.Models;
using MvcQRcode.BLL;

namespace MvcDataManage.Controllers
{
    [AuthorizeEx]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Userid =(int)Session["User"];
            ViewBag.RoseId = (int)Session["RoseId"];
            ViewBag.ReCont = BllBuilder.BuilRes().ReCont();//所有物证数量
            ViewBag.ReWeiCont = BllBuilder.BuilRes().ReWeiCont();//所有未移交的数据
            ViewBag.ReYiCont = ViewBag.ReCont - ViewBag.ReWeiCont;//所有已移交的数据
           //ViewBag.NotNull = Json(BllBuilder.BuilRes().SelectNotNullResID());//把放了东西的柜子
            return View();
        }
        /// <summary>
        /// 当前柜子是否为空
        /// </summary>
        /// <param name="cabinet"></param>
        /// <returns></returns>
        
        public JsonResult IsNotNull()
        {
            var list = BllBuilder.BuilRes().SelectNotNullResID();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 打开柜子
        /// </summary>
        /// <param name="cabinet"></param>
        /// <returns></returns>
       
        public JsonResult OpenCab(string cabinet)
        {
          var list=  BllBuilder.BuilRes().Select(cabinet);

          return Json(list,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 资料搜索
        /// </summary>
        /// <returns></returns>
       [LoginAction(LogMessage = 8)]
        public ActionResult DataSearch(string queryCondition)
        {
            ViewBag.Userid =(int)Session["User"];
            ViewBag.RoseId = (int) Session["RoseId"];
           
            if (queryCondition.IsEmpty() )
            {
                ViewBag.QueryModellist = new List<QueryModel>();
                ViewBag.Select = "";
                return View();

            }
           
           IEnumerable<QueryModel> list=   BllBuilder.BuilRes().QueryByMuti(queryCondition);
            ViewBag.QueryModellist = list;
            ViewBag.Select = queryCondition;
           return View();

        }
        /// <summary>
        /// 根据物品id打开相应资料
        /// </summary>
        /// <returns></returns>
       [LoginAction(LogMessage = 7)]
        public ActionResult Data(int id)
        {
            ViewBag.CaseList = BllBuilder.BuilCase().SelectByResID(id);//案件信息
            ViewBag.SuspectList = BllBuilder.BuilSuspect().SelectByResID(id);//犯罪嫌疑人信息
            ViewBag.ResList = BllBuilder.BuilRes().SelectByResID(id);//涉案物证信息
            ViewBag.PhotoList = BllBuilder.BuilPhoto().SelectByResID(id);//图片信息
            List<ww_Transfe> list = BllBuilder.BuilTransfe().QueryById(id);
            if (list.Count==0)
            {
                ww_Transfe myTransfe=new ww_Transfe()
                {
                    TransfeUnit="",
                    TransfeDepartment = "",
                    TransfePeople= "",
                    TransfeTime =DateTime.Now.ToLocalTime(),
                    ReciveUnit = "",
                    ReciveDepartment = "",
                    RecivePeople = "",
                    ReciveTime =DateTime.Now.ToLocalTime() ,

                };
                List<ww_Transfe> myList = new List<ww_Transfe>();
                myList.Add(myTransfe);
                ViewBag.TransfeList = myList;

            }
            else
            {
                 ViewBag.TransfeList = list;//
            }


            return View();
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetImg(int id)
        {
            using (var ms = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(190, 160);
                QrcodeModel qrcode = BllBuilder.BuilRes().QrCode(id);
                string stringtest =qrcode.ResNmae.Trim()+"      "+ qrcode.SuspectName.Trim() + " " + qrcode.SuspectSex.Trim() + " "  + " " + qrcode.SuspectAdress.Trim() + " " +
                    qrcode.SuspectIdCard.Trim() + "     " + qrcode.CaseName.Trim() + " " + qrcode.CaseReasonName.Trim() + " " + qrcode.CaseNum.Trim() + " " + qrcode.Num.Trim();//内容 
                QRcodeHelper.GetQRCode(stringtest, ms);
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return File(ms.ToArray(), "image/Jpeg");
                //  Response.ContentType = "image/Png";
                // Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
                // Response.End();
            }

            //Graphics g = Graphics.FromImage(bmp);
            //g.Clear(Color.White);
            //g.FillRectangle(Brushes.Red, 2, 2, 65, 31);
            //g.DrawString("学习MVC", new Font("黑体", 15f), Brushes.Yellow, new PointF(5f, 5f));
            //MemoryStream ms = new MemoryStream();
          
            //g.Dispose();
            //bmp.Dispose();
           
        }
        /// <summary>
        /// 查看相应图片
        /// </summary>
        /// <returns></returns>
        public ActionResult Image()
        {
            return View();
        }
        /// <summary>
        /// 移交
        /// </summary>
        /// <returns></returns>
        public ActionResult Transfer()
        {
            return View();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify()
        {
            return View();
        }
        
    }
}
