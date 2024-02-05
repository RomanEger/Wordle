using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordlee.DataBase
{
    class RepositoryContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Word> Words { get; set; } = null!;
        public DbSet<Resolved> Resolveds { get; set; } = null!;

        public RepositoryContext()
            : base()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .Build();
            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlServer(
"Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=rom4k_wordle;User ID=rom4k_wordle;Password=qwerty;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(x => x.Login).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Login).HasMaxLength(30);
            modelBuilder.Entity<User>().ToTable("User").HasCheckConstraint("CK_User_Login", "Login <>'' AND LEN(Login)>0");
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(20);
            modelBuilder.Entity<User>().ToTable("User").HasCheckConstraint("CK_User_Password", "Password <>'' AND LEN(Password)>0");

            modelBuilder.Entity<Word>().HasIndex(x => x.WordName).IsUnique();
            modelBuilder.Entity<Word>().Property(x => x.WordName).HasMaxLength(11);
            modelBuilder.Entity<Word>().ToTable("Word").HasCheckConstraint("CK_Word_Name", "WordName<>'' AND LEN(WordName)>0");

            modelBuilder.Entity<Resolved>().HasIndex(x => new
            {
                x.UserId,
                x.WordId
            }).IsUnique();

            User user = new()
            {
                Id = 1,
                Login = "test",
                Password = "test"
            };
            modelBuilder.Entity<User>().HasData(user);
        }

    }
}
