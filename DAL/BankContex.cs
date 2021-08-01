﻿using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.DAL
{
    public class BankContex : DbContext
    {

        public BankContex(DbContextOptions<BankContex> options) : base(options)
        {

        }

        public DbSet<AdminLogin> BankAdmin { get; set; }
        public DbSet<BankingSystem.Models.AdminLoginViewModel> AdminLoginViewModel { get; set; }

        public DbSet<NewUser> AccHolder { get; set; }
        public DbSet<BankingSystem.Models.NewUserViewModel> NewUserViewModel { get; set; }

    }
}
