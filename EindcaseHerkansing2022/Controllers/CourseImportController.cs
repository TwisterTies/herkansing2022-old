using EindcaseHerkansing2022.Interfaces.CourseImport;
using EindcaseHerkansing2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace EindcaseHerkansing2022.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseImportController : Controller
{
    private readonly ICourseImportService _courseImportService;
    
    public CourseImportController(ICourseImportService courseImportService)
    {
        _courseImportService = courseImportService;
    }
    
    [HttpPost]
    public async Task<CourseImportReply> ImportFile(IFormFile file)
    {
        return await _courseImportService.ImportCourses(file);
    }
}