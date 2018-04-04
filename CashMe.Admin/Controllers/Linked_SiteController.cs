using CashMe.Core.Data;
using CashMe.Service;
using CashMe.Shared.Common;
using CashMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMe.Admin.Controllers
{
    public class Linked_SiteController : Controller
    {
        #region Contractor
        private ILinked_SiteService Linked_SiteService;
        private IGroupSiteService GroupSiteService;

        public Linked_SiteController(ILinked_SiteService linked_siteService, IGroupSiteService GroupSiteService)
        {
            this.Linked_SiteService = linked_siteService;
            this.GroupSiteService = GroupSiteService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Linked_SiteService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: Linked_Site
        public ActionResult Index()
        {
            var lsGroupSite = GroupSiteService.GetAllGroupSite();
            ViewBag.lsGroupSite = lsGroupSite;
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllLinked_Site()
        {
            return Json(Linked_SiteService.GetAllLinked_Site(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getLinked_SiteById(long id)
        {
            return Json(Linked_SiteService.GetLinked_Site(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(Linked_Site model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    model.ModifyDate = DateTime.Now;
                    Linked_SiteService.InsertLinked_Site(model);
                }
                else
                {
                    var update = Linked_SiteService.GetLinked_Site(model.Id);
                    update.SiteName = model.SiteName;
                    update.GroupSiteId = model.GroupSiteId;
                    update.LinkAffiliate = model.LinkAffiliate;
                    update.Logo = model.Logo;
                    update.Flag = model.Flag;
                    update.ModifyDate = DateTime.Now;
                    Linked_SiteService.UpdateLinked_Site(update);

                }
                Linked_SiteService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeleteLinked_Site(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = Linked_SiteService.GetLinked_Site(id);
                    Linked_SiteService.DeleteLinked_Site(model);
                    Linked_SiteService.Save();
                    return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }
        [AjaxValidateAntiForgeryToken]
        public JsonResult UploadLogo()
        {
            try
            {
                var file = Request.Files[0];
                string fullfilename = "data:image/png;base64," + Command.ImageToBase64(file, ImageFormat.Png);
                //string fileName = file.FileName ;// DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".jpg";
                //var path = Path.Combine(Server.MapPath("~/Content/Images/Logo/"));
                //Command.SaveImage(path, fileName, file);
                //string fullfilename = path.Substring(path.IndexOf("\\Content\\Images", StringComparison.Ordinal)) + fileName;
                return Json(new MessageResults { Status = "Success", Message = fullfilename }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var allLinked_Site = Linked_SiteService.GetAllLinked_Site();
            IEnumerable<Linked_Site> filteredLinked_Site;
            //Check whether the Linked_Site should be filtered by keyword
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

                filteredLinked_Site = Linked_SiteService.GetAllLinked_Site()
                   .Where(c => isNameSearchable && c.SiteName.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredLinked_Site = allLinked_Site;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Linked_Site, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? GroupSiteService.GetGroupSite(c.GroupSiteId).GroupName :
                                                           sortColumnIndex == 3 && isAddressSortable ? c.SiteName.ToString() :
                                                           sortColumnIndex == 4 && isTownSortable ? c.Flag.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredLinked_Site = filteredLinked_Site.OrderBy(orderingFunction);
            else
                filteredLinked_Site = filteredLinked_Site.OrderByDescending(orderingFunction);

            var displayedLinked_Site = filteredLinked_Site.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedLinked_Site select new[] { Convert.ToString(c.Id), GroupSiteService.GetGroupSite(c.GroupSiteId).GroupName, Convert.ToString(c.SiteName), Convert.ToString(c.ModifyDate), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allLinked_Site.Count(),
                iTotalDisplayRecords = filteredLinked_Site.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}