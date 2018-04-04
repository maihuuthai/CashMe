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
    public class CategoriesController : Controller
    {
        #region Contractor
        private ICategoriesService cateService;

        public CategoriesController(ICategoriesService cateService)
        {
            this.cateService = cateService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cateService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllCategories()
        {
            return Json(cateService.GetAllCategories(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getCategoriesById(long id)
        {
            return Json(cateService.GetCategories(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(Category model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    model.ModifyDate = DateTime.Now;
                    cateService.InsertCategories(model);
                }
                else
                {
                    var update = cateService.GetCategories(model.Id);
                    update.CategoriesName = model.CategoriesName;
                    update.Flag = model.Flag;
                    update.ModifyDate = DateTime.Now;
                    cateService.UpdateCategories(update);

                }
                cateService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeleteCategories(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = cateService.GetCategories(id);
                    cateService.DeleteCategories(model);
                    cateService.Save();
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
            var allCategories = cateService.GetAllCategories();
            IEnumerable<Category> filteredCategories;
            //Check whether the Categories should be filtered by keyword
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

                filteredCategories = cateService.GetAllCategories()
                   .Where(c => isNameSearchable && c.CategoriesName.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredCategories = allCategories;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Category, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.CategoriesName :
                                                           sortColumnIndex == 3 && isTownSortable ? c.CreateDate.ToString() :
                                                           sortColumnIndex == 4 && isTownSortable ? c.ModifyDate.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredCategories = filteredCategories.OrderBy(orderingFunction);
            else
                filteredCategories = filteredCategories.OrderByDescending(orderingFunction);

            var displayedCategories = filteredCategories.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCategories select new[] { Convert.ToString(c.Id), c.CategoriesName, Convert.ToString(c.CreateDate), Convert.ToString(c.ModifyDate), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allCategories.Count(),
                iTotalDisplayRecords = filteredCategories.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}