using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DAL.Repository.Abstract;
using Project.Entities;
using Project.UI.Areas.Filter;
using Project.UI.Library;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminRole]
    
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;
        private readonly MailSender mailSender;

        public HomeController(IUnitOfWork _unitOfWork, MailSender mailSender)
        {
            unitOfWork = _unitOfWork;
            this.mailSender = mailSender;
        }
        public IActionResult Index()
        {
            var data = unitOfWork.Contacts.GetAll().ToList();
            ViewBag.contact = data;
            return View();
        }

        [HttpPost]
        public IActionResult EditContact(Contact contact)
        {
            unitOfWork.Contacts.Edit(contact);
            unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult About()
        {
            var data = unitOfWork.Abouts.GetAll().ToList();
            ViewBag.content = data;
            return View();
        }

        [HttpPost]
        public IActionResult About(About about)
        {
            unitOfWork.Abouts.Edit(about);
            unitOfWork.SaveChanges();
            return RedirectToAction("About");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SendMail(string content, string senderMail)
        {
            mailSender.SendMail(content, senderMail);
            return Json(true);
        }

        
    }
}