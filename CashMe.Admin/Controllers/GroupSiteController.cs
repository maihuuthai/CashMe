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
    public class GroupSiteController : Controller
    {
        #region Contractor
        private IGroupSiteService groupService;

        public GroupSiteController(IGroupSiteService groupService)
        {
            this.groupService = groupService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                groupService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: GroupSite
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllGroupSite()
        {
            return Json(groupService.GetAllGroupSite(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getGroupSiteById(long id)
        {
            return Json(groupService.GetGroupSite(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(GroupSite model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    model.ModifyDate = DateTime.Now;
                    groupService.InsertGroupSite(model);
                }
                else
                {
                    var update = groupService.GetGroupSite(model.Id);
                    update.GroupName = model.GroupName;
                    update.Flag = model.Flag;
                    update.ModifyDate = DateTime.Now;
                    groupService.UpdateGroupSite(update);

                }
                groupService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeleteGroupSite(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = groupService.GetGroupSite(id);
                    groupService.DeleteGroupSite(model);
                    groupService.Save();
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
            var allGroupSite = groupService.GetAllGroupSite();
            IEnumerable<GroupSite> filteredGroupSite;
            //Check whether the GroupSite should be filtered by keyword
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

                filteredGroupSite = groupService.GetAllGroupSite()
                   .Where(c => isNameSearchable && c.GroupName.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredGroupSite = allGroupSite;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<GroupSite, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.GroupName :
                                                           sortColumnIndex == 3 && isTownSortable ? c.CreateDate.ToString() :
                                                           sortColumnIndex == 4 && isTownSortable ? c.ModifyDate.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredGroupSite = filteredGroupSite.OrderBy(orderingFunction);
            else
                filteredGroupSite = filteredGroupSite.OrderByDescending(orderingFunction);

            var displayedGroupSite = filteredGroupSite.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedGroupSite select new[] { Convert.ToString(c.Id), c.GroupName, Convert.ToString(c.CreateDate), Convert.ToString(c.ModifyDate), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allGroupSite.Count(),
                iTotalDisplayRecords = filteredGroupSite.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}