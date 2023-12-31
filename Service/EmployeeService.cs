﻿using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using KendoUIApp2.DAO;
using KendoUIApp2.DBContex;
using KendoUIApp2.DTO;
using KendoUIApp2.Models;

namespace KendoUIApp2.Service
{
    public class EmployeeService : IEmployeeService
    {
        private EmployeeDAO employeeDAO = new EmployeeDAO();

        public bool checkUserIdExist(string userId)
        {
            return employeeDAO.checkUserIdExist(userId);
        }
        public bool create(EmployeeCreateDTO employeeDTO)
        {
            Employee employee = new Employee();
            employee.userId = employeeDTO.userId.ToUpper() ;
            employee.username = employeeDTO.username;
            employee.email = employeeDTO.email;
            employee.password = employeeDTO.password;
            employee.tel = employeeDTO.tel;
            employee.disable = 0;

            return employeeDAO.create(employee);
        }

        public bool delete(string id)
        {
            return employeeDAO.delete(id);
        }

        public EmployeeCreateDTO findById(string id)
        {
            Employee employee = employeeDAO.findById(id);
            EmployeeCreateDTO employeeDTO = new EmployeeCreateDTO();
            employeeDTO.userId = employee.userId;
            employeeDTO.username = employee.username;
            employeeDTO.email = employee.email;
            employeeDTO.password = employee.password;
            employeeDTO.tel = employee.tel;

            return employeeDTO;
        }

        public List<EmployeeCreateDTO> getAll()
        {
            List<EmployeeCreateDTO> listEmployeeDTO = new List<EmployeeCreateDTO>();
            List<Employee> listEmployee = new List<Employee>();
            listEmployee = employeeDAO.getAll();

            foreach(Employee item in listEmployee)
            {
                EmployeeCreateDTO employeeDTO= new EmployeeCreateDTO();
                employeeDTO.userId = item.userId;
                employeeDTO.username = item.username;
                employeeDTO.password = item.password;
                employeeDTO.email = item.email;
                employeeDTO.tel = item.tel;

                listEmployeeDTO.Add(employeeDTO);
            }
            return listEmployeeDTO;
        }

        public bool update(EmployeeUpdateDTO employeeDTO)
        {
            Employee employee = new Employee();
            employee.userId = employeeDTO.userId;
            employee.username = employeeDTO.username;
            employee.email = employeeDTO.email;
            employee.tel = employeeDTO.tel;
            employee.disable = 0;

            return employeeDAO.update(employee);
        }
    }
}
