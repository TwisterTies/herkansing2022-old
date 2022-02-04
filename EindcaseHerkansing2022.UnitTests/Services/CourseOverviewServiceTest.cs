using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using EindcaseHerkansing2022.Interfaces.Interfaces.CourseOverview;
using EindcaseHerkansing2022.Models;
using EindcaseHerkansing2022.Services;
using Moq;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Services;

public class CourseOverviewServiceTest
{
    private readonly Mock<ICourseOverviewRepository> _courseOverviewRepository;
    private readonly CourseOverviewService _courseOverviewService;
    
    public CourseOverviewServiceTest()
    {
        _courseOverviewRepository = new Mock<ICourseOverviewRepository>();
        _courseOverviewService = new CourseOverviewService(_courseOverviewRepository.Object);
    }
    
    [Fact]
    public async Task GetOverviewOfCourses_ShouldReturnAListOfCourseOverviews()
    {
        // Arrange
        var courseOverview = new List<CourseOverview>();
        _courseOverviewRepository.Setup(x => x.GetOverviewOfCourses()).ReturnsAsync(courseOverview);
        
        // Act
        var result = await _courseOverviewService.GetOverviewOfCourses();
        
        // Assert
        Assert.Equal(courseOverview, result);
    }
    
    [Fact]
    public async Task GetCurrentWeekOfCourses_ShouldReturnAListOfCoursesInTheGivenWeek()
    {
        // Arrange
        var initialCourseOverviews = new List<CourseOverview>
        {
            new ()
            {
                Title = "Overview1",
                Duration = 2,
                StartDate = new DateTime(2022, 04, 02),
            },
            new ()
            {
                Title = "Overview2",
                Duration = 2,
                StartDate = new DateTime(2020, 01, 01)
            }
        };
        
        var expected = new List<CourseOverview>
        {
            new ()
            {
                Title = "Overview1",
                Duration = 2,
                StartDate = new DateTime(2022, 04, 02),
            }
        };
        const int selectedWeekNumber = 5;
        _courseOverviewRepository.Setup(x => x.GetCurrentWeekOverviewOfCourses(selectedWeekNumber)).ReturnsAsync(expected);
        
        // Act
        var result = await _courseOverviewService.GetCurrentWeekOverviewOfCourses(selectedWeekNumber);
        
        // Assert
        Assert.NotEqual(result, initialCourseOverviews);
    }
}