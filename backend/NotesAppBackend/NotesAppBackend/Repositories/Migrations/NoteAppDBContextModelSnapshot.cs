﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotesAppBackend.Repositories;

#nullable disable

namespace NotesAppBackend.Repositories.Migrations
{
    [DbContext(typeof(NoteAppDBContext))]
    partial class NoteAppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryNote", b =>
                {
                    b.Property<int>("CategoriesIdCategory")
                        .HasColumnType("int");

                    b.Property<int>("NotesIdNote")
                        .HasColumnType("int");

                    b.HasKey("CategoriesIdCategory", "NotesIdNote");

                    b.HasIndex("NotesIdNote");

                    b.ToTable("CategoryNote");
                });

            modelBuilder.Entity("NotesAppBackend.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategory"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegistrationDate")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("IdCategory");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NotesAppBackend.Models.Note", b =>
                {
                    b.Property<int>("IdNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNote"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("RegistrationDate")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("TextContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdNote");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("CategoryNote", b =>
                {
                    b.HasOne("NotesAppBackend.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesIdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesAppBackend.Models.Note", null)
                        .WithMany()
                        .HasForeignKey("NotesIdNote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
