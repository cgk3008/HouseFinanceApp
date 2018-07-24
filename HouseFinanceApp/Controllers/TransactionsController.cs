using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HouseFinanceApp.Models;
using Microsoft.AspNet.Identity;

namespace HouseFinanceApp.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Account).Include(t => t.Category).Include(t => t.EnteredBy);
            return View(transactions.ToList());
        }

        // GET: HouseTransactions
        public ActionResult HouseTransactions()
        {
            var userId = User.Identity.GetUserId();
            //var acctIdByHouseId = db.Users.Find(id)/*.Accounts.ToList()*/;
            var userHouse = db.Households.Where(h => h.Id == User.Identity.GetHouseholdId());


            var transactions = db.Transactions.Where(t => t.Account.HouseholdId == User.Identity.GetHouseholdId()).Include(t => t.Account).Include(t => t.Category).Include(t => t.EnteredBy);


            return View(transactions.ToList());
        }

        // GET: AccountTransactions
        public ActionResult AccountTransactions(int? id) //need to pass account ID from HouseAccounts or MyAccounts View to this action
        {

            //setup a Trnsactions view model????


            var transactions = db.Transactions.Where(t => t.AccountId == id).Include(t => t.Account).Include(t => t.Category).Include(t => t.EnteredBy);

            //foreach (Transaction transaction in transactions)
            //{

            //}

            return View(transactions.ToList());
        }


        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {

            //var user = User.Identity.GetUserId();

            //var acct = db.PersonalAccounts.Find(user);

            //var acct = User.Identity.GetPersonalAccounts();
            //ViewBag.CreatedById = new SelectList(user, "Id", "Name", acct.CreatedById);
            var HouseholdId = User.Identity.GetHouseholdId().Value;
            var acct = db.PersonalAccounts.Where(h => h.HouseholdId == HouseholdId);

            ViewBag.AccountId = new SelectList(acct, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountId,Description,Date,Amount,Type,Void,CategoryId,EnteredById,Reconciled,ReconciledAmount,IsDeleted")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.EnteredById = User.Identity.GetUserId();
                transaction.IsDeleted = false;

                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.PersonalAccounts, "Id", "Name", transaction.AccountId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FullName", transaction.EnteredById);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            var HouseholdId = User.Identity.GetHouseholdId().Value;
            var acct = db.PersonalAccounts.Where(h => h.HouseholdId == HouseholdId);
            if (transaction == null)
            {
                return HttpNotFound();
            }



            ViewBag.AccountId = new SelectList(acct, "Id", "Name");

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            ViewBag.EnteredById = new SelectList(db.Users.Where(u => u.HouseholdId == HouseholdId), "Id", "FullName", transaction.EnteredById);
            ViewBag.Date = transaction.Date;
            

            return View(transaction);




        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*int? id,*/ [Bind(Include = "Id,AccountId,Description,Date,Amount,Type,Void,CategoryId,EnteredById,Reconciled,ReconciledAmount,IsDeleted")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {

                var acctId = transaction.AccountId;

                var transact = transaction.ReconciledAmount;
                var acctTransactions = db.Transactions.Where(t => t.AccountId == acctId).Include(r => r.ReconciledAmount);

                var personalBalance = db.PersonalAccounts.Where(p => p.Id == acctId).Include(b => b.Balance);

             
                //var acctTransactions2 = db.Transactions.Where(t => t.AccountId == acctId).ToList();

                foreach (var item in acctTransactions)
                {
                    var pBalance = db.PersonalAccounts;

                    if (item.AccountId == acctId && item.Reconciled == true)
                    {
                        item.ReconciledAmount = personalBalance - item.ReconciledAmount;
                    }
                }


                    db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("AccountTransactions", new { id = transaction.AccountId });
            }
            ViewBag.AccountId = new SelectList(db.PersonalAccounts, "Id", "Name", transaction.AccountId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            ViewBag.EnteredById = new SelectList(db.Users, "Id", "FullName", transaction.EnteredById);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("AccountTransactions", new { id = transaction.AccountId });
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
