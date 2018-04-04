using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashMe.Service;
using CashMe.Core.Data;
using CashMe.Shared.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using CashMe.Shared.Common;

namespace CashMe.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ICashbackService CashbackService;
        private IGroupSiteService GroupSiteService;
        private ILinked_SiteService Linked_SiteService;
        private IVoucherService VoucherService;

        private ApplicationUserManager _userManager;

        public HomeController(ICashbackService CashbackService, IGroupSiteService GroupSiteService, ILinked_SiteService Linked_SiteService, IVoucherService VoucherService)
        {
            this.CashbackService = CashbackService;
            this.GroupSiteService = GroupSiteService;
            this.Linked_SiteService = Linked_SiteService;
            this.VoucherService = VoucherService;
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var lsCashback = CashbackService.GetCashbackMaxValueView();
            ViewBag.lsCashback = lsCashback;
            var lsGroupSite = GroupSiteService.GetAllGroupSite();
            ViewBag.lsGroupSite = lsGroupSite;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult TestApi()
        {
            var user = Request.GetOwinContext();
            //var client = new RestClient("http://localhost:44532/api/");
            //var request = new RestRequest("Account/GetCurrentUser", Method.GET);
            //var response = client.Execute<UserModel>(request);
            //var tmp = response.Data;
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult CashBackDetail()
        {
            var allCashback = CashbackService.GetCashbackView();
            ViewBag.allCashback = allCashback;
            var allLinkSite = Linked_SiteService.GetAllLinked_Site();
            ViewBag.allLinkSite = allLinkSite;
            return View();
        }
        public ActionResult MainCashBack()
        {
            var modelUser = new RegisterViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var getId = User.Identity;
                var user = UserManager.FindById(User.Identity.GetUserId());
                modelUser = user.MapTo();
            }
            return View(modelUser);
        }
        public ActionResult Voucher()
        {
            var lsVoucher = VoucherService.GetVoucherVIewStore();
            ViewBag.lsVoucher = lsVoucher;
            return View();
        }
        //hướng dẫn
        [AllowAnonymous]
        public ActionResult Guide()
        {
            return View();
        }
    }
}
