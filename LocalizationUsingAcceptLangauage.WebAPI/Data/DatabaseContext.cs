using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.WebAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }
    }

    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
