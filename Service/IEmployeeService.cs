using KendoUIApp2.Common;
using KendoUIApp2.DTO;

namespace KendoUIApp2.Service
{
    public interface IEmployeeService : CommonService<EmployeeDTO>
    {
        bool checkUserIdExist(string userId);
    }
}
