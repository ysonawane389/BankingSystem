

using BankingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSystem.DAL
{
    public class NewUserRepository
    {
        private BankContex _dbcontext;
        public NewUserRepository(BankContex dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<NewUser> GetNewUser()
        {
            return _dbcontext.AccHolder.ToList();
        }

        public void CreateNewUser(NewUser user)
        {
            _dbcontext.AccHolder.Add(user);
            _dbcontext.SaveChanges();
        }
        public void EditNewUser(NewUser user)
        {
            try
            {
                _dbcontext.AccHolder.Update(user);
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            finally
            {

            }
        }

        public void DeleteUser(int AccNo)
        {
            try
            {
                var selectUser = _dbcontext.AccHolder.Where(i => i.AccNo == AccNo).FirstOrDefault();
                if (selectUser != null)
                {
                    _dbcontext.AccHolder.Remove(selectUser);
                    _dbcontext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {

            }

        }



    }
}
