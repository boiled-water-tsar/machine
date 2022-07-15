﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using machines.DataBase;

#nullable disable

namespace machines.Migrations
{
    [DbContext(typeof(MachineDbContext))]
    partial class MachineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:CaseInsensitive", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .HasAnnotation("Npgsql:DefaultColumnCollation", "CaseInsensitive")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("machines.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("MachineId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("machines.Machine", b =>
                {
                    b.OwnsMany("machines.Jobs.Job", "Jobs", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<int>("DurationSeconds")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("FK_MachineId")
                                .HasColumnType("integer");

                            b1.Property<Guid>("JobId")
                                .HasColumnType("uuid");

                            b1.Property<string>("MachineName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Status")
                                .HasColumnType("integer");

                            b1.HasKey("Id");

                            b1.HasIndex("FK_MachineId");

                            b1.ToTable("Jobs");

                            b1.WithOwner()
                                .HasForeignKey("FK_MachineId");
                        });

                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
