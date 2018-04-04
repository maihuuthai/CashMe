using CashMe.Core.Data;
using CashMe.Service;
using CashMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMe.Admin.Controllers
{
    public class PercentController : Controller
    {
        #region Contractor
        private IPercentService percentService;

        public PercentController(IPercentService percentService)
        {
            this.percentService = percentService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                percentService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: Percent
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllPercent()
        {
            return Json(percentService.GetAllPercent(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getPercentById(long id)
        {
            return Json(percentService.GetPercent(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(Percent model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    model.ModifyDate = DateTime.Now;
                    percentService.InsertPercent(model);
                }
                else
                {
                    var update = percentService.GetPercent(model.Id);
                    update.Value = model.Value;
                    update.Flag = model.Flag;
                    update.ModifyDate = DateTime.Now;
                    percentService.UpdatePercent(update);

                }
                percentService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeletePercent(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = percentService.GetPercent(id);
                    percentService.DeletePercent(model);
                    percentService.Save();
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
            var allPercent = percentService.GetAllPercent();
            IEnumerable<Percent> filteredPercent;
            //Check whether the Percent should be filtered by keyword
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

                filteredPercent = percentService.GetAllPercent()
                   .Where(c => isNameSearchable && c.Value.Equals(param.sSearch.ToLower()));
            }
            else
            {
                filteredPercent = allPercent;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Percent, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.Value.ToString() :
                                                           sortColumnIndex == 3 && isTownSortable ? c.CreateDate.ToString() :
                                                           sortColumnIndex == 4 && isTownSortable ? c.ModifyDate.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredPercent = filteredPercent.OrderBy(orderingFunction);
            else
                filteredPercent = filteredPercent.OrderByDescending(orderingFunction);

            var displayedPercent = filteredPercent.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedPercent select new[] { Convert.ToString(c.Id), Convert.ToString(c.Value), Convert.ToString(c.CreateDate), Convert.ToString(c.ModifyDate), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allPercent.Count(),
                iTotalDisplayRecords = filteredPercent.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}