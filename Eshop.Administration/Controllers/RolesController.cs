using Eshop.Administration.Models;
using Eshop.ClassLibrary.Models.Roles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class RolesController : Controller
    {
        //naming conversion
        private RoleManager<IdentityRole> _RoleManager { get; set; }
        public RolesController()
        {
            this._RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        //public RolesController(RoleManager<IdentityRole> r)
        //{
        //    this._RoleManager = r;
        //}
        // GET: Roles
        public ActionResult Index()
        {
            var roles = this._RoleManager.Roles.ToList();
            if ((roles != null) && (roles.Count() > 0))
            {
                return View(roles.Select(x => new RoleViewModel { Name = x.Name, Id = x.Id }));
            }
            return View();
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            var r = this._RoleManager.FindById(id.ToString());
            if (r != null)
            {
                return View(new RoleViewModel
                {
                    Name = r.Name,
                    Id = r.Id
                });
            }
            return HttpNotFound();
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var newRole = new IdentityRole { Name = model.Name, Id = model.Id };
                    this._RoleManager.Create(newRole);

                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {

            var r = this._RoleManager.FindById(id.ToString());
            if (r != null)
            {
                return View(new RoleViewModel
                {
                    Name = r.Name,
                    Id = r.Id
                });
            }
            return HttpNotFound();
        }

        // POST: Roles/Edit/5
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

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Roles/Delete/5
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
