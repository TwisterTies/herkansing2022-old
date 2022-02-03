using System;
using EindcaseHerkansing2022.Addons.FileSplitter;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Splitters;

public class CodeSplitterTest
{
    [Fact]
    public void CodeSplitter_Returns_Valid_Input()
    {
        // Arrange
        var input = "Cursuscode: LINQ";
        var expected = "LINQ";

        // Act
        CodeSplitter splitter = new CodeSplitter();
        var result = splitter.SplitCode(input);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void CodeSplitter_ShouldThrowException()
    {
        // Arrange
        string input = "Title; LINQ";
        CodeSplitter durationSplitter = new CodeSplitter();
        
        // Act & Assert
        Assert.Throws<Exception>(() => durationSplitter.SplitCode(input));
    }
}