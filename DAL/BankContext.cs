using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.DAL
{
    public class BankContext : DbContext
    {

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }

        public DbSet<NewUser> AccHolder { get; set; }
        public DbSet<BankingSystem.Models.NewUserViewModel> NewUserViewModel { get; set; }
        public DbSet<BankingSystem.Models.Deposit> Deposit { get; set; }

    }
}
