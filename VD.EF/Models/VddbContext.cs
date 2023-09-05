using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace VD.EF.Models;

public partial class VddbContext : DbContext
{
    public VddbContext()
    {
    }

    public VddbContext(DbContextOptions<VddbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MtAdmin> MtAdmins { get; set; }

    public virtual DbSet<MtBrand> MtBrands { get; set; }

    public virtual DbSet<MtPermission> MtPermissions { get; set; }

    public virtual DbSet<MtRole> MtRoles { get; set; }

    public virtual DbSet<PRolePermission> PRolePermissions { get; set; }

    public virtual DbSet<PTransaction> PTransactions { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var a = ConfigurationManager.ConnectionStrings["VD"].ConnectionString;
		optionsBuilder.UseSqlServer(a);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MtAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtAdmin__3213E83FB15754AB");

            entity.ToTable("mtAdmin");

            entity.HasIndex(e => e.Username, "UC_mtAdmin").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(200)
                .HasColumnName("password_salt");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.MtAdmins).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<MtBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtBrand__3213E83F632F4AFF");

            entity.ToTable("mtBrand");

            entity.HasIndex(e => e.Name, "UC_mtBrand").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.FlgDeleted).HasColumnName("flg_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<MtPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtPermis__3213E83F299B08F8");

            entity.ToTable("mtPermission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Display)
                .HasMaxLength(200)
                .HasColumnName("display");
            entity.Property(e => e.Seq).HasColumnName("seq");
            entity.Property(e => e.SubSeq).HasColumnName("sub_seq");
        });

        modelBuilder.Entity<MtRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtRole__3213E83F24E4C64D");

            entity.ToTable("mtRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role)
                .HasMaxLength(200)
                .HasColumnName("role");
        });

        modelBuilder.Entity<PRolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pRolePer__3213E83FD5F68754");

            entity.ToTable("pRolePermission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.PRolePermissions).HasForeignKey(d => d.PermissionId);

            entity.HasOne(d => d.Role).WithMany(p => p.PRolePermissions).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<PTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pTransac__3213E83F7CED7A94");

            entity.ToTable("pTransaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.FlgDeleted).HasColumnName("flg_deleted");
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .HasColumnName("result");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Brand).WithMany(p => p.PTransactions)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_pTransaction_mtBrand_brandId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
