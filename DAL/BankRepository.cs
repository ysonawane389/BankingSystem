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

        public List<AdminLogin> GetAdmin()
        {
            return _dbcontext.BankAdmin.ToList();
        }

        public void ValidateAdmin(AdminLogin login)
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

        //////////////    for new User
        
        public List<NewUser> GetNewUser()
        {        
                return _dbcontext.AccHolder.ToList();
        }

        public void CreateNewUser(NewUser user)
        {
            try
            {
                _dbcontext.AccHolder.Add(user);
                _dbcontext.SaveChanges();
            }
            catch(Exception e)
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

       
        public void DeleteUser(long Accno)
        {
            var selectUser = _dbcontext.AccHolder.Where(i => i.AccNo == Accno).FirstOrDefault();
            if(selectUser!=null)
            {
                _dbcontext.AccHolder.Remove(selectUser);
                _dbcontext.SaveChanges();
            }
        }

        ////// Login for USe


        public void ValidateUser(NewUser login)
        {
            try
            {
                var validate = (from user in _dbcontext.AccHolder
                                where user.EmailId == login.EmailId && user.MobNo == login.MobNo
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
