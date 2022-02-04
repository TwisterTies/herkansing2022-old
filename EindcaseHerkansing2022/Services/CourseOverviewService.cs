using EindcaseHerkansing2022.Interfaces.Interfaces.CourseOverview;
using EindcaseHerkansing2022.Models;

namespace EindcaseHerkansing2022.Services;

public class CourseOverviewService : ICourseOverviewService
{
    private readonly ICourseOverviewRepository _courseOverviewRepository;
    
    public CourseOverviewService(ICourseOverviewRepository courseOverviewRepository)
    {
        _courseOverviewRepository = courseOverviewRepository;
    }
    
    public async Task<IEnumerable<CourseOverview>> GetOverviewOfCourses()
    {
        return await _courseOverviewRepository.GetOverviewOfCourses();
    }
    
    public async Task<IEnumerable<CourseOverview>> GetCurrentWeekOverviewOfCourses(int weekNumber)
    {
        return await _courseOverviewRepository.GetCurrentWeekOverviewOfCourses(weekNumber);
    }
    
}