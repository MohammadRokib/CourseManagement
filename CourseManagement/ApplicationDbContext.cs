using CourseManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    public class ApplicationDbContext : DbContext {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext() {
            // ConnectionString Home PC
            _connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=CourseManagement; Trusted_Connection=True";

            // ConnectionString Work PC
            // _connectionString = "Server=RONY; Database=CourseManagement; Uid=LEADSOFT\\rokib.khan Trusted_Connection=True";
            // _connectionString = "Server=RONY; Database=CourseManagement; Uid=SA; Pwd=leads@2024; Encrypt=False";
            _migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // modelBuilder.Entity<Attendance>().ToTable("Attendances");
            modelBuilder.Entity<Attendance>().HasKey(x => new { x.StudentId, x.CourseId, x.Date });

            modelBuilder.Entity<CourseRegistration>().ToTable("CourseRegistrations");
            modelBuilder.Entity<CourseRegistration>().HasKey(x => new {x.CourseId, x.StudentId});

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(x => x.Student)
                .WithMany(y => y.EnrolledCourses)
                .HasForeignKey(z => z.StudentId);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(x => x.Course)
                .WithMany(y => y.RegisteredStudents)
                .HasForeignKey(z => z.CourseId);

            modelBuilder.Entity<Teacher>()
                .HasMany(x => x.AssignedCourses)
                .WithOne(y => y.Instructor)
                .HasForeignKey(z => z.InstructorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
