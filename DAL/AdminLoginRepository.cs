using BankingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.DAL
{
    public class AdminLoginRepository
    {
        private BankContex _dbcontext;
        public AdminLoginRepository(BankContex dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<AdminLogin> GetAdmin()
        {
            return _dbcontext.BankAdmin.ToList();
        }

        public void ValidateUser(AdminLogin login)
        {
            try
            {
                var validate = (from user in _dbcontext.BankAdmin
                                where user.AdminId == login.AdminId && user.Password == login.Password && user.AdminName == login.AdminName
                                select user).FirstOrDefault();
                if (validate == null)
                {
                    Console.WriteLine("Enter Valid Password");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("LOGIN SUCCESSFULLY...!");
            }
        }
    }
}
