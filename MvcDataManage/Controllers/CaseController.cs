using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DefaultConnection;
using MvcDataManage.Bll;
using MvcDataManage.Filter;
using MvcDataManage.Models;

namespace MvcDataManage.Controllers
{
     [AuthorizeEx]
    public class CaseController : Controller
    {
        //
        // GET: /Case/
        
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 嫌疑人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Suspect()
        {
            return View();
        }
        /// <summary>
        /// 修改案件信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify(int id)
        {
           CaseJoinCabinet list = BllBuilder.BuilCase().ModifyByCaseId(id);

           //IEnumerable<ww_Cabinet> listItem = BllBuilder.BuilCabinet().DropDownList();//空柜子
          // IEnumerable<ww_Cabinet> listItem1 = BllBuilder.BuilCabinet().SelectByCabinetIId(list.CabinetIId);//当前柜子
            //IEnumerable<ww_Cabinet> listTatls = listItem.Union(listItem1).OrderBy(a=>a.CabinetIId);
            IEnumerable<ww_Cabinet> listTatls=BllBuilder.BuilCabinet().DropDownList();//所有柜子
            ViewBag.CabinetList = listTatls.ToList().Select(c => new SelectListItem { Value = c.CabinetIId.ToString(), Text = c.Num });
            ViewBag.CaseReason =
             BllBuilder.BuilCaseReasonBll()
                 .DropDownList()
                 .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString() + "first", Text = a.CaseReasonName });

            ViewBag.CaseReasonn =
                  BllBuilder.BuilCaseReasonBll()
                      .DropDownListMingXi(list.CaseReasonId_Paent)
                      .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString(), Text = a.CaseReasonName });
            //ViewBag.CabinetList = BllBuilder.BuilCabinet().DropDownList();
            ViewData["CaseReasonId_Paent"] = list.CaseReasonId_Paent + "first";
            ViewData["CaseReasonId"] = list.CaseReasonId;
            return View(list);
        }
       
        /// <summary>
        /// 提交修改案件信息
        /// </summary>
        /// <returns></returns>
         [HttpPost]
         [LoginAction(LogMessage = 4)]
        public ActionResult DoModify(ww_Case model)
        {
            if (BllBuilder.BuilCase().Modify(model))
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Alert", "Login", new { alert ="修改成功！！！"});
            }
            
            //return RedirectToAction("Index","Home");
            //CaseJoinCabinet list = BllBuilder.BuilCase().ModifyByCaseId(id);
            return RedirectToAction("Alert", "Login", new { alert = "修改失败，请重新修改！！！" });
        }
        /// <summary>
        /// 修改嫌疑人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifySuspect(int id)
        {
            ww_Suspect list = BllBuilder.BuilSuspect().SelectBySuspectId(id);
            return View(list);
        }
        /// <summary>
        /// 提交修改嫌疑人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [LoginAction(LogMessage = 4)]
        public ActionResult DoModifySuspect(ww_Suspect model)
        {
            if (BllBuilder.BuilSuspect().Modify(model))
            {
                return RedirectToAction("Alert", "Login", new { alert = "修改成功！！！" });
            }
           // BllBuilder.BuilSuspect().Modify(model);
            return RedirectToAction("Alert", "Login", new { alert = "修改失败，请重新修改！！！" });
            //CaseJoinCabinet list = BllBuilder.BuilCase().ModifyByCaseId(id);
            //return View(list);
        }
        /// <summary>
        /// 修改物证信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifiRes(int id)
        {
            List<ww_Re> listRe = BllBuilder.BuilRes().SelectByResID(id);
            return View(listRe.FirstOrDefault());
        }
        /// <summary>
        /// 确认修改物证信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [LoginAction(LogMessage = 4)]
        public ActionResult DoModifiRes(ww_Re model)
        {
            if (BllBuilder.BuilRes().Modify(model))
            {
                return RedirectToAction("Alert", "Login", new { alert = "修改成功！！！" });
            }
            // BllBuilder.BuilSuspect().Modify(model);
            return RedirectToAction("Alert", "Login", new { alert = "修改失败，请重新修改！！！" });
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelPhoto(int id)
        {
            if (BllBuilder.BuilPhoto().Del(id))
            {
                return RedirectToAction("Alert", "Login", new { alert = "删除成功！！！" });
            }

            return RedirectToAction("Alert", "Login", new { alert = "删除失败，请重新修改！！！" });
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload(int id)
        {
            ViewBag.ResId = id;
            return View();
        }
        /// <summary>
        /// 确认上传图片
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAction(LogMessage = 11)]
        public ActionResult UploadAction(AnyModel model, List<HttpPostedFileBase> fileUpload, int ResId)
        {
            // Your Code - / Save Model Details to DB

            // Handling Attachments - 
            foreach (HttpPostedFileBase item in fileUpload)
            {

                if (item != null && Array.Exists(model.FilesToBeUploaded.Split(','), s => s.Equals(item.FileName)))
                {
                   // string path = AppDomain.CurrentDomain.BaseDirectory + "Picture/";
                   // string filename = Path.GetFileName(Request.Files[upload].FileName);
                    string dt = DateTime.Now.Millisecond.ToString();
                    item.SaveAs(Server.MapPath(Path.Combine("~/Picture/", dt + item.FileName)));
                    ww_Photo myPhoto=new ww_Photo(){
                        BoolDel=false,
                        ResID = ResId,
                        PhotoURL = "/Picture/" + dt + item.FileName,
                        };
                    BllBuilder.BuilPhoto().Upload(myPhoto);
                    // item.SaveAs(Server.MapPath("~/Picture/NewFolder1"));
                    //Save or do your action -  Each Attachment ( HttpPostedFileBase item ) 
                }
            }
            //var file = Request.Files["fileUpload"];
            //file.SaveAs(Server.MapPath("~/Picture/NewFolder1"));
            return RedirectToAction("Alert", "Login", new { alert = "上传成功！！！" });
        }
        /// <summary>
        /// 移交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAction(LogMessage = 10)]
        public ActionResult Transfe(ww_Transfe model)
        {
            BllBuilder.BuilTransfe().Add(model);
            return RedirectToAction("Index", "Home");
        }
       
       

    }
}
