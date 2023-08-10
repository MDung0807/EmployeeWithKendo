using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoUIApp2.Models;
using KendoUIApp2.DTO;
using KendoUIApp2.Service;
using Telerik.SvgIcons;
using System.Web.Services.Description;

namespace KendoUIApp2.Controllers
{
    public class HomeController : Controller
    {

        private IEmployeeService employeeService = new EmployeeService();
        private EmployeeDTO employeeDTO = new EmployeeDTO();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult checkUserIdExist([DataSourceRequest] DataSourceRequest requesst, string userId)
        {
            string status = employeeService.checkUserIdExist(userId).ToString().ToUpper();
            return Json(new {Data = status}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Select([DataSourceRequest] DataSourceRequest request)
        {
            var data = Enumerable.Range(1, 10)
                .Select(index => new Product
                {
                    ProductID = index,
                    ProductName = "Product #" + index,
                    UnitPrice = index * 10,
                    Discontinued = false
                });

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            employeeDTOs = employeeService.getAll();

            return Json(employeeDTOs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult delete([DataSourceRequest] DataSourceRequest request, string userId)
        {
            bool status = employeeService.delete(userId);
            return Json(status, JsonRequestBehavior.AllowGet);
        }


        public ActionResult update([DataSourceRequest] DataSourceRequest request, EmployeeDTO employeeDTO)
        {
            bool status = false;
            if(ModelState.IsValid)
                status = employeeService.update(employeeDTO);
            return Json(status, JsonRequestBehavior.AllowGet);
        }   


        public ActionResult create([DataSourceRequest]DataSourceRequest request, EmployeeDTO employeeDTO)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                status = employeeService.create(employeeDTO);
            }
            return Json(status);
        }
        public ActionResult findById([DataSourceRequest] DataSourceRequest request, string userId) {

            employeeDTO = employeeService.findById(userId);
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            employeeDTOs.Add(employeeDTO);
            return Json(employeeDTOs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

    }
}