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
    public class VoucherController : Controller
    {
        #region Contractor
        private IVoucherService VoucherService;
        private ILinked_SiteService Linked_SiteService;

        public VoucherController(IVoucherService VoucherService, ILinked_SiteService Linked_SiteService)
        {
            this.VoucherService = VoucherService;
            this.Linked_SiteService = Linked_SiteService;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                VoucherService.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: Voucher
        public ActionResult Index()
        {
            var lsLinked_Site = Linked_SiteService.GetAllLinked_Site();
            ViewBag.lsLinked_Site = lsLinked_Site;
            return View();
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetAllVoucher()
        {
            return Json(VoucherService.GetAllVoucher(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult getVoucherById(long id)
        {
            return Json(VoucherService.GetVoucher(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult Modify(Voucher model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateDate = DateTime.Now;
                    VoucherService.InsertVoucher(model);
                }
                else
                {
                    var update = VoucherService.GetVoucher(model.Id);
                    update.Linked_SiteId = model.Linked_SiteId;
                    update.VoucherName = model.VoucherName;
                    update.Body = model.Body;
                    update.EndDate = model.EndDate;
                    update.Flag = model.Flag;
                    VoucherService.UpdateVoucher(update);

                }
                VoucherService.Save();
                return Json(new MessageResults { Status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new MessageResults { Status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeleteVoucher(int id)
        {
            try
            {
                if ( id != 0)
                {
                    var model = VoucherService.GetVoucher(id);
                    VoucherService.DeleteVoucher(model);
                    VoucherService.Save();
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
            var allVoucher = VoucherService.GetVoucherVIewStore();
            IEnumerable<VoucherModel> filteredVoucher;
            //Check whether the Voucher should be filtered by keyword
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

                filteredVoucher = VoucherService.GetVoucherVIewStore()
                   .Where(c => isNameSearchable && (c.VoucherName.ToLower().Contains(param.sSearch.ToLower()) || c.Body.ToLower().Contains(param.sSearch.ToLower())));
            }
            else
            {
                filteredVoucher = allVoucher;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<VoucherModel, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Id.ToString() :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.SiteName.ToString() :
                                                           sortColumnIndex == 3 && isAddressSortable ? c.LinkAffiliate.ToString() :
                                                           sortColumnIndex == 4 && isAddressSortable ? c.VoucherName.ToString() :
                                                           sortColumnIndex == 5 && isAddressSortable ? c.Body.ToString() :
                                                           sortColumnIndex == 6 && isTownSortable ? c.CreateDate.ToString() :
                                                           sortColumnIndex == 7 && isTownSortable ? c.EndDate.ToString() :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredVoucher = filteredVoucher.OrderBy(orderingFunction);
            else
                filteredVoucher = filteredVoucher.OrderByDescending(orderingFunction);

            var displayedVoucher = filteredVoucher.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedVoucher select new[] { Convert.ToString(c.Id), c.SiteName,c.VoucherName, c.Body, Convert.ToString(c.EndDate), Convert.ToString(c.Flag) };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allVoucher.Count(),
                iTotalDisplayRecords = filteredVoucher.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}