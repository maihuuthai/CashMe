using CashMe.Core.Data;
using CashMe.Service;
using CashMe.Service.Models;
using CashMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMe.Admin.Controllers
{
    public class CashoutController : Controller
    {
        #region Contractor
        private ICashoutService CashoutService;

        public CashoutController(ICashoutService CashoutService)
        {
            this.CashoutService = CashoutService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CashoutService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: Cashout
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllCashout()
        {
            return Json(CashoutService.GetAllCashout(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getCashoutById(long id)
        {
            return Json(CashoutService.GetCashout(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(History_Checkout model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    model.ModifyDate = DateTime.Now;
                    CashoutService.InsertCashout(model);
                }
                else
                {
                    var update = CashoutService.GetCashout(model.Id);
                    update.UserId = model.UserId;
                    update.Main_CaskbackId = model.Main_CaskbackId;
                    update.PriceMain = model.PriceMain;
                    update.Status = model.Status;
                    update.Flag = model.Flag;
                    update.ModifyDate = DateTime.Now;
                    CashoutService.UpdateCashout(update);

                }
                CashoutService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeleteCashout(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = CashoutService.GetCashout(id);
                    CashoutService.DeleteCashout(model);
                    CashoutService.Save();
                    return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var allCashout = CashoutService.GetAllCashoutView();
            IEnumerable<CashoutModel> filteredCashout;
            //Check whether the Cashout should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                var nameFilter = Convert.ToString(Request["sSearch_1"]);
                var addressFilter = Convert.ToString(Request["sSearch_2"]);
                var townFilter = Convert.ToString(Request["sSearch_3"]);

                //Optionally check whether the columns are searchable at all 
                var isNameSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isAddressSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isTownSearchable = Convert.ToBoolean(Request["bSearchable_3"]);

                filteredCashout = CashoutService.GetAllCashoutView()
                   .Where(c => isNameSearchable && (c.SiteName.Equals(param.sSearch.ToLower()) || c.PriceMain.Equals(param.sSearch.ToLower())) ||
                                                    c.StatusName.Equals(param.sSearch.ToLower()) || c.Cashback.Equals(param.sSearch.ToLower()));
            }
            else
            {
                filteredCashout = allCashout;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CashoutModel, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.SiteName.ToString() :
                                                           sortColumnIndex == 3 && isTownSortable ? c.CreateDate.ToString() :
                                                           sortColumnIndex == 4 && isTownSortable ? c.PriceMain.ToString() :
                                                           sortColumnIndex == 5 && isTownSortable ? c.Cashback.ToString() :
                                                           sortColumnIndex == 6 && isTownSortable ? c.StatusName.ToString() :
                                                           sortColumnIndex == 7 && isTownSortable ? c.Flag.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredCashout = filteredCashout.OrderBy(orderingFunction);
            else
                filteredCashout = filteredCashout.OrderByDescending(orderingFunction);

            var displayedCashout = filteredCashout.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCashout select new[] { Convert.ToString(c.Id), Convert.ToString(c.CreateDate), Convert.ToString(c.PriceMain), Convert.ToString(c.Cashback), Convert.ToString(c.StatusName), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allCashout.Count(),
                iTotalDisplayRecords = filteredCashout.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}