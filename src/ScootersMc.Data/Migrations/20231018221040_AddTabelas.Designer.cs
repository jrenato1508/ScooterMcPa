﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScootersMc.Data.Context;

#nullable disable

namespace ScootersMc.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20231018221040_AddTabelas")]
    partial class AddTabelas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ScootersMc.Business.Models.ContatoEmergencia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MembroMcId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("MembroMcId");

                    b.ToTable("ContatoEmergencia", (string)null);
                });

            modelBuilder.Entity("ScootersMc.Business.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<Guid>("MembroMcId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("MembroMcId")
                        .IsUnique();

                    b.ToTable("Enderecos", (string)null);
                });

            modelBuilder.Entity("ScootersMc.Business.Models.MembroMc", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<int>("Hierarquia")
                        .HasColumnType("int");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<int>("TipoSanguineo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MembrosMc", (string)null);
                });

            modelBuilder.Entity("ScootersMc.Business.Models.ContatoEmergencia", b =>
                {
                    b.HasOne("ScootersMc.Business.Models.MembroMc", "MembroMc")
                        .WithMany("ContatoEmergencia")
                        .HasForeignKey("MembroMcId")
                        .IsRequired();

                    b.Navigation("MembroMc");
                });

            modelBuilder.Entity("ScootersMc.Business.Models.Endereco", b =>
                {
                    b.HasOne("ScootersMc.Business.Models.MembroMc", "Membro")
                        .WithOne("Endereco")
                        .HasForeignKey("ScootersMc.Business.Models.Endereco", "MembroMcId")
                        .IsRequired();

                    b.Navigation("Membro");
                });

            modelBuilder.Entity("ScootersMc.Business.Models.MembroMc", b =>
                {
                    b.Navigation("ContatoEmergencia");

                    b.Navigation("Endereco")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
