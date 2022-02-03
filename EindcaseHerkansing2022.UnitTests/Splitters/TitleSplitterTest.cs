using System;
using EindcaseHerkansing2022.Addons.FileSplitter;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Splitters;

public class TitleSplitterTest
{
    [Fact]
    public void Split_ValidInput_ReturnsCorrectTitle()
    {
        // Arrange
        string input = "Titel: Azure Fundamentals";
        string expected = "Azure Fundamentals";
        TitleSplitter splitter = new TitleSplitter();

        // Act
        var result = splitter.SplitTitle(input);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TitleSplitter_ShouldThrowException_WhenInIncorrectFormat()
    {
        // Arrange
        string input = "Cursuscode: LINQ";
        TitleSplitter durationSplitter = new TitleSplitter();
        
        // Act & Assert
        Assert.Throws<Exception>(() => durationSplitter.SplitTitle(input));
    }
}