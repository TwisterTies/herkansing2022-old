namespace EindcaseHerkansing2022.Interfaces.Interfaces.CourseOverview;

public interface ICourseOverviewService
{
    Task<IEnumerable<Models.CourseOverview>> GetOverviewOfCourses();
    
    Task<IEnumerable<Models.CourseOverview>> GetCurrentWeekOverviewOfCourses(int weekNumber);
}