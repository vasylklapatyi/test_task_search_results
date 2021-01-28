using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Task.Models;

namespace Test_Task
{
        public class DbEntities : DbContext
        {
            public static readonly string connectionstring = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog= SearchResultsDb;";
            //public static readonly string connectionstring = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog= ExchangesDb;";

        public DbSet<SearchResult> SearchResults { get; set; }
            public DbEntities()
            {
            //first/create the empty 
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            public DbContextOptions<DbEntities> _options;
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(connectionstring);
            }

        }
}
