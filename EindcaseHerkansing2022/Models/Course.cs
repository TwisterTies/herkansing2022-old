using System.ComponentModel.DataAnnotations;

namespace EindcaseHerkansing2022.Models;

public class Course
{ 
    [Key]
    public int Id { get; set; }
    
    [MaxLength(300)]
    public string Title { get; set; }
    
    [MaxLength(10)]
    public string CourseCode { get; set; }
        
    public int Duration { get; set; }
    
    public virtual ICollection<CourseInstance> CourseInstances { get; set; }
}