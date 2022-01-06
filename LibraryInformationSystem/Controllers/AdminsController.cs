using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LibraryInformationSystem.Models;
using PagedList;

namespace LibraryInformationSystem.Controllers
{
    public class AdminsController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Admins
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin data)
        {
            if (ModelState.IsValid)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] passwordBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data.Password_MD5));
                StringBuilder builder = new StringBuilder();

                for (int count = 0; count < passwordBytes.Length; count++)
                {
                    builder.Append(passwordBytes[count].ToString("x2"));
                }
                string password = builder.ToString();

                var admin = db.Admin.Where(
                    account => account.AdminName.Equals(data.AdminName))
                    .Where(p => p.Password_MD5.Equals(password))
                    .FirstOrDefault();

                if (admin != null)
                {
                    Session["Admin"] = admin.AdminName.ToString();
                    return RedirectToAction("Index");
                }

            }

            return View(data);
        }

        public ActionResult Members()
        {

            return View(db.Admin.ToList().OrderBy(m => m.Create_Time));
        }

        public ActionResult Books(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var books = from book in db.Book
                        .OrderBy(m => m.PublicTime)
                        select book;
            var pagedResult = books.ToPagedList(currentPage, 5);
            return View(pagedResult);

        }

        // Get: Admins/Create
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult addBooks()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addBooks(Book data)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(data);
                db.SaveChanges();
                return RedirectToAction("Books");
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Members");

            }

            return View(admin);
        }

        // GET: Admin/Edit/?
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Admin admin = db.Admin.Find(id);

            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,AdminName,Password_MD5,Contact,Sex,Status")] Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View(admin);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Admin admin = db.Admin.Find(id);

            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
