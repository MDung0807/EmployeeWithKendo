using KendoUIApp2.Common;
using KendoUIApp2.Models;

namespace KendoUIApp2.DAO
{
    public interface IEmployeeDAO:CommonRepository<Employee>
    {
        bool checkUserIdExist(string userId);
    }
}
