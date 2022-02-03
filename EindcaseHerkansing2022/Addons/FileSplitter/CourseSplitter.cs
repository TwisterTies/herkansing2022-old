using EindcaseHerkansing2022.Addons.Interfaces;

namespace EindcaseHerkansing2022.Addons.FileSplitter;

public class CourseSplitter : ICourseSplitter
{
    private string[]? _courses;

    public string[] SplitCourse(string input)
    {
        input = input.Replace("\r\n\r\n", "\n\n").Replace("\r\r", "\n\n");
        _courses = input.Split(new string[] { "\n\n" }, StringSplitOptions.None);
        return _courses;
    }
}