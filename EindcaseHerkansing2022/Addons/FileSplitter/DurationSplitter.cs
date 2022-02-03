using System.Text.RegularExpressions;
using EindcaseHerkansing2022.Addons.Interfaces;

namespace EindcaseHerkansing2022.Addons.FileSplitter;

public class DurationSplitter : IDurationSplitter
{
    private Regex _regex;
    private int _duration;

    public DurationSplitter()
    {
        _regex = new Regex("^(Duur: )*(.*?)( dagen)*$");
    }

    public int SplitDuration(string input)
    {
        if (!_regex.Match(input).Groups[3].Value.Contains(" dagen") ||
            !_regex.Match(input).Groups[1].Value.Contains("Duur: "))
        {
            throw new Exception("Duur is niet in het correcte formaat");
        }
        var match = _regex.Match(input);
        _duration = int.Parse(match.Groups[2].Value);
        return _duration;
    }
}