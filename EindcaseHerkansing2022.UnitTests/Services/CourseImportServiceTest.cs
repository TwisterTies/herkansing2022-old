using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EindcaseHerkansing2022.Addons;
using EindcaseHerkansing2022.Addons.Interfaces;
using EindcaseHerkansing2022.Interfaces.CourseImport;
using EindcaseHerkansing2022.Models;
using EindcaseHerkansing2022.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Services;

public class CourseImportServiceTest
{
    [Fact]
    public async Task CourseImport_ShouldImport_WhenCourseFoundAndCourseInstanceNotFound_IsFalse()
    {
        var courses = new List<Course>
        {
            new Course() {
                Id = 1,
                Title = "C# Programmeren",
                CourseCode = "CNETIN",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 1,
                        StartDate = new DateTime(2021, 3, 22)
                    }
                }
            },
            new Course() {
                Id = 2,
                Title = "Blazor",
                CourseCode = "BLZ",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 2,
                        StartDate = new DateTime(2021, 01, 22)
                    }
                }
            }
        };
        
        const string mockedFileContent = $@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 22/03/2021

Titel: Blazor
Cursuscode: BLZ
Duur: 5 dagen
Startdatum: 22/01/2021";
        
        var courseImportRepository = new Mock<ICourseImportRepository>();
        var courseFileSplitter = new Mock<ICourseFileSplitter>();
        courseImportRepository.Setup(cir => cir.CheckIfCourseExists(It.IsAny<Course>())).Returns(Task.FromResult(false));
        courseImportRepository.Setup(cir => cir.CheckIfCourseInstanceExists(It.IsAny<CourseInstance>())).Returns(Task.FromResult(false));
        courseFileSplitter.Setup(cfs => cfs.SplitFile(It.IsAny<string>())).Returns(courses);
        var courseImportService = new CourseImportService(courseFileSplitter.Object, courseImportRepository.Object);

        var fileBytes = Encoding.UTF8.GetBytes(mockedFileContent);
        var mockFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "file.txt");

        var reply = await courseImportService.ImportCourses(mockFile);
        
        Assert.Equal(2, reply.AmountOfCoursesImported);
        Assert.Equal(2, reply.AmountOfCourseInstancesImported);
    }
    
    [Fact]
    public async Task CourseImport_ShouldImport_WhenCoursesFound_ButCourseInstancesNotFound()
    {
        var courses = new List<Course>
        {
            new Course() {
                Id = 1,
                Title = "C# Programmeren",
                CourseCode = "CNETIN",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 1,
                        StartDate = new DateTime(2021, 3, 22)
                    }
                }
            },
            new Course() {
                Id = 2,
                Title = "Blazor",
                CourseCode = "BLZ",
                Duration = 5,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 2,
                        StartDate = new DateTime(2021, 01, 22)
                    }
                }
            }
        };
        
        const string mockedFileContent = $@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 22/03/2021

Titel: Blazor
Cursuscode: BLZ
Duur: 5 dagen
Startdatum: 22/01/2021";
        
        var courseImportRepository = new Mock<ICourseImportRepository>();
        var courseFileSplitter = new Mock<ICourseFileSplitter>();
        courseImportRepository.Setup(cir => cir.CheckIfCourseExists(It.IsAny<Course>())).Returns(Task.FromResult(true));
        courseImportRepository.Setup(cir => cir.CheckIfCourseInstanceExists(It.IsAny<CourseInstance>())).Returns(Task.FromResult(false));
        courseFileSplitter.Setup(cfs => cfs.SplitFile(It.IsAny<string>())).Returns(courses);
        var courseImportService = new CourseImportService(courseFileSplitter.Object, courseImportRepository.Object);

        var fileBytes = Encoding.UTF8.GetBytes(mockedFileContent);
        var mockFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "file.txt");

        var reply = await courseImportService.ImportCourses(mockFile);
        
        Assert.Equal(0, reply.AmountOfCoursesImported);
        Assert.Equal(2, reply.AmountOfCourseInstancesImported);
    }
}