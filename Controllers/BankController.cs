using BankingSystem.DAL;
using BankingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Controllers
{
    public class BankController : Controller
    {


        BankRepository bankRepo;
        private BankContext dbContext;


        public BankController(BankContext _dbcontext)
        {

            bankRepo = new BankRepository(_dbcontext);
            dbContext = _dbcontext;

        }

        //Login Login
        [HttpGet]
        public IActionResult UserLogIn()
        {

            return View();
        }

        [HttpPost]
        // Below function is use to Authorized
        public IActionResult UserLogIn(string EmailId, string Password)
        {

            NewUserViewModel selectUser = bankRepo.GetNewUser().Where(i => i.EmailId == EmailId && i.Password == Password && i.ActivationStatus==true
           ).Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
              ActivationStatus=e.ActivationStatus,
                Password = e.Password
              
            }).FirstOrDefault();

            if (selectUser == null)
            {
                ViewBag.errormessage = "Enter Valid EmailId,  Password";
                return View();
            }
            else if (Password=="4455")
            {
                return RedirectToAction("NewUserDetails");
            }
            else
            {
              

                //Set session
                HttpContext.Session.SetString("Name", selectUser.AccHolderName); //we can store any type string, int, object, list etc.
                HttpContext.Session.SetString("EmailId", selectUser.EmailId);

                // return RedirectToAction("UserDetails", "Bank", new { EmailId = EmailId });

                return RedirectToAction("Home");
            }


        }

        // User deatails

        public IActionResult NewUserDetails()
        {
            var test = bankRepo.GetNewUser().ToList();
            
            var userlist = bankRepo.GetNewUser().Where(i=>i.ActivationStatus==true).Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City
               
            }).ToList();
            return View(userlist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            NewUserViewModel user = new NewUserViewModel();
            return View(user);
        }
        [HttpPost]
        public IActionResult Create(NewUserViewModel user)

        {
            if (ModelState.IsValid)
            {
                NewUser userEntity = new NewUser()
                {
                    AccNo = user.AccNo,
                    AccHolderName = user.AccHolderName,
                    MobNo = user.MobNo,
                    EmailId = user.EmailId,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    City = user.City,
                    AvlBalance = user.AvlBalance,
                    Password = user.Password,
                    ActivationStatus=user.ActivationStatus
                };
                bankRepo.CreateNewUser(userEntity);
            }
            return RedirectToAction("NewUserDetails");
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            NewUserViewModel editselect = bankRepo.GetNewUser().Where(i => i.AccNo == id).Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
                AvlBalance = e.AvlBalance,
                Password=e.Password,
                ActivationStatus=e.ActivationStatus

            }).FirstOrDefault();
            return View(editselect);
        }

        [HttpPost]
        public IActionResult Edit(NewUserViewModel edit)
        {
            if (ModelState.IsValid)
            {
                NewUser editEntity = new NewUser()
                {
                    AccNo = edit.AccNo,
                    AccHolderName = edit.AccHolderName,
                    MobNo = edit.MobNo,
                    EmailId = edit.EmailId,
                    Gender = edit.Gender,
                    DateOfBirth = edit.DateOfBirth,
                    City = edit.City,
                    AvlBalance = edit.AvlBalance,
                    Password=edit.Password,
                    ActivationStatus=edit.ActivationStatus
                };
                bankRepo.EditUser(editEntity);
            }
            return RedirectToAction("NewUserDetails");
        }


        [HttpGet]
        public IActionResult Details(long id)
        {
            NewUserViewModel selectUser = bankRepo.GetNewUser().Where(i => i.AccNo == id).Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
                AvlBalance = e.AvlBalance
            }).FirstOrDefault();
            return View(selectUser);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            NewUserViewModel selectUser = bankRepo.GetNewUser().Where(i => i.AccNo == id).Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
                AvlBalance = e.AvlBalance
            }).FirstOrDefault();
            return View(selectUser);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            bankRepo.DeleteUser(id);
            return RedirectToAction("NewUserDetails");
        }
        [HttpGet]
        public ActionResult Deposit(long Amount, string password)
        {
            Deposit obj = new Deposit();

            return View(obj);
        }

        [HttpPost]
        public ActionResult Deposit(Deposit obj)
        {
            bankRepo.UpdateBalance(obj.Amount, obj.Password);

            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult Withdraw(long Amount, string password)
        {
            Deposit obj = new Deposit();
         
            return View(obj);
        }

        [HttpPost]
        public ActionResult Withdraw(Deposit obj)
        {
            var usr = dbContext.AccHolder.Where(e => e.Password == obj.Password).FirstOrDefault();
            if (usr.AvlBalance >obj.Amount)
            {
                bankRepo.WithdrawBalance(obj.Amount,obj.Password);
            }
            else
            {
                ViewBag.errormessage = "Insufficient Balance";
                
                return View();
            }
               
                return RedirectToAction("Home");
            }
        

        [HttpGet]
        public ActionResult Home()
        {
            string emailId = HttpContext.Session.GetString("EmailId");

            ViewBag.Name = HttpContext.Session.GetString("Name");

            var users = bankRepo.GetUsersBySp().Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
                AvlBalance = e.AvlBalance
            }).Where(e => e.EmailId == emailId).ToList();

            return View(users);
        }

    }
}