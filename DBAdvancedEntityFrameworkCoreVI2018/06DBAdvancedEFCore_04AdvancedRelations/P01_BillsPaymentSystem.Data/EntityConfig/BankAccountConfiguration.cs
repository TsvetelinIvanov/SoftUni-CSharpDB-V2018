using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasOne(ba => ba.PaymentMethod).WithOne(pm => pm.BankAccount)
                .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId);

            builder.Property(ba => ba.BankName).IsUnicode();
        }
    }
}