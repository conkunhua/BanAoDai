using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanAoDai.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        protected BanAoDaiDBContext db = new BanAoDaiDBContext();
        // GET: Admin/Auth
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(string username,string password)
        {
            string error = null;
            User user = db.User.Where(x => x.Status == 1 
            && x.CountError == 1 && 
            (x.UserName == username || x.Email == username) 
            && x.Password == password).FirstOrDefault();
            if (user == null)
            {
                error = "Thông tin đăng nhập không chính xác !";
            }
            else
            {
                Session["UserAdmin"] = username;
                Session["UserAdmin_id"] = user.Id;
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error= error;
            return View();
        }
        public ActionResult LogoutAdmin()
        {
            Session["UserAdmin"] = "";
            Session["UserAdmin_id"] = "";
            return Redirect("~/Admin/Login");
        }
    }
}