using System;
using EindcaseHerkansing2022.Addons.FileSplitter;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Splitters;

public class StartDateSplitterTest
{
    [Fact]
    public void Split_ValidInput_ReturnsCorrectDate()
    {
        // Arrange
        var input = "Startdatum: 01/01/2020";
        StartDateSplitter splitter = new StartDateSplitter();
        var expected = new DateTime(2020, 1, 1);

        // Act
        var result = splitter.SplitStartDate(input);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void StartDateSplitter_ShouldThrowAnException()
    {
        // Arrange
        var input = "Startdatum: 01-01-2020";
        StartDateSplitter splitter = new StartDateSplitter();
        
        // Act & Assert
        Assert.Throws<Exception>(() => splitter.SplitStartDate(input));
    }
}