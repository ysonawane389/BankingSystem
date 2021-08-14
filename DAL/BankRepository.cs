using Microsoft.EntityFrameworkCore;
using BankingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.DAL
{
    public class BankRepository
    {
        private BankContext _dbcontext;
        public BankRepository(BankContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        /////   for new User

        public List<NewUser> GetNewUser()
        {
            return _dbcontext.AccHolder.ToList();
        }

        public List<NewUser> GetUsersBySp()
        {
            return _dbcontext.AccHolder.FromSqlRaw("EXECUTE dbo.uspGetUsers").ToList();
        }

        public void CreateNewUser(NewUser user)
        {
            try
            {
                _dbcontext.AccHolder.Add(user);
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {

            }
        }
        public void EditUser(NewUser user)
        {
            _dbcontext.AccHolder.Update(user);
            _dbcontext.SaveChanges();
        }
        public void UpdateBalance(long Amount, string Password)
        {
            var usr = _dbcontext.AccHolder.Where(e => e.Password == Password).FirstOrDefault();
            usr.AvlBalance = usr.AvlBalance + Amount;
            _dbcontext.SaveChanges();

        }
        public void WithdrawBalance(long Amount, string Password)
        {
       
                var usr = _dbcontext.AccHolder.Where(e => e.Password == Password).FirstOrDefault();
                    usr.AvlBalance = usr.AvlBalance - Amount;
                    _dbcontext.SaveChanges();
        }
        public void DeleteUser(long Accno)
        {
            var selectUser = _dbcontext.AccHolder.Where(i => i.AccNo == Accno).FirstOrDefault();
            if (selectUser != null)
            {
                // _dbcontext.AccHolder.Remove(selectUser);
                selectUser.ActivationStatus = false;
                _dbcontext.AccHolder.Update(selectUser);
                _dbcontext.SaveChanges();
            }
        }

     
    }
}

