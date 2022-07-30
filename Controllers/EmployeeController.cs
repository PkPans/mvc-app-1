using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mymvc1.Models;
using mymvc1.Models.Employee;
using mymvc1.Models.MyProfile;
using Dapper;
using System.Data.SqlClient;

namespace mymvc1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<IActionResult> Create()
        {
            var employee = new CreateEmployeeModel();
            return View(employee);
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeModel createEmployeeModel)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            int newEmployeeId;
            using(var connection = new SqlConnection(connectionString))
            {
                newEmployeeId = connection.ExecuteScalar<int>(@"
                    INSERT INTO Employees(FirstName, LastName, DateOfBirth, PhoneNumber)
                    OUTPUT INSERTED.EmployeeId
                    VALUES(@FirstName, @LastName, @DateOfBirth, @PhoneNumber);
                ", createEmployeeModel);
            }
            if(newEmployeeId > 0)
                return RedirectToAction("Details", new { employeeId = newEmployeeId});
            return View(createEmployeeModel);
        }
        public IActionResult Details(int employeeId)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var employee = new CreateEmployeeModel();
            using(var connection = new SqlConnection(connectionString))
            {
                employee = connection.QuerySingle<CreateEmployeeModel>("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId", new { EmployeeId = employeeId});
            }
            return View(employee);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int employeeId)
        {
            return View();
        }
        public IActionResult Delete(int employeeId)
        {
            return View();
        }
    }
}

//CRUD operations
//Create - Employee
//Read - Employee 
    //1) Read a SINGLE employee
    //2) Read ALL (or some) employees
//Update - Employee
//Delete - Employee
