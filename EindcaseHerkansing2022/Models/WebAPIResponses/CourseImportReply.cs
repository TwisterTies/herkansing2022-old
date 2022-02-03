namespace EindcaseHerkansing2022.Models;

public class CourseImportReply
{
    public int AmountOfCoursesImported { get; set; }
    public int AmountOfCourseInstancesImported { get; set; }
    public int AmountOfDuplicateCourses { get; set; }
    public int AmountOfDuplicateCourseInstances { get; set; }
    
    public string ErrorMessage { get; set; }
}