using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystemRemake.Models;

namespace SchoolManagmentSystemRemake.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EducationalLevel> educationalLevels { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get;set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTeacher>()
                .HasKey(tc => new { tc.TeacherId, tc.CourseId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
