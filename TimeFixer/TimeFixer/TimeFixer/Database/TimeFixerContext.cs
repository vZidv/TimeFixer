﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimeFixer.Database;

public partial class TimeFixerContext : DbContext
{
    public TimeFixerContext()
    {
    }

    public TimeFixerContext(DbContextOptions<TimeFixerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ModelClock> ModelClocks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2BSAL1V\\SQL;Database=TimeFixer;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<ModelClock>(entity =>
        {
            entity.ToTable("ModelClock");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Manufacturer).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.DateReceiving).HasColumnType("date");
            entity.Property(e => e.DateReturn).HasColumnType("date");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Client");

            entity.HasOne(d => d.IdClockNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdClock)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_ModelClock");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_OrderStatus");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.Property(e => e.NameStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.ToTable("Setting");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Settings)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Setting_UserStatus");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Ld);

            entity.ToTable("User");

            entity.Property(e => e.Ld).HasColumnName("ld");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.IdSettingNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdSetting)
                .HasConstraintName("FK_User_Setting");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.ToTable("UserStatus");

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
