using EindcaseHerkansing2022.Interfaces.Interfaces.CourseOverview;
using EindcaseHerkansing2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace EindcaseHerkansing2022.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseOverviewController : Controller
{
    private readonly ICourseOverviewService _courseOverviewService;
    
    public CourseOverviewController(ICourseOverviewService courseOverviewService)
    {
        _courseOverviewService = courseOverviewService;
    }

    [HttpGet]
    public async Task<IEnumerable<CourseOverview>> GetOverviewOfCourses()
    {
        return await _courseOverviewService.GetOverviewOfCourses();
    }
}