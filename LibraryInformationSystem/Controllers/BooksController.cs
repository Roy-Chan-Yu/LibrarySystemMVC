using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using LibraryInformationSystem.Models;
using PagedList;

namespace LibraryInformationSystem.Controllers
{
    public class BooksController : Controller
    {
        private DemoEntities db = new DemoEntities();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Book.Take(5));
        }

        // Get: Books/Search
        public ActionResult Search(string queryString, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var books = from book in db.Book
                        .OrderBy(m => m.PublicTime)
                        select book;
            ViewBag.queryString = queryString;

            if (!String.IsNullOrEmpty(queryString))
            {
                books = books.Where(name => name.BookName.Contains(queryString));
            }
            var pagedResult = books.ToPagedList(currentPage, 5);
            return View(pagedResult);
        }

        public ActionResult AdvancedSearch(string ISBN, string category,
            string BookName, string Publisher, string Author,
            string Location, DateTime? PublicTime, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            ViewBag.ISBN = ISBN;
            ViewBag.Category = category;
            ViewBag.BookName = BookName;
            ViewBag.Publisher = Publisher;
            ViewBag.Author = Author;
            ViewBag.Location = Location;
            ViewBag.PublicTime = PublicTime;

            var result = from books in db.Book
                         .OrderBy(m => m.PublicTime)
                         select books;

            if (!String.IsNullOrEmpty(ISBN))
            {
                result = result.Where(name => name.ISBN.Contains(ISBN));
            }

            if (!String.IsNullOrEmpty(category))
            {
                result = result.Where(name => name.category.Contains(category));
            }

            if (!String.IsNullOrEmpty(BookName))
            {
                result = result.Where(name => name.BookName.Contains(BookName));
            }

            if (!String.IsNullOrEmpty(Publisher))
            {
                result = result.Where(name => name.Publisher.Contains(Publisher));
            }

            if (!String.IsNullOrEmpty(Author))
            {
                result = result.Where(name => name.Author.Contains(Author));
            }

            if (!String.IsNullOrEmpty(Location))
            {
                result = result.Where(name => name.Location.Contains(Location));
            }

            if (PublicTime != null)
            {
                result = result.Where(name => name.PublicTime >= PublicTime);
            }

            var pageResult = result.ToList().ToPagedList(currentPage, 5);
            return View(pageResult);



            //PropertyInfo[] props = book.GetType().GetProperties();
            //foreach (var prop in props)
            //{
            //    //if (prop.PropertyType.Name.Equals)
            //    Debug.WriteLine("{0}: {1}: {2}", prop.Name,
            //                                   prop.PropertyType.Name,
            //                                   prop.GetValue(book));
            //}

        }


        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,ISBN,category,BookName,Publisher,PublicTime,Author,Price,Location")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,ISBN,category,BookName,Publisher,PublicTime,Author,Price,Location")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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
