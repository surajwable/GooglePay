using GooglePay.Models;
using Microsoft.EntityFrameworkCore;

namespace GooglePay.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        
        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        //we always have to specify the configuraztion of decimal manually.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(10,2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<AccountHolder>()
                .Property(t => t.Balance)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Sender)
            .WithMany(u => u.SentTransactions)
            .HasForeignKey(t => t.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Receiver)
                .WithMany(u => u.RecievedTransactions)
                .HasForeignKey(t => t.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
