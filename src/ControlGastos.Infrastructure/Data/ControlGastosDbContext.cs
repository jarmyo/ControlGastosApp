using ControlGastos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Infrastructure.Data
{
    public class ControlGastosDbContext : IdentityDbContext<IdentityUser>
    {
        public ControlGastosDbContext(DbContextOptions<ControlGastosDbContext> options)
            : base(options)
        {
        }

        public DbSet<RecurringExpense> RecurringExpenses { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<RecurringIncome> RecurringIncomes { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<ExpenseInstallment> ExpenseInstallments { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Category
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired();
                e.Property(c => c.ColorHex).IsRequired();
                e.HasMany(c => c.Expenses)
                 .WithOne(x => x.Category!)
                 .HasForeignKey(x => x.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            // ExpenseInstallment
            modelBuilder.Entity<ExpenseInstallment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.SequenceNumber).IsRequired();
                e.Property(x => x.TotalSequences).IsRequired();
                e.HasOne(x => x.RecurringExpense)
                 .WithMany() // o .WithMany(r=>r.Installments) si añades colección
                 .HasForeignKey(x => x.RecurringExpenseId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

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

