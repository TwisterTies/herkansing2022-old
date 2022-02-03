using EindcaseHerkansing2022.Models;

namespace EindcaseHerkansing2022.Interfaces.CourseImport;

public interface ICourseImportRepository
{
    Task AddToDatabase(Course course);
    
    Task AddCourseInstanceToDatabase(CourseInstance courseInstance, int courseId);

    Task <int> GetCourseId(Course course);

    Task<bool> CheckIfCourseExists(Course course);
    
    Task<bool> CheckIfCourseInstanceExists(CourseInstance courseInstance);
}