﻿// <auto-generated />
using Assignment_Bosch.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment_Bosch.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20230917053441_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assignment_Bosch.Integration.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Assignment_Bosch.Integration.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id")
                        .HasName("PK__UserInfos");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Assignment_Bosch.Integration.UserInfo", b =>
                {
                    b.HasOne("Assignment_Bosch.Integration.Roles", "Roles")
                        .WithMany("UserInfos")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__UserInfo__Role");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Assignment_Bosch.Integration.Roles", b =>
                {
                    b.Navigation("UserInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
