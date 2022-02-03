using EindcaseHerkansing2022.Models;
using Microsoft.EntityFrameworkCore;

namespace EindcaseHerkansing2022.Data;

public class CourseContext : DbContext
{
    public CourseContext(DbContextOptions<CourseContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseInstance> CourseInstances { get; set; }
}