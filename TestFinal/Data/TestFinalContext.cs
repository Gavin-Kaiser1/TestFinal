#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestFinal.Models;

namespace TestFinal.Data
{
    public class TestFinalContext : DbContext
    {
        public TestFinalContext (DbContextOptions<TestFinalContext> options)
            : base(options)
        {
        }

        public DbSet<TestFinal.Models.Inventory> Inventory { get; set; }
    }
}
