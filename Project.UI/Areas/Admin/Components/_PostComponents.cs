using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Components
{
    public class _PostComponents:ViewComponent
    {
        private IUnitOfWork unitOfWork;
        public _PostComponents(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.PostComponents = unitOfWork.Posts.GetAll().Include(k => k.Comments).ToList();
            return View();
        }
    }
}
