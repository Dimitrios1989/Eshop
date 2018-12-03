using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eshop.ClassLibrary.Models.Roles;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Eshop.ClassLibrary.Models;
using Eshop.Administration;
using Eshop.Administration.Models;

namespace Eshop.Controllers
{


    public class UsersController : Controller
    {
        private RoleManager<IdentityRole> _RoleManager { get; set; }
        private ApplicationUserManager userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }
        public UsersController()
        {
            this._RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }



        // GET: Users
        public ActionResult Index()
        {
            var roles = _RoleManager.Roles.ToList();
            var users = UserManager.Users.ToList().Select(x => new UserViewModel() {
                //Email = x.Email,
                UserName = x.UserName,
                Roles = x.Roles.Select(r => new RoleViewModel
                {
                    Id = r.RoleId.ToString(),
                    Name = roles.Where(rr => rr.Id == r.RoleId).First().Name
                }).ToList()
            });
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
