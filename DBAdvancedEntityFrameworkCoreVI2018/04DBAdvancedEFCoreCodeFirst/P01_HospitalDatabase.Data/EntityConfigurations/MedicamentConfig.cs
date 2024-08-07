﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfigurations
{
    public class MedicamentConfig : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(m => m.MedicamentId);

            builder.Property(m => m.Name).HasMaxLength(50).IsUnicode();

            builder.HasMany(m => m.Prescriptions).WithOne(pm => pm.Medicament).HasForeignKey(pm => pm.MedicamentId);
        }
    }
}