using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.Response;
using Time_tracking_system.Domain.ViewModels;

namespace Time_tracking_system.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IBaseResponse<List<EmployeeVM>>> GetAllEmployees();
        Task<IBaseResponse<List<EmployeeVM>>> GetEditEmployees(Employee authorizedEmployee);
        Task<IBaseResponse<bool>> EditEmployee(EditEmployeeVM model, bool currentUserRoleIsAdmin);
        Task<IBaseResponse<List<IdentityRole>>> GetAllRoles();
        Task<IBaseResponse<EditEmployeeVM>> GetEmployeeAndRole(string id);
        Task<IBaseResponse<List<EmployeeVM>>> GetEmployeesInCalendar(Employee authorizedEmployee);

    }
}
