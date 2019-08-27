using AspnetBBS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetBBS.DataContext
{
    //code first, inherit by DbContext
    public class AppDbContext : DbContext
    {
        //Codes to create table
        //DbSet<Type Entity>: Generic type
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
  

        //to make connection connected Database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string info as a parameter, 
            //get an info from https://www.connectionstrings.com/
            //@ meanning: deliver exactly same thing in double quotes
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=AspnetNoteDb;User Id=sa;Password=password;");
        }
    }
}
