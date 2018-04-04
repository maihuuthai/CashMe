using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CashMe.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Trang chu
            routes.MapRoute(
               "trangchu",
               "trang-chu" + ".html",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //cashback
            routes.MapRoute(
               "cashback",
               "cashback" + ".html",
               new { controller = "Home", action = "MainCashBack", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //huong dan su dung
            routes.MapRoute(
               "huong-dan-su-dung",
               "huong-dan-su-dung" + ".html",
               new { controller = "Home", action = "Guide", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //chi tiet danh muc huong chiet khau
            routes.MapRoute(
               "chi-tiet-danh-muc-huong-chiet-khau",
               "chi-tiet-danh-muc-huong-chiet-khau" + ".html",
               new { controller = "Home", action = "CashBackDetail", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //dang xuat
            routes.MapRoute(
               "dang-xuat",
               "tai-khoan/dang-xuat" + ".html",
               new { controller = "Account", action = "LogOff", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //dang nhap
            routes.MapRoute(
               "dang-nhap",
               "tai-khoan/dang-nhap" + ".html",
               new { controller = "Account", action = "Login", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //dang ky
            routes.MapRoute(
               "dang-ky",
               "tai-khoan/dang-ky" + ".html",
               new { controller = "Account", action = "Register", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );
            //quen mat khau
            routes.MapRoute(
               "quen-mat-khau",
               "tai-khoan/quen-mat-khau" + ".html",
               new { controller = "Account", action = "ForgotPassword", id = UrlParameter.Optional },
               new string[] { "CashMe.Admin.Controllers" }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}