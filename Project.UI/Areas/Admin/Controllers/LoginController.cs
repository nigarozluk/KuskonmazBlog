using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DAL.Repository.Abstract;
using Project.UI.Library;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private IUnitOfWork unitOfWork;
        public LoginController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string pass)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pass))
            {
                var admin = unitOfWork.Admins.FirsOrDefault(k => k.adminName == name && k.adminpassword == pass);
                if (admin != null)
                {
                    // Session oluşturma
                    HttpContext.Session.SetObject("admin", admin);


                    return RedirectToAction("Index", "Post","Admin");
                }
                else
                {
                    TempData["msg"] = "false";
                }
            }
            else
            {
                TempData["msg"] = "false";
            }

            return RedirectToAction("Index","Login");
        }

        public IActionResult Logout()
        {

            // Session Silme
            HttpContext.Session.GetString("admin");

            return RedirectToAction("Index","Login");
        }
    }
}