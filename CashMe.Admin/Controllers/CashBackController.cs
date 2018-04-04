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
    [Authorize]
    public class CashbackController : Controller
    {
        #region Contractor
        private ICashbackService CashbackService;
        private ILinked_SiteService Linked_SiteService;
        private ICategoriesService CategoriesService;
        private IPercentService PercentService;
        public CashbackController(ICashbackService CashbackService, ILinked_SiteService Linked_SiteService,
        ICategoriesService CategoriesService, IPercentService PercentService)
        {
            this.CashbackService = CashbackService;
            this.Linked_SiteService = Linked_SiteService;
            this.CategoriesService = CategoriesService;
            this.PercentService = PercentService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CashbackService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: Cashback
        public ActionResult Index()
        {
            var lsLinked_Site = Linked_SiteService.GetAllLinked_Site();
            var lsCategories = CategoriesService.GetAllCategories();
            var lsPercent = PercentService.GetAllPercent();
            ViewBag.lsLinked_Site = lsLinked_Site;
            ViewBag.lsCategories = lsCategories;
            ViewBag.lsPercent = lsPercent;
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllCashback()
        {
            return Json(CashbackService.GetAllCashback(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getCashbackById(long id)
        {
            return Json(CashbackService.GetCashback(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(Main_Cashback model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    CashbackService.InsertCashback(model);
                }
                else
                {
                    var update = CashbackService.GetCashback(model.Id);
                    update.Linked_SiteId = model.Linked_SiteId;
                    update.CategoriesId = model.CategoriesId;
                    update.PercentId = model.PercentId;
                    update.CreateDate = DateTime.Now;
                    update.Flag = model.Flag;
                    CashbackService.UpdateCashback(update);

                }
                CashbackService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeleteCashback(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = CashbackService.GetCashback(id);
                    CashbackService.DeleteCashback(model);
                    CashbackService.Save();
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
            var allCashback = CashbackService.GetCashbackView();
            IEnumerable<CashbackModel> filteredCashback;
            //Check whether the Cashback should be filtered by keyword
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

                filteredCashback = CashbackService.GetCashbackView()
                   .Where(c => isNameSearchable && (c.SiteName.ToLower().Contains(param.sSearch.ToLower()) || c.CategoriesName.ToLower().Contains(param.sSearch.ToLower())
                             || c.Value.Equals(param.sSearch.ToLower())));
            }
            else
            {
                filteredCashback = allCashback;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CashbackModel, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.SiteName.ToString() :
                                                           sortColumnIndex == 4 && isAddressSortable ? c.CategoriesName.ToString() :
                                                           sortColumnIndex == 5 && isAddressSortable ? c.Value.ToString() :
                                                           sortColumnIndex == 6 && isTownSortable ? c.CreateDate.ToString() :
                                                           sortColumnIndex == 7 && isTownSortable ? c.Flag.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredCashback = filteredCashback.OrderBy(orderingFunction);
            else
                filteredCashback = filteredCashback.OrderByDescending(orderingFunction);

            var displayedCashback = filteredCashback.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCashback select new[] { Convert.ToString(c.Id), c.SiteName, c.CategoriesName, Convert.ToString(c.Value), Convert.ToString(c.CreateDate), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allCashback.Count(),
                iTotalDisplayRecords = filteredCashback.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}