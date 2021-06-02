
using Microsoft.EntityFrameworkCore;

namespace DesafioFULLApi.Models
{
    public class TitleDelayContext : DbContext
    {
        public TitleDelayContext(DbContextOptions<TitleDelayContext> opt) : base(opt)
        {

        }

        public DbSet<TitleDelay> TitleDelays { get; set; }
        public DbSet<DebtInstallment> DebtInstallments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DebtInstallment>()
               .HasKey(d => new { d.TitleDelayId, d.Id });

            modelBuilder.Entity<DebtInstallment>()
                .HasOne(debt => debt.TitleDelay)
                .WithMany(title => title.DebtInstallments)
                .HasForeignKey(debt => debt.TitleDelayId)
                .HasConstraintName("ForeignKey_DebtInstallments_TitleDelay");

            modelBuilder.Entity<TitleDelay>().HasMany(td => td.DebtInstallments);

            /*modelBuilder.Entity<DebtInstallments>()
                .HasOne(di => di.titleDelay)
                .WithMany(td => td.DebtInstallments)
                .HasForeignKey(di => di.TitleDelayId);*/

            /*modelBuilder.Entity<TitleDelay>()
                .HasMany(td => td.DebtInstallments)
                .WithOne(di => di.TitleDelay)
                .IsRequired();*/
        }
}
}
