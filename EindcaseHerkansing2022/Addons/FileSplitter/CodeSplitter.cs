using System.Text.RegularExpressions;
using EindcaseHerkansing2022.Addons.Interfaces;

namespace EindcaseHerkansing2022.Addons.FileSplitter;

public class CodeSplitter : ICodeSplitter
{
    private string? _code;
    private Regex _regex;

    public CodeSplitter()
    {
        _regex = new Regex("^(Cursuscode: )*(.*?)$");
    }
    
    public string SplitCode(string input)
    {
        if (!_regex.Match(input).Groups[1].Value.Contains("Cursuscode: "))
        {
            throw new Exception("Cursuscode is niet in het correcte formaat");
        }
        _code = _regex.Match(input).Groups[2].Value;
        return _code;
    }
}