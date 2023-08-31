using Esafe_Team_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Esafe_Team_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Certificate> Certificates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(e => e.Client)
                .WithMany(e => e.ClientAddresses)
                .HasForeignKey(e => e.ClientID);

            modelBuilder.Entity<Certificate>()
               .HasOne(e => e.client)
               .WithMany(e => e.ClientCertificates)
               .HasForeignKey(e => e.ClientId);


            modelBuilder.Entity<CreditCard>()
               .HasOne(e => e.client)
               .WithMany(e => e.ClientCreditCards)
               .HasForeignKey(e => e.ClientId);

        }

    }
}
