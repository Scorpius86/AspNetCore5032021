﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using System;


namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts.Configurations
{
    public partial class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        partial void OnConfigurePartial(EntityTypeBuilder<Student> entity)
        {
            entity.Property(e => e.UserId).IsRequired();

            entity.HasOne(d => d.CreationUser)       
                .WithMany()
                .HasForeignKey(d => d.CreationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCreationUser");

            entity.HasOne(d => d.UpdateUser)
                .WithMany()
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentUpdateUser");

            entity.HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Student>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentUser");
        }
    }
}
