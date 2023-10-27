using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScootersMc.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Data.Mappings
{
    public class ContatoEmergenciaMapping : IEntityTypeConfiguration<ContatoEmergencia>
    {
        public void Configure(EntityTypeBuilder<ContatoEmergencia> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("ContatoEmergencia");
        }
    }
}
