using System.Globalization;
using System.Text.RegularExpressions;
using EindcaseHerkansing2022.Addons.Interfaces;

namespace EindcaseHerkansing2022.Addons.FileSplitter;

public class StartDateSplitter : IStartDateSplitter
{
    private string? _startDate;
    private Regex _regex;
    
    public StartDateSplitter()
    {
        _regex = new Regex("^(Startdatum: )*(.*?)$");
    }

    public DateTime SplitStartDate(string input)
    {
        if (!_regex.Match(input).Groups[1].Value.Contains("Startdatum: ") ||
            !_regex.Match(input).Groups[2].Value.Contains('/'))
        {
            throw new Exception("Startdatum is niet in het correcte formaat");
        }
        _startDate = _regex.Match(input).Groups[2].Value;
        CultureInfo dutchCultureInfo = new CultureInfo("nl-NL");
        return DateTime.Parse(_startDate, dutchCultureInfo);
    }
}