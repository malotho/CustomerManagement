using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Auditable && (
                e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Auditable)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    //((Auditable)entityEntry.Entity).CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
                }
                else
                {
                    Entry((Auditable)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((Auditable)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                ((Auditable)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                //((Auditable)entityEntry.Entity).ModifiedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        public int GetMySequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlRaw("SELECT @result = (NEXT VALUE FOR shared.Code)", result);

            return (int)result.Value;
        }
        public int GetCurrentSequenceValue()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlRaw("SELECT @result = (SELECT Cast(ISNULL(seq.current_value,N'''') as INT) AS [Current Value] FROM sys.sequences AS seq where seq.name = 'Code')", result);

            return (int)result.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("Code", schema: "shared")
                .StartsAt(1)
                .IncrementsBy(1);


            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password123"));

                modelBuilder.Entity<User>().HasData(new User { firstName = "User1", lastName = "User1", username = "User1@User1", password = "password123", id = 1, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User2", lastName = "User2", username = "User2@User2", password = "password123", id = 2, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User3", lastName = "User3", username = "User3@User3", password = "password123", id = 3, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User4", lastName = "User4", username = "User4@User4", password = "password123", id = 4, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User5", lastName = "User5", username = "User5@User5", password = "password123", id = 5, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User6", lastName = "User6", username = "User6@User6", password = "password123", id = 6, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User7", lastName = "User7", username = "User7@User7", password = "password123", id = 7, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User8", lastName = "User8", username = "User8@User8", password = "password123", id = 8, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User9", lastName = "User9", username = "User9@User9", password = "password123", id = 9, passwordHash = passwordHash, passwordSalt = passwordSalt });
                modelBuilder.Entity<User>().HasData(new User { firstName = "User10", lastName = "User10", username = "User10@User10", password = "password123", id = 10, passwordHash = passwordHash, passwordSalt = passwordSalt });
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
