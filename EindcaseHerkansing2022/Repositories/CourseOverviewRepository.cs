using System.Globalization;
using EindcaseHerkansing2022.Data;
using EindcaseHerkansing2022.Interfaces.Interfaces.CourseOverview;
using EindcaseHerkansing2022.Models;
using Microsoft.EntityFrameworkCore;

namespace EindcaseHerkansing2022.Repositories;

public class CourseOverviewRepository : ICourseOverviewRepository   
{
    private readonly CourseContext _context;
    
    public CourseOverviewRepository(CourseContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CourseOverview>> GetOverviewOfCourses()
    {
        return await (from courseInstance in _context.CourseInstances
            join course in _context.Courses on courseInstance.CourseId equals course.Id
            orderby courseInstance.StartDate
            select new CourseOverview
            {
                Title = course.Title,
                Duration = course.Duration,
                StartDate = courseInstance.StartDate,
            }).ToListAsync();
    }

    public async Task<IEnumerable<CourseOverview>> GetCurrentWeekOverviewOfCourses(int weekNumber)
    {
        var culture = new CultureInfo("nl-NL");
        var listOfOverviews = await (from courseInstance in _context.CourseInstances
            join course in _context.Courses on courseInstance.CourseId equals course.Id
            orderby courseInstance.StartDate
            select new CourseOverview
            {
                Title = course.Title,
                Duration = course.Duration,
                StartDate = courseInstance.StartDate,
            }).ToListAsync();
        return listOfOverviews.Where(ci => weekNumber == culture.Calendar.GetWeekOfYear(ci.StartDate, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday));
    }
}
