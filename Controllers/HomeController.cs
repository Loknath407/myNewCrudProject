using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNewCrudProject.Data;

namespace MyNewCrudProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int employeeCount = await _context.Employees.CountAsync();

            ViewBag.EmployeeCount = employeeCount;

            return View();
        }
    }
}