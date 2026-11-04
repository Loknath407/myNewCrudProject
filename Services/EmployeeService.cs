using MyNewCrudProject.Models;
using MyNewCrudProject.Repositories;

namespace MyNewCrudProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Employee>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task AddAsync(Employee employee)
        {
            return _repository.AddAsync(employee);
        }

        public Task UpdateAsync(Employee employee)
        {
            return _repository.UpdateAsync(employee);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _repository.ExistsAsync(id);
        }
    }
}