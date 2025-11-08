using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppDataBaseView.Models;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Load> Loads { get; set; }

    public virtual DbSet<TypesAuto> TypesAutos { get; set; }

    public virtual DbSet<TypesLoad> TypesLoads { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\Шура\\Desktop\\AppDataBase\\AppDataBaseView\\main_db.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeCode);

            entity.HasIndex(e => e.Passport, "IX_Employees_passport").IsUnique();

            entity.Property(e => e.EmployeeCode).HasColumnName("employee_code");
            entity.Property(e => e.Addres).HasColumnName("addres");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Fcs).HasColumnName("fcs");
            entity.Property(e => e.Gender)
                .HasColumnType("CHAR(1)")
                .HasColumnName("gender");
            entity.Property(e => e.Passport).HasColumnName("passport");
            entity.Property(e => e.Phonenumber)
                .HasColumnType("CHAR(8)")
                .HasColumnName("phonenumber");
            entity.Property(e => e.Position).HasColumnName("position");
           
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightCode);

            entity.Property(e => e.FlightCode).HasColumnName("flight_code");
            entity.Property(e => e.AriveData).HasColumnName("arive_data");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.EmployeeCode).HasColumnName("employee_code");
            entity.Property(e => e.From).HasColumnName("_from");
            entity.Property(e => e.IsBought)
                .HasColumnType("BOOL")
                .HasColumnName("is_bought");
            entity.Property(e => e.IsRefund)
                .HasColumnType("BOOL")
                .HasColumnName("is_refund");
            entity.Property(e => e.LoadCode).HasColumnName("load_code");
            entity.Property(e => e.Price)
                .HasColumnType("DECIMAL")
                .HasColumnName("price");
            entity.Property(e => e.SendDate).HasColumnName("send_date");
            entity.Property(e => e.Where)
                .HasColumnType("TEX")
                .HasColumnName("_where");

            entity.HasOne(d => d.EmployeeCodeNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.EmployeeCode)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.LoadCodeNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.LoadCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Load>(entity =>
        {
            entity.HasKey(e => e.LoadCode);

            entity.Property(e => e.LoadCode).HasColumnName("load_code");
            entity.Property(e => e.Describe).HasColumnName("describe");
            entity.Property(e => e.ExpDate).HasColumnName("exp_date");
            entity.Property(e => e.LoadTypeCode).HasColumnName("load_type_code");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.LoadTypeCodeNavigation).WithMany(p => p.Loads)
                .HasForeignKey(d => d.LoadTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TypesAuto>(entity =>
        {
            entity.HasKey(e => e.AutoTypeCode);

            entity.ToTable("TypesAuto");

            entity.Property(e => e.AutoTypeCode).HasColumnName("auto_type_code");
            entity.Property(e => e.Describe).HasColumnName("describe");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<TypesLoad>(entity =>
        {
            entity.HasKey(e => e.LoadTypeCode);

            entity.Property(e => e.LoadTypeCode).HasColumnName("load_type_code");
            entity.Property(e => e.AutoTypeCode).HasColumnName("auto_type_code");
            entity.Property(e => e.Describe).HasColumnName("describe");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.AutoTypeCodeNavigation).WithMany(p => p.TypesLoads)
                .HasForeignKey(d => d.AutoTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.EmployeeCodeNavigation)
            .WithMany(e => e.Flights)
            .HasForeignKey(f => f.EmployeeCode)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.LoadCodeNavigation)
            .WithMany(l => l.Flights)
            .HasForeignKey(f => f.LoadCode)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TypesAuto>()
            .HasMany(ta => ta.TypesLoads)
            .WithOne(tl => tl.AutoTypeCodeNavigation)
            .HasForeignKey(tl => tl.AutoTypeCode)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TypesLoad>()
            .HasMany(tl => tl.Loads)
            .WithOne(l => l.LoadTypeCodeNavigation)
            .HasForeignKey(l => l.LoadTypeCode)
            .OnDelete(DeleteBehavior.Cascade);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
