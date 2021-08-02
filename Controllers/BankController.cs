using BankingSystem.DAL;
using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Controllers
{
    public class BankController : Controller
    {

        BankRepository bankRepo;
       
        
        public BankController(BankContext _dbcontext)
        {
            bankRepo = new BankRepository(_dbcontext);
            
        }

        //HomePage
      public IActionResult HomePage()
        {
            return View();
        }

// For Admin Login
        [HttpGet]
        public IActionResult AdminLogIn()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogIn(int Adminid, string adminname, string password)
        {
           AdminLoginViewModel ValidAdmin = bankRepo.GetAdmin().Where(i =>i.AdminId==Adminid && i.AdminName == adminname && i.Password == password).Select(e => new AdminLoginViewModel
            {
               AdminId=e.AdminId,
                AdminName = e.AdminName,
                Password = e.Password
            }).FirstOrDefault();
            if (ValidAdmin == null)
            {
                ViewBag.errormessage = "Enter Valid AdminId,  AdminName and Password";
                return RedirectToAction("AdminLogIn");
            }
            return RedirectToAction("NewUserDetails");
        }


        // User deatails

        public IActionResult NewUserDetails()
        {
            var test = bankRepo.GetNewUser().ToList();
            var userlist = bankRepo.GetNewUser().Select(e => new NewUserViewModel
            {
                AccNo = e.AccNo,
                AccHolderName = e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
                AvlBalance = e.AvlBalance
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
                    AvlBalance = user.AvlBalance
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
                AccNo=e.AccNo,
                AccHolderName=e.AccHolderName,
                MobNo = e.MobNo,
                EmailId = e.EmailId,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                City = e.City,
                AvlBalance = e.AvlBalance

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
                    AccNo= edit.AccNo,
                    AccHolderName= edit.AccHolderName,
                    MobNo = edit.MobNo,
                    EmailId = edit.EmailId,
                    Gender = edit.Gender,
                    DateOfBirth = edit.DateOfBirth,
                    City = edit.City,
                    AvlBalance = edit.AvlBalance
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
                AccNo=e.AccNo,
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
        public IActionResult  DeleteConfirmed(long id)
        {
            bankRepo.DeleteUser(id);
            return RedirectToAction("NewUserDetails");
        }




        [HttpGet]
        public IActionResult UserLogIn()
        {

            return View();
        }

        [HttpPost]
        public IActionResult UserLogIn(string emailid,long mobno)
        {
            NewUserViewModel ValidUser = bankRepo.GetNewUser().Where(i => i.EmailId == emailid && i.MobNo == mobno ).Select(e => new NewUserViewModel
            {
               EmailId=e.EmailId,
               MobNo=e.MobNo
            }).FirstOrDefault();
            if (ValidUser == null)
            {
                ViewBag.errormessage = "Enter Valid UserID,  Password";
                return RedirectToAction("UserLogIn");
            }
            return RedirectToAction("NewUserDetails");
        }


    }
}
