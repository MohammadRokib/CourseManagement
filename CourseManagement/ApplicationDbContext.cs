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
            _connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=CourseManagement; Trusted_Connection=True";
            _migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
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
    }
}
