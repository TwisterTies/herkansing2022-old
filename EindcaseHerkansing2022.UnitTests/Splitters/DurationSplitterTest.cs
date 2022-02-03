using System;
using EindcaseHerkansing2022.Addons.FileSplitter;
using Xunit;

namespace EindcaseHerkansing2022.UnitTest.Splitters;

public class DurationSplitterTest
{
    [Fact]
    public void Split_ValidDuration_ReturnsDuration()
    {
        // Arrange
        string input = "Duur: 2 dagen";
        int expected = 2;
        DurationSplitter durationSplitter = new DurationSplitter();
        
        // Act
        var result = durationSplitter.SplitDuration(input);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Split_ValidDuration_ShouldThrowException()
    {
        // Arrange
        string input = "Duur: 2";
        DurationSplitter durationSplitter = new DurationSplitter();
        
        // Act & Assert
        Assert.Throws<Exception>(() => durationSplitter.SplitDuration(input));
    }
}