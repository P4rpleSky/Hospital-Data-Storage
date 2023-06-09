﻿using System;
using System.Collections.Generic;
using Kosta_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Kosta_Task.DbContexts;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentDepartmentId).HasColumnName("ParentDepartmentID");

            entity.HasOne(d => d.ParentDepartment).WithMany(p => p.InverseParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .HasConstraintName("FK_Categories_ParentId");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DocNumber).HasMaxLength(6);
            entity.Property(e => e.DocSeries).HasMaxLength(4);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.SurName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Empoyees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_DepartmentId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
