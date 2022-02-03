using EindcaseHerkansing2022.Data;
using EindcaseHerkansing2022.Interfaces.CourseImport;
using EindcaseHerkansing2022.Models;
using Microsoft.EntityFrameworkCore;

namespace EindcaseHerkansing2022.Repositories;

public class CourseImportRepository : ICourseImportRepository
{
    private readonly CourseContext _context;
    
    public CourseImportRepository(CourseContext context)
    {
        _context = context;
    }
    
    public async Task AddToDatabase(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task AddCourseInstanceToDatabase(CourseInstance courseInstance, int courseId)
    {
        var course = await _context.Courses.Include(x => x.CourseInstances).FirstAsync(c => c.Id == courseId);
        course.CourseInstances.Add(courseInstance);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> CheckIfCourseExists(Course course)
    {
        var courseExists = await _context.Courses.Where(c =>
            c.Title == course.Title && c.Duration == course.Duration && c.CourseCode == course.CourseCode).AnyAsync();
        return courseExists;
    }
    
    public async Task<int> GetCourseId(Course course)
    {
        var courseId = await _context.Courses.Where(c =>
            c.Title == course.Title && c.Duration == course.Duration && c.CourseCode == course.CourseCode).Select(c => c.Id).FirstOrDefaultAsync();
        return courseId;
    }

    public async Task<bool> CheckIfCourseInstanceExists(CourseInstance courseInstance)
    {
        var courseInstanceExists = await _context.CourseInstances.AnyAsync(ci => ci.StartDate == courseInstance.StartDate);
        return courseInstanceExists;
    }
}