using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EindcaseHerkansing2022.Data;
using EindcaseHerkansing2022.Models;
using EindcaseHerkansing2022.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Repositories;

public class ImportRepositoryTests
{
    [Fact]
    public async Task CheckIfCourseAndCourseInstanceExists_ShouldReturn_True()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CourseContext>()
            .UseInMemoryDatabase(databaseName: "ReturnTrueDatabase")
            .Options;
        
        Course testCourse1 = new Course()
        {
            Id = 1,
            Title = "TestCourse",
            CourseCode = "TST",
            Duration = 2,
            CourseInstances = new List<CourseInstance>()
            {
                new CourseInstance()
                {
                    CourseId = 1,
                    StartDate = new DateTime(2020, 1, 1)
                }
            }
        };
        // Act
        await using (var context = new CourseContext(options))
        {
            context.Courses.Add(testCourse1);
            await context.SaveChangesAsync();
        }

        await using (var context = new CourseContext(options))
        {
            // Assert
            var repository = new CourseImportRepository(context);
            var existingCourse = await repository.CheckIfCourseExists(testCourse1);
            var listOfCourseInstances = testCourse1.CourseInstances.ToList();
            var existingCourseInstance = await repository.CheckIfCourseInstanceExists(listOfCourseInstances[0]);
            Assert.True(existingCourseInstance);
            Assert.True(existingCourse);
        }
    }

    [Fact]
    public async Task GetCourseId_ShouldReturn_CourseId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CourseContext>()
            .UseInMemoryDatabase(databaseName: "ReturnIdDatabase")
            .Options;
        
        Course testCourse1 = new Course()
        {
            Id = 1,
            Title = "TestCourse",
            CourseCode = "TST",
            Duration = 2,
            CourseInstances = new List<CourseInstance>()
            {
                new CourseInstance()
                {
                    CourseId = 1,
                    StartDate = new DateTime(2020, 1, 1)
                }
            }
        };
        // Act
        await using (var context = new CourseContext(options))
        {
            context.Courses.Add(testCourse1);
            await context.SaveChangesAsync();
        }
        
        await using (var context = new CourseContext(options))
        {
            // Assert
            var repository = new CourseImportRepository(context);
            var existingCourseId = await repository.GetCourseId(testCourse1);
            var expectedCourseId = testCourse1.Id;
            Assert.Equal(expectedCourseId, existingCourseId);
        }
    }
}