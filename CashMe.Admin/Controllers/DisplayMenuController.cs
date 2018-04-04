using CashMe.Admin.Models;
using CashMe.Core.Data;
using CashMe.Service.DisplayMenu;
using CashMe.Service.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMe.Admin.Controllers
{
    public class DisplayMenuController : Controller
    {
        #region Ctor
        private readonly IDisplayMenuServices _displayMenuServices;
        private readonly IRoleServices _roleServices;

        public DisplayMenuController(IDisplayMenuServices displayMenuServices, IRoleServices roleServices)
        {
            _displayMenuServices = displayMenuServices;
            _roleServices = roleServices;
        }
        #endregion

        // GET: DisplayMenu
        public ActionResult Index()
        {
            var lstRole = _roleServices.GetAllRoles();
            return View(lstRole);
        }

        #region Interactive between view and controller
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetMenuById(int id)
        {
            return Json(_displayMenuServices.GetById(id), JsonRequestBehavior.AllowGet);
            //return Json(_accountService.GetUser(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(Display_MenuAdmin model)
        {
            if (!ModelState.IsValid)
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            var result = false;
                        


            if (result)
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load data to form
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var displayMenus = _displayMenuServices.GetAll();
            IEnumerable<Display_MenuAdmin> filteredMenu;
            //Check whether the Categories should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                //var nameFilter = Convert.ToString(Request["sSearch_1"]);
                //var addressFilter = Convert.ToString(Request["sSearch_2"]);
                //var townFilter = Convert.ToString(Request["sSearch_3"]);

                //Optionally check whether the columns are searchable at all 
                //var isNameSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                //var isAddressSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                //var isTownSearchable = Convert.ToBoolean(Request["bSearchable_3"]);

                filteredMenu = _displayMenuServices.GetAll().Where(aa => aa.Name.ToLower().Contains(param.sSearch.ToLower()));
                //   .Where(c => isNameSearchable && c.CategoriesName.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredMenu = displayMenus;
            }

            var isSortName = Convert.ToBoolean(Request["bSortable_1"]);
            //var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            //var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Display_MenuAdmin, string> orderingFunction = (c => c.Name);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredMenu = filteredMenu.OrderBy(orderingFunction);
            else
                filteredMenu = filteredMenu.OrderByDescending(orderingFunction);

            var datas = filteredMenu.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in datas select new[] { c.Id.ToString(), c.Name, c.IsActive.ToString(), c.IsOfTheWebsite.ToString(), c.IsOfTheAdmin.ToString() };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = displayMenus.Count(),
                iTotalDisplayRecords = filteredMenu.Count(),
                aaData = result
            },
            JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}