using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EindcaseHerkansing2022.Addons;
using EindcaseHerkansing2022.Addons.FileSplitter;
using EindcaseHerkansing2022.Addons.Interfaces;
using EindcaseHerkansing2022.Models;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;

namespace EindcaseHerkansing2022.UnitTest.Splitters;

public class FileSplitter
{
    [Fact]
    public void SplitFile_ShouldSplitFile()
    {
        // Arrange
        const string mockedFileContent = $@"Titel: C# Programmeren
Cursuscode: CNETIN
Duur: 5 dagen
Startdatum: 22/03/2021

Titel: Blazor
Cursuscode: BLZ
Duur: 2 dagen
Startdatum: 22/01/2021";
    
        List<Course> courses = CreateCourseFileSplitter().SplitFile(mockedFileContent);
        
        // Act & Assert
        Assert.Collection(courses,
            c => 
        {
            Assert.Equal("C# Programmeren", c.Title);
            Assert.Equal("CNETIN", c.CourseCode);
            Assert.Equal(5, c.Duration);
            Assert.Collection(c.CourseInstances, courseInstance =>
            {
                Assert.Equal(new DateTime(2021, 03, 22), courseInstance.StartDate);
            });
        },
            c =>
            {
                Assert.Equal("Blazor", c.Title);
                Assert.Equal("BLZ", c.CourseCode);
                Assert.Equal(2, c.Duration);
                Assert.Collection(c.CourseInstances, courseInstance =>
                {
                    Assert.Equal(new DateTime(2021, 01, 22), courseInstance.StartDate);
                });
            });
    }
    
    private static CourseFileSplitter CreateCourseFileSplitter()
    {
        var codeSplitter = new Mock<ICodeSplitter>();
        var durationSplitter = new Mock<IDurationSplitter>();
        var startDateSplitter = new Mock<IStartDateSplitter>();
        var titleSplitter = new Mock<ITitleSplitter>();
        var courseSplitter = new Mock<ICourseSplitter>();
        
        var mockedFileStringArray = new string[]
        {
            $"Titel: C# Programmeren\n" +
            $"Cursuscode: CNETIN\n" +
            $"Duur: 5 dagen\n" +
            $"Startdatum: 22/03/2021",

            $"Titel: Blazor\n" +
            $"Cursuscode: BLZ\n" +
            $"Duur: 2 dagen\n" +
            $"Startdatum: 22/01/2021"
        };
        
        courseSplitter.Setup(cs => cs.SplitCourse(It.IsAny<string>())).Returns((mockedFileStringArray));

        titleSplitter.Setup(ts => ts.SplitTitle("Titel: C# Programmeren")).Returns("C# Programmeren");
        titleSplitter.Setup(ts => ts.SplitTitle("Titel: Blazor")).Returns("Blazor");
        
        codeSplitter.Setup(cs => cs.SplitCode("Cursuscode: CNETIN")).Returns("CNETIN");
        codeSplitter.Setup(cs => cs.SplitCode("Cursuscode: BLZ")).Returns("BLZ");
        
        durationSplitter.Setup(ds => ds.SplitDuration("Duur: 5 dagen")).Returns(5);
        durationSplitter.Setup(ds => ds.SplitDuration("Duur: 2 dagen")).Returns(2);
        
        startDateSplitter.Setup(sd => sd.SplitStartDate("Startdatum: 22/03/2021")).Returns(new DateTime(2021, 03, 22));
        startDateSplitter.Setup(sd => sd.SplitStartDate("Startdatum: 22/01/2021")).Returns(new DateTime(2021, 01, 22));
        var courseFileSplitter = new CourseFileSplitter(codeSplitter.Object, durationSplitter.Object, startDateSplitter.Object, titleSplitter.Object, courseSplitter.Object);
        return courseFileSplitter;
    }

}