﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectGuard.Ef;

namespace ProjectGuard.Migrations
{
    [DbContext(typeof(ProjectGuardDbContext))]
    partial class ProjectGuardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.User_seq", "'User_seq', '', '2', '1', '', '', 'Int32', 'False'");

            modelBuilder.Entity("ProjectGuard.Ef.Entities.FileCheckResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("HashValueId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<bool>("Result")
                        .HasColumnType("boolean");

                    b.Property<int>("VerificationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HashValueId");

                    b.HasIndex("VerificationId");

                    b.ToTable("FileCheckResults");
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.HashValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("Hash")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("NeedHash")
                        .HasColumnType("boolean");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("HashValues");
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"User_seq\"')");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("HashPassword")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationTime = new DateTime(2020, 9, 29, 9, 33, 34, 495, DateTimeKind.Utc).AddTicks(3124),
                            HashPassword = "E0C0D34C03BECC6536359ADDCB082214D1F4919149AF2FDD9B6E0C8C0C8E86934735385EA09DA0AAEFC1652A5F553A70B8E9B5D2A3712AB5B6933FF1784F96F9",
                            Login = "Aadmin"
                        });
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.Verification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<bool>("Result")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Verifications");
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.FileCheckResult", b =>
                {
                    b.HasOne("ProjectGuard.Ef.Entities.HashValue", "HashValue")
                        .WithMany()
                        .HasForeignKey("HashValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectGuard.Ef.Entities.Verification", "Verification")
                        .WithMany("FileCheckResults")
                        .HasForeignKey("VerificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.HashValue", b =>
                {
                    b.HasOne("ProjectGuard.Ef.Entities.Project", "Project")
                        .WithMany("HashValues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectGuard.Ef.Entities.Verification", b =>
                {
                    b.HasOne("ProjectGuard.Ef.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
