using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LibraryInformationSystem.Models;

namespace LibraryInformationSystem.Controllers
{
    public class CustomersController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Customers
        public ActionResult Index()
        {
            if (Session["Account"] != null)
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
        public ActionResult Login(Customer data)
        {
            if (ModelState.IsValid)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                var passwordBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data.Password_MD5));
                StringBuilder builder = new StringBuilder();
                for (int cnt = 0; cnt < passwordBytes.Length; cnt++)
                {
                    builder.Append(passwordBytes[cnt].ToString("x2"));
                }
                string password = builder.ToString();

                var customer = db.Customer.Where(account =>
                    account.Account.Equals(data.Account) &&
                    account.Password_MD5.Equals(password)).FirstOrDefault();

                if (customer != null)
                {
                    Session["Account"] = customer.Account.ToString();
                    Session["Name"] = customer.Name.ToString();
                    return RedirectToAction("Index");
                }

            }

            return View(data);
        }



        // GET: Customers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customer.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// GET: Customers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CustomerID,Account,Password_MD5,Name,Contact,email,Address,BirthDate,Create_Time,Update_Time,Sex,Status")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Customer.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(customer);
        //}

        //// GET: Customers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customer.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CustomerID,Account,Password_MD5,Name,Contact,email,Address,BirthDate,Create_Time,Update_Time,Sex,Status")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(customer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(customer);
        //}

        //// GET: Customers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customer.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Customer customer = db.Customer.Find(id);
        //    db.Customer.Remove(customer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
