using EindcaseHerkansing2022.Models;
using EindcaseHerkansing2022.Addons.Interfaces;

namespace EindcaseHerkansing2022.Addons;

public class CourseFileSplitter : ICourseFileSplitter
{
    private readonly ICodeSplitter _codeSplitter;
    private readonly IDurationSplitter _durationSplitter;
    private readonly IStartDateSplitter _startDateSplitter;
    private readonly ITitleSplitter _titleSplitter;
    private readonly ICourseSplitter _courseSplitter;

    public CourseFileSplitter(ICodeSplitter codeSplitter, IDurationSplitter durationSplitter,
        IStartDateSplitter startDateSplitter, ITitleSplitter titleSplitter, ICourseSplitter courseSplitter)
    {
        _codeSplitter = codeSplitter;
        _durationSplitter = durationSplitter;
        _startDateSplitter = startDateSplitter;
        _titleSplitter = titleSplitter;
        _courseSplitter = courseSplitter;
    }

    public List<Course> SplitFile(string fileContent)
    {
        var courseArray = _courseSplitter.SplitCourse(fileContent).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var courses = new List<Course>();
        int fileToUploadLine = 0;
        foreach (var courseInArray in courseArray)
        {
            try
            {
                var lines = courseInArray.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                
                fileToUploadLine += 1;
                var title = _titleSplitter.SplitTitle(lines[0]);
                
                fileToUploadLine += 1;
                var code = _codeSplitter.SplitCode(lines[1]);
               
                fileToUploadLine += 1;
                var duration = _durationSplitter.SplitDuration(lines[2]);
                
                fileToUploadLine += 1;
                var startDate = _startDateSplitter.SplitStartDate(lines[3]);
                
                fileToUploadLine += 1;
                if (lines.Length >= 5 && !string.IsNullOrEmpty(lines[4]))
                {
                    throw new Exception("Cursussen zijn onjuist gescheiden");
                }
                
                var existingCourse =
                    courses.FirstOrDefault(x => x.Title == title && x.CourseCode == code && x.Duration == duration);
                if (existingCourse != null)
                {
                    var existingCourseInstance = new CourseInstance()
                    {
                        StartDate = startDate
                    };
                    existingCourse.CourseInstances.Add(existingCourseInstance);
                }
                else
                {
                    var newCourse = new Course()
                    {
                        Title = title,
                        CourseCode = code,
                        Duration = duration,
                        CourseInstances = new List<CourseInstance>()
                    };
                    var courseInstance = new CourseInstance()
                    {
                        StartDate = startDate
                    };
                    newCourse.CourseInstances.Add(courseInstance);

                    courses.Add(newCourse);
                }
            }
            catch(Exception e)
            {
               throw new Exception(e.Message + " op regel " + fileToUploadLine);
            }
        }
        return courses;
    }
}