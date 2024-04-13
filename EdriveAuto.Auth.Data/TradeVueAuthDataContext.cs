using Microsoft.EntityFrameworkCore;
using EDriveAuto.Auth.Data.Models;

#pragma warning disable CS8618

namespace EDriveAuto.Auth.Data
{
    public class EDriveAutoAuthDataContext : DbContext
    {
        public DbSet<AccountRole> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        public EDriveAutoAuthDataContext()
        {
        }
        
        public EDriveAutoAuthDataContext(DbContextOptions<EDriveAutoAuthDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if(ConnectionString.IsUseSqlite)
                    optionsBuilder.UseSqlite(ConnectionString.Get);
                else
                    optionsBuilder.UseSqlServer(ConnectionString.Get);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(c =>
            {
                c.ToTable(nameof(Account));
                c.Property(p => p.Email).IsRequired().HasMaxLength(100);
                c.Property(p => p.DateCreated).HasDefaultValueSql("getutcdate()").IsRequired();
                c.Property(p => p.SmsActivationCode).IsRequired();
                c.Property(p => p.UniqueKey).HasDefaultValueSql("newid()").IsRequired();
                c.Property(p => p.IsPhoneVerified).HasDefaultValue(false);
                c.Property(p => p.IsActive).HasDefaultValue(false);
                c.Property(p => p.IsEmailVerified).HasDefaultValue(false);
                c.Property(p => p.LastLoginDate).IsRequired(false);
                c.Property(p => p.EmailVerificationDate).IsRequired(false);
                c.HasIndex(p => p.UniqueKey).IsUnique();
                c.HasIndex(p => new { p.Email, p.AccountRoleID }).IsUnique();
                c.HasIndex(p => new { p.Email, p.AccountRoleID, p.MobilePhone }).IsUnique();
            });

            modelBuilder.Entity<RefreshToken>(c =>
            {
                c.ToTable(nameof(RefreshToken));
                c.Property(p => p.AccountID).IsRequired();
                c.Property(p => p.Token).IsRequired();
                c.Property(p => p.ExpireAt).IsRequired().HasDefaultValueSql("DATEADD(hour, 12, getutcdate())");
                c.HasIndex(p => p.Token).IsUnique();
            });

            modelBuilder.Entity<AccountRole>(c =>
            { 
                c.ToTable(nameof(AccountRole));
                c.Property(p => p.ID).ValueGeneratedNever();
                c.Property(p => p.Name).IsRequired();
                c.HasIndex(p => p.Name).IsUnique();
            });

            AddRoles(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


        private static void AddRoles(ModelBuilder modelBuilder)
        {
            var roles = Enum.GetValues<AuthRoles>()
                .Select(r => new AccountRole
                {
                    ID = (int) r,
                    Name = r.ToString(),
                    DateCreated = DateTime.UtcNow
                }).ToList();

            modelBuilder.Entity<AccountRole>().HasData(roles);
        }
    }
}