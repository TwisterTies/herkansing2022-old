using System;
using System.Collections.Generic;
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
        if (courseOverview == null) throw new ArgumentNullException(nameof(courseOverview));
        _courseOverviewRepository.Setup(x => x.GetOverviewOfCourses()).ReturnsAsync(courseOverview);
        
        // Act
        var result = await _courseOverviewService.GetOverviewOfCourses();
        
        // Assert
        Assert.Equal(courseOverview, result);
    }
}