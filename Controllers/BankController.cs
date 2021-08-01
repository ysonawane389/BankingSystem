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

        AdminLoginRepository adminRepo;
        NewUserRepository userRepo;
        
        public BankController(BankContex _dbcontext)
        {
            adminRepo = new AdminLoginRepository(_dbcontext);
            userRepo = new NewUserRepository(_dbcontext);
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
           AdminLoginViewModel ValidAdmin = adminRepo.GetAdmin().Where(i =>i.AdminId==Adminid && i.AdminName == adminname && i.Password == password).Select(e => new AdminLoginViewModel
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
            return RedirectToAction("HomePage");//remain
        }


        // User deatails

        public IActionResult NewUserDetails()
        {
            var test = userRepo.GetNewUser().ToList();
            var userlist = userRepo.GetNewUser().Select(e => new NewUserViewModel
            {
                AccNo=e.AccNo,
                AccHolderName=e.AccHolderName,
                MobNo=e.MobNo,
                EmailId=e.EmailId,
                Gender=e.Gender,
                DateOfBirth=e.DateOfBirth,
                City=e.City,
                AvlBalance=e.AvlBalance
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
                NewUser  userEntity = new NewUser()
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
                userRepo.CreateNewUser(userEntity);
            }
            return RedirectToAction("NewUserDetails");
        }

        [HttpGet]
        public IActionResult Edit(int accno)
        {
            NewUserViewModel selecteUser = userRepo.GetNewUser().Where(i => i.AccNo == accno).Select(e => new NewUserViewModel
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
            return View(selecteUser);
        }

        [HttpPost]
        public IActionResult Edit(NewUserViewModel user)
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
                userRepo.EditNewUser(userEntity);
            }
            return RedirectToAction("NewUserDetails");
        }

        [HttpGet]
        public IActionResult Details(int accno)
        {
            NewUserViewModel selecteUser = userRepo.GetNewUser().Where(i => i.AccNo  == accno).Select(e => new NewUserViewModel
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
            return View(selecteUser);
        }


        [HttpGet]
        public IActionResult Delete(int accno)
        {
            NewUserViewModel selecteUser = userRepo.GetNewUser().Where(i => i.AccNo == accno).Select(e => new NewUserViewModel
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
            return View(selecteUser);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int accno)
        {
            userRepo.DeleteUser(accno);
            return RedirectToAction("NewUserDetails");
        }


    }
}
