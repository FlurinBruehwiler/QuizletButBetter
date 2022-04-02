﻿// <auto-generated />
using System;
using M133.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace M133.Migrations
{
    [DbContext(typeof(QuizletContext))]
    partial class QuizletContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("M133.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Begriff")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LernsetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LernsetId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("M133.Models.Learn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LernsetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LernsetId");

                    b.ToTable("Learn");
                });

            modelBuilder.Entity("M133.Models.LearnCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("LearnId")
                        .HasColumnType("int");

                    b.Property<int>("PoolId")
                        .HasColumnType("int");

                    b.Property<int>("RepeatedFalse")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("LearnId");

                    b.HasIndex("PoolId");

                    b.HasIndex("UserId");

                    b.ToTable("LearnCards");
                });

            modelBuilder.Entity("M133.Models.Lernset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ErstellerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("ErstellerId");

                    b.ToTable("Lernsets");
                });

            modelBuilder.Entity("M133.Models.Pool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pools");
                });

            modelBuilder.Entity("M133.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("M133.Models.Card", b =>
                {
                    b.HasOne("M133.Models.Lernset", null)
                        .WithMany("Cards")
                        .HasForeignKey("LernsetId");
                });

            modelBuilder.Entity("M133.Models.Learn", b =>
                {
                    b.HasOne("M133.Models.Lernset", "Lernset")
                        .WithMany()
                        .HasForeignKey("LernsetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lernset");
                });

            modelBuilder.Entity("M133.Models.LearnCard", b =>
                {
                    b.HasOne("M133.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("M133.Models.Learn", "Learn")
                        .WithMany()
                        .HasForeignKey("LearnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("M133.Models.Pool", "Pool")
                        .WithMany()
                        .HasForeignKey("PoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("M133.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Learn");

                    b.Navigation("Pool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("M133.Models.Lernset", b =>
                {
                    b.HasOne("M133.Models.User", "Ersteller")
                        .WithMany()
                        .HasForeignKey("ErstellerId");

                    b.Navigation("Ersteller");
                });

            modelBuilder.Entity("M133.Models.Lernset", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
