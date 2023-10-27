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
    public class MembroMcMapping : IEntityTypeConfiguration<MembroMc>
    {
        public void Configure(EntityTypeBuilder<MembroMc> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Imagem)
                .IsRequired()
                .HasColumnType("Varchar(200)");

            builder.Property(m => m.Nome)
                .IsRequired()
                .HasColumnType("Varchar(200)");

            builder.Property(m => m.Cpf)
                .IsRequired()
                .HasColumnType("Varchar(11)");

            builder.Property(m => m.Email)
                .IsRequired()
                .HasColumnType("Varchar(100)");

            builder.Property(m => m.Telefone)
                .IsRequired()
                .HasColumnType("Varchar(20)");

            //  Configuração Relacional 1 : 1 => Membro : Endereco
            builder.HasOne(m => m.Endereco)
                .WithOne(e => e.Membro);

            //Configuração Relacional 1 : N => Contatos de Emergencia  : Membro
            builder.HasOne(m => m.ContatoEmergencia)
                .WithOne(c => c.MembroMc);
            builder.ToTable("MembrosMc");
        }
    }
}
