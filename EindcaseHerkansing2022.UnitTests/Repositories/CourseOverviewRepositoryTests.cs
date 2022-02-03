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

public class CourseOverviewRepositoryTest
{
    [Fact]
    public async Task GetOverviewOfCourses_ShouldReturnAnOverviewOfCourses()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CourseContext>()
            .UseInMemoryDatabase(databaseName: "OverviewDatabase")
            .Options;

        var courses = new List<Course>
        {
            new Course() {
                Id = 1,
                Title = "TestCourse1",
                CourseCode = "TST1",
                Duration = 2,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 1,
                        StartDate = new DateTime(2020, 1, 1)
                    }
                }
            },
            new Course() {
                Id = 2,
                Title = "TestCourse2",
                CourseCode = "TST2",
                Duration = 2,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 2,
                        StartDate = new DateTime(2020, 1, 1)
                    }
                }
            },
            new Course() {
                Id = 3,
                Title = "TestCourse3",
                CourseCode = "TST3",
                Duration = 2,
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        CourseId = 3,
                        StartDate = new DateTime(2020, 1, 1)
                    }
                }
            }
        };

        // Act
        await using (var context = new CourseContext(options))
        {
            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
        }

        await using (var context = new CourseContext(options))
        {
            var repository = new CourseOverviewRepository(context);
            var result = await repository.GetOverviewOfCourses();

            // Assert
            Assert.Equal(3, result.Count());
        }
    }
}