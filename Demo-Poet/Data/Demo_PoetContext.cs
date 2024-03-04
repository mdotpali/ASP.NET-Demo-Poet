using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo_Poet.Models;

namespace Demo_Poet.Data
{
    public class Demo_PoetContext : DbContext
    {
        public Demo_PoetContext (DbContextOptions<Demo_PoetContext> options)
            : base(options)
        {
        }

        public DbSet<Demo_Poet.Models.Poet> Poet { get; set; } = default!;
    }
}
