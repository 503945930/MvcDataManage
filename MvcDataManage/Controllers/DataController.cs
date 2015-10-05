using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DefaultConnection;
using MvcDataManage.Bll;
using MvcDataManage.Filter;
using MvcDataManage.Models;

namespace MvcDataManage.Controllers
{
     [AuthorizeEx]
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Add(string Resid)
        {
            ViewBag.Resid = Resid;
            IEnumerable<ww_CaseReason> listReasons=BllBuilder.BuilCaseReasonBll().DropDownList();
            ViewBag.CaseReason =
                BllBuilder.BuilCaseReasonBll()
                    .DropDownList()
                    .Select(a => new SelectListItem() {Value = a.CaseReasonId.ToString()+"first", Text = a.CaseReasonName});

            ViewBag.CaseReasonn =
                  BllBuilder.BuilCaseReasonBll()
                      .DropDownListMingXi(61162)
                      .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString(), Text = a.CaseReasonName });

          
            return View();
        }
        public JsonResult Selete(int id)
        {

            return Json(BllBuilder.BuilCaseReasonBll().DropDownListMingXi(id), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 确定录入数据
        /// </summary>
        /// <param name="modelRe"></param>
        /// <param name="modelCase"></param>
        /// <param name="modelSuspect"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAction(LogMessage = 3)]
        public ActionResult DataAdd(ww_Re modelRe, ww_Case modelCase, ww_Suspect modelSuspect, string Num, AnyModel model, List<HttpPostedFileBase> fileUpload)
        {
          int Resid=  BllBuilder.BuilRes().DataAdd(modelRe, Num, modelCase, modelSuspect);
           

            foreach (HttpPostedFileBase item in fileUpload)
            {

                if (item != null && Array.Exists(model.FilesToBeUploaded.Split(','), s => s.Equals(item.FileName)))
                {
                    // string path = AppDomain.CurrentDomain.BaseDirectory + "Picture/";
                    // string filename = Path.GetFileName(Request.Files[upload].FileName);
                    string dt = DateTime.Now.Millisecond.ToString();
                    item.SaveAs(Server.MapPath(Path.Combine("~/Picture/", dt + item.FileName)));
                    ww_Photo myPhoto = new ww_Photo()
                    {
                        BoolDel = false,
                        ResID = Resid,
                        PhotoURL = "/Picture/" + dt + item.FileName,
                    };
                    BllBuilder.BuilPhoto().Upload(myPhoto);
                    // item.SaveAs(Server.MapPath("~/Picture/NewFolder1"));
                    //Save or do your action -  Each Attachment ( HttpPostedFileBase item ) 
                }
            }


          
            //ViewBag.Resid = Resid;
            return RedirectToAction("Alert", "Login", new { alert = "录入成功！！！" });
        }

        #region MyRegion
        public ActionResult test()
        {
            
            return null;
        } 
        #endregion

    }
}
