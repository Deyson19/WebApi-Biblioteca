﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi_DataAccess;

#nullable disable

namespace WebApi_DataAccess.Migrations
{
    [DbContext(typeof(BibliotecaDbContext))]
    [Migration("20240505004141_Relacion_Prestamo_TipoUsuario")]
    partial class Relacion_Prestamo_TipoUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi_DataAccess.Entities.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaMaximaDevolucion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPrestamo")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentificacionUsuario")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Isbn")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("TipoUsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoUsuarioId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("WebApi_DataAccess.Entities.TipoUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoUsuarios");
                });

            modelBuilder.Entity("WebApi_DataAccess.Entities.Prestamo", b =>
                {
                    b.HasOne("WebApi_DataAccess.Entities.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("TipoUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
