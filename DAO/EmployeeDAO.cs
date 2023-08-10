using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using KendoUIApp2.DBContex;
using KendoUIApp2.DTO;
using KendoUIApp2.Models;

namespace KendoUIApp2.DAO
{
    public class EmployeeDAO : IEmployeeDAO
    {
        private DBMain db = new DBMain();
        private DataTable dataTable;
        private Employee employee;

        public bool checkUserIdExist(string userId)
        {
            return db.executeFunc($"select dbo.func_checkUserIdExist('{userId}')");
        }
        public bool create(Employee employee)
        {
            bool status = false;
            string procName = "createEmployee";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("userId", employee.userId),
                new SqlParameter("username", employee.username),
                new SqlParameter("password", employee.password),
                new SqlParameter("email", employee.email),
                new SqlParameter("tel", employee.tel),
                new SqlParameter("disable", employee.disable)
            };
            status = db.executeProc(procName, CommandType.StoredProcedure, parameters);
            return status;
        }

        public bool delete(string id)
        {
            bool status = false;
            string procName = "delEmployee";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("userId", id),
            };
            status = db.executeProc(procName,CommandType.StoredProcedure, parameters);
            return status;
        }

        public Employee findById(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("params", id)
            };
            string query = $"func_finByUserId('{id}')";
            dataTable = db.loadData(query);
            Employee employee = new Employee();
            foreach (DataRow row in dataTable.Rows)
            {
                employee.userId = Convert.ToString(row["userId"]);
                employee.username = Convert.ToString(row["username"]);
                employee.email = Convert.ToString(row["email"]);
                employee.tel = Convert.ToString(row["tel"]);

            }
            return employee;
        }

        public List<Employee> getAll()
        {
            List<Employee> listEmployee = new List<Employee>();
            dataTable = db.loadData("EmployeeView");
            foreach (DataRow row in dataTable.Rows)
            {
                employee = new Employee();
                employee.userId = Convert.ToString(row["userId"]);
                employee.username = Convert.ToString(row["username"]);
                employee.email = Convert.ToString(row["email"]);
                employee.tel = Convert.ToString(row["tel"]);

                listEmployee.Add(employee);
            }
            return listEmployee;
        }

        public bool update(Employee employee)
        {
            bool status = false;
            string procName = "updateEmployee";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("userId", employee.userId),
                new SqlParameter("username", employee.username),
                new SqlParameter("email", employee.email),
                new SqlParameter("tel", employee.tel),
            };
            try
            {
                status = db.executeProc(procName, CommandType.StoredProcedure, parameters);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                status = false;
            }
            return status;
        }
    }
}
