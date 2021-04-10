﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.Exam.Infrastructure.Data.Audit.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Audit.Entities;
using System;


namespace Net5.AspNet.Exam.Infrastructure.Data.Audit.Contexts.Configurations
{
    public partial class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            entity.ToTable("AuditLog", "Audit");

            entity.Property(e => e.Action)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Browser)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Error)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Ipaddress)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IPAddress");

            entity.Property(e => e.Request)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Response)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Service)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(256);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AuditLog> entity);
    }
}
