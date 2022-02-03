using System.ComponentModel.DataAnnotations;

namespace EindcaseHerkansing2022.Models;

public class CourseInstance
{
    [Key]
    public int InstanceId { get; set; }
    public int CourseId { get; set; }
    public DateTime StartDate { get; set; }
}