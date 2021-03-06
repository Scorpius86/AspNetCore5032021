// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts.Configurations;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using System;

#nullable disable

namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts
{
    public partial class ClassroomContext : DbContext
    {
        public ClassroomContext()
        {
        }

        public ClassroomContext(DbContextOptions<ClassroomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new Configurations.CourseConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GradeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StudentConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
