using System.Text.RegularExpressions;
using EindcaseHerkansing2022.Addons.Interfaces;

namespace EindcaseHerkansing2022.Addons.FileSplitter;

public class TitleSplitter : ITitleSplitter
{
    private string? _title;
    private Regex _regex;

    public TitleSplitter()
    {
        _regex = new Regex("^(Titel: )*(.*?)$");
    }

    public string SplitTitle(string input)
    {
        if (!_regex.Match(input).Groups[1].Value.Contains("Titel: "))
        {
            throw new Exception("Titel is niet in het correcte formaat");
        }
        _title = _regex.Match(input).Groups[2].Value;
        return _title;
    }
}