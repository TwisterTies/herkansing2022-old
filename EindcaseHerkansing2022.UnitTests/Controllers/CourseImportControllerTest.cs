using System.IO;
using System.Text;
using System.Threading.Tasks;
using EindcaseHerkansing2022.Controllers;
using EindcaseHerkansing2022.Interfaces.CourseImport;
using EindcaseHerkansing2022.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest;

public class CourseImportControllerTest
{
    private readonly Mock<ICourseImportService> _mockCourseImportService = new();

    [Fact]
    public async Task CourseImport_ShouldReturn_AReply_WithAmountOfImportedCourses_And_CourseInstances()
    {
        // Arrange
        const string mockedFileContent = $@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 22/03/2021

Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 29/03/2021

Titel: Blazor
Cursuscode: BLZ
Duur: 5 dagen
Startdatum: 22/01/2021";

        var fileBytes = Encoding.UTF8.GetBytes(mockedFileContent);
        var mockFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "file.txt");
        
        CourseImportReply expectedReply = new()
        {
            AmountOfCoursesImported = 2,
            AmountOfCourseInstancesImported = 3
        };
        
        _mockCourseImportService.Setup(x => x.ImportCourses(mockFile)).ReturnsAsync(expectedReply);
        
        var courseImportController = new CourseImportController(_mockCourseImportService.Object);
        
        // Act
        var result = await courseImportController.ImportFile(mockFile);
        
        // Assert
        Assert.Equal(expectedReply, result);
    }
}