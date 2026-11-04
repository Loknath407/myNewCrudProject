using Microsoft.AspNetCore.Mvc;
using MyNewCrudProject.Models;
using MyNewCrudProject.Services;

namespace MyNewCrudProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // READ: List employees and search
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                employees = employees.Where(e =>
                    e.Name.Contains(searchString,
                        StringComparison.OrdinalIgnoreCase) ||
                    e.Email.Contains(searchString,
                        StringComparison.OrdinalIgnoreCase) ||
                    e.Department.Contains(searchString,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewData["SearchString"] = searchString;

            return View(employees);
        }

        // READ: Employee details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _service.GetByIdAsync(id.Value);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // CREATE: Display form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Save employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Email,Department")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // UPDATE: Display edit form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _service.GetByIdAsync(id.Value);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // UPDATE: Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,Email,Department")] Employee employee)
        {
            if (id != employee.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _service.ExistsAsync(employee.Id))
                    return NotFound();

                await _service.UpdateAsync(employee);

                return RedirectToAction(nameof(Index));
            }

            return view(employee);
        }

        // DELETE: Display confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _service.GetByIdAsync(id.Value);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // DELETE: Remove employee
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}