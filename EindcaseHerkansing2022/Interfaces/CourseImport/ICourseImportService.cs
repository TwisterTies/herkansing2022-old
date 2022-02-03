using EindcaseHerkansing2022.Models;

namespace EindcaseHerkansing2022.Interfaces.CourseImport;

public interface ICourseImportService
{
    Task<CourseImportReply> ImportCourses(IFormFile file);
}