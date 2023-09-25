﻿// <auto-generated />
using System;
using EFCoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreWebApp.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreWebApp.Models.TAccount", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ACCOUNT_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ACCOUNT_NAME");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CREATED_BY");

                    b.Property<int?>("CustId")
                        .HasColumnType("int")
                        .HasColumnName("CUST_ID");

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_CREATED")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DateModifed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_MODIFED")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("AccountId")
                        .HasName("PK__T_ACCOUN__05B22F601D9B2048");

                    b.HasIndex("CustId");

                    b.ToTable("T_ACCOUNTS", (string)null);
                });

            modelBuilder.Entity("EFCoreWebApp.Models.TCustomer", b =>
                {
                    b.Property<int>("CustId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CUST_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustId"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_CREATED")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DateModifed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_MODIFED")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("LAST_NAME");

                    b.HasKey("CustId")
                        .HasName("PK__T_CUSTOM__93ABC0033BEC0F36");

                    b.ToTable("T_CUSTOMERS", (string)null);
                });

            modelBuilder.Entity("EFCoreWebApp.Models.TAccount", b =>
                {
                    b.HasOne("EFCoreWebApp.Models.TCustomer", "Cust")
                        .WithMany("TAccounts")
                        .HasForeignKey("CustId")
                        .HasConstraintName("FK__T_ACCOUNT__CUST___3B75D760");

                    b.Navigation("Cust");
                });

            modelBuilder.Entity("EFCoreWebApp.Models.TCustomer", b =>
                {
                    b.Navigation("TAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
