using EindcaseHerkansing2022.Addons.FileSplitter;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Splitters;

public class CourseSplitterTest
{
    [Fact]
    public void Should_Return_Correct_Number_Of_Courses()
    {
        // Arrange
        var fileContent = $@"Titel: Object Oriented Programming in C# By Example
Cursuscode: OOCS
Duur: 5 dagen
Startdatum: 22/03/2021

Titel: Object Oriented Programming in C# By Example
Cursuscode: OOCS
Duur: 5 dagen
Startdatum: 29/03/2021";
        
        var courseSplitter = new CourseSplitter();

        // Act
        var courses = courseSplitter.SplitCourse(fileContent);

        // Assert
        Assert.Equal(2, courses.Length);
    }
}