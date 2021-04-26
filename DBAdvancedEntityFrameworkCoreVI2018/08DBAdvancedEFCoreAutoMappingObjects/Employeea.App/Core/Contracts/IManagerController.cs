using Employees.App.Core.DTOs;

namespace Employees.App.Core.Contracts
{
    public interface IManagerController
    {
        string SetManager(int employeeId, int managerId);

        ManagerDto GetManagerInfo(int employeeId);
    }
}