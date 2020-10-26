using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LibraryDdContext:DbContext
    {
        public LibraryDdContext(DbContextOptions<LibraryDdContext> options) : base(options)
        {

        }

        public DbSet<LibraryModel> Library { get; set; }
    }
}
