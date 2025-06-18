using ControlGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Data
{
    public class ControlGastosDbContext : DbContext
    {
        public ControlGastosDbContext(DbContextOptions<ControlGastosDbContext> options)
            : base(options)
        {
        }

        public DbSet<RecurringExpense> RecurringExpenses { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<RecurringIncome> RecurringIncomes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecurringExpense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.DayOfPayment).IsRequired();
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.PaymentDate).IsRequired();
                entity.Property(p => p.Amount).IsRequired();
                entity
                  .HasOne(p => p.RecurringExpense)
                  .WithMany()
                  .HasForeignKey(p => p.RecurringExpenseId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RecurringIncome>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Name).IsRequired();
                entity.Property(i => i.FixedAmount).IsRequired();
                entity.Property(i => i.DayOfIncome).IsRequired();
            });
        }
    }
}
