using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWebApp.Models;

public partial class MainDbContext : DbContext
{
    public MainDbContext()
    {
    }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAccount> TAccounts { get; set; }

    public virtual DbSet<TBankList> TBankLists { get; set; }

    public virtual DbSet<TCustomer> TCustomers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MJaber_XPS;Database=MainDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__T_ACCOUN__05B22F601D9B2048");

            entity.ToTable("T_ACCOUNTS");

            entity.HasIndex(e => e.CustId, "IX_T_ACCOUNTS_CUST_ID");

            entity.Property(e => e.AccountId).HasColumnName("ACCOUNT_ID");
            entity.Property(e => e.AccountName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACCOUNT_NAME");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CustId).HasColumnName("CUST_ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModifed)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFED");

            entity.HasOne(d => d.Cust).WithMany(p => p.TAccounts)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__T_ACCOUNT__CUST___3B75D760");
        });

        modelBuilder.Entity<TBankList>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PK__T_BANK_L__06D33C4674A2DD9D");

            entity.ToTable("T_BANK_LIST");

            entity.Property(e => e.BankId).HasColumnName("BANK_ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.NameEn)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME_EN");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("STATUS_ID");
        });

        modelBuilder.Entity<TCustomer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__T_CUSTOM__93ABC0033BEC0F36");

            entity.ToTable("T_CUSTOMERS");

            entity.Property(e => e.CustId).HasColumnName("CUST_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModifed)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFED");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
