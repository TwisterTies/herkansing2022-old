using EindcaseHerkansing2022.Addons;
using EindcaseHerkansing2022.Interfaces.CourseImport;
using EindcaseHerkansing2022.Models;

namespace EindcaseHerkansing2022.Services;

public class CourseImportService : ICourseImportService
{
    private readonly ICourseFileSplitter _courseFileSplitter;
    private readonly ICourseImportRepository _courseImportRepository;

    
    public CourseImportService(ICourseFileSplitter courseFileSplitter, ICourseImportRepository courseImportRepository)
    {
        _courseFileSplitter = courseFileSplitter;
        _courseImportRepository = courseImportRepository;
    }

    public async Task<CourseImportReply> ImportCourses(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var fileContent = await reader.ReadToEndAsync();
        try
        {
            var courses = _courseFileSplitter.SplitFile(fileContent);
            var importedCourses = 0;
            var importedCourseInstances = 0;
            var duplicateCourses = 0;
            var duplicateCourseInstances = 0;

            foreach (var course in courses)
            {

                if (!await _courseImportRepository.CheckIfCourseExists(course))
                {
                    await _courseImportRepository.AddToDatabase(course);
                    importedCourses++;
                    importedCourseInstances += course.CourseInstances.Count;
                }
                else
                {
                    duplicateCourses++;
                    foreach (var courseInstance in course.CourseInstances)
                    {
                        if (!await _courseImportRepository.CheckIfCourseInstanceExists(courseInstance))
                        {
                            await _courseImportRepository.AddCourseInstanceToDatabase(courseInstance,
                                await _courseImportRepository.GetCourseId(course));
                            importedCourseInstances++;
                        }
                        else
                        {
                            duplicateCourseInstances++;
                        }
                    }
                }
            }

            return new CourseImportReply()
            {
                AmountOfCoursesImported = importedCourses,
                AmountOfCourseInstancesImported = importedCourseInstances,
                AmountOfDuplicateCourses = duplicateCourses,
                AmountOfDuplicateCourseInstances = duplicateCourseInstances
            };
        }
        catch (Exception e)
        {
            return new CourseImportReply
            {
                ErrorMessage = e.Message
            };
        }
    }
}