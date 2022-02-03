using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EindcaseHerkansing2022.Controllers;
using EindcaseHerkansing2022.Interfaces.Interfaces.CourseOverview;
using EindcaseHerkansing2022.Models;
using Xunit;
using Moq;

namespace EindcaseHerkansing2022.UnitTest;

public class CourseOverviewControllerTest
{
    private readonly Mock<ICourseOverviewService> _mockCourseOverviewService = new();
    
    [Fact]
    public async Task GetOverviewOfCourses_ShouldReturnAListOfCourseOverviews()
    {
        // Arrange
        var courseOverviewController = new CourseOverviewController(_mockCourseOverviewService.Object);
        var expectedCourses = new List<CourseOverview>
        {
            new() { StartDate = new DateTime(2022, 01, 01), Title = "C# Programmeren", Duration = 5 },
            new() { StartDate = new DateTime(2022, 02, 01), Title = "C# Programmeren", Duration = 5 },
            new() { StartDate = new DateTime(2022, 03, 02), Title = "C# Programmeren", Duration = 5 }
        };

        _mockCourseOverviewService.Setup(x => x.GetOverviewOfCourses()).ReturnsAsync(expectedCourses);

        // Act
        var actualCourses = await courseOverviewController.GetOverviewOfCourses();

        // Assert
        Assert.Equal(expectedCourses, actualCourses);
    }
}