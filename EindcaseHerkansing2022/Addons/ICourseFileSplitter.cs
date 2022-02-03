using EindcaseHerkansing2022.Models;

namespace EindcaseHerkansing2022.Addons;

public interface ICourseFileSplitter
{ 
    List<Course> SplitFile(string fileContent);
}