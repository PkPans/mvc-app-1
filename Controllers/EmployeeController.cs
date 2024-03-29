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
using mymvc1.Models.Company;

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
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            using(var connection = new SqlConnection(connectionString))
            {
                employee.AllCompanies = connection.Query<DetailsCompanyModel>("SELECT * FROM Companies").ToList();
                employee.AllCompanies.Add(new DetailsCompanyModel{ Name = "", Id = -1});
                employee.AllCompanies = employee.AllCompanies.OrderBy(c => c.Id).ToList();
            }
                       
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
                    INSERT INTO Employees(FirstName, LastName, DateOfBirth, PhoneNumber, CompanyId)
                    OUTPUT INSERTED.Id
                    VALUES(@FirstName, @LastName, @DateOfBirth, @PhoneNumber, @CompanyId);
                ", createEmployeeModel);
               
            }
            if(newEmployeeId > 0)
                return RedirectToAction("Details", new { id = newEmployeeId});
            return View(createEmployeeModel);
        }
        public IActionResult Details(int id)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var employee = new DetailsEmployeeModel();
            using(var connection = new SqlConnection(connectionString))
            {
                employee = connection.QuerySingle<DetailsEmployeeModel>(@"SELECT e.*, c.[Name] as CompanyName
                                                                            FROM Employees e 
                                                                            INNER JOIN Companies c ON e.CompanyId = c.Id
                                                                            WHERE e.Id = @Id", new { Id = id});
            }
            return View(employee);
        }
        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var employees = new List<DetailsEmployeeModel>();
            using(var connection = new SqlConnection(connectionString))
            {
                employees = connection.Query<DetailsEmployeeModel>(@"SELECT e.*, c.[Name] as CompanyName
                                                                            FROM Employees e 
                                                                            INNER JOIN Companies c ON e.CompanyId = c.Id").ToList();
            }
            return View(employees);
        }
        public IActionResult Edit(int id)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var employee = new EditEmployeeModel();
            employee.AllCompanies = new List<DetailsCompanyModel>();
            using(var connection = new SqlConnection(connectionString))
            {
                employee = connection.QuerySingle<EditEmployeeModel>("SELECT * FROM Employees WHERE Id = @Id", new { Id = id});
                employee.AllCompanies = connection.Query<DetailsCompanyModel>("SELECT * FROM Companies").ToList();
                employee.AllCompanies.Add(new DetailsCompanyModel{ Name = "", Id = -1});
                employee.AllCompanies = employee.AllCompanies.OrderBy(c => c.Id).ToList();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(EditEmployeeModel editEmployeeModel)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            using(var connection = new SqlConnection(connectionString))
            {
                var sql = @"UPDATE Employees SET FirstName = @FirstName, 
                                        LastName = @LastName,
                                        DateOfBirth = @DateOfBirth,
                                        PhoneNumber = @PhoneNumber,
                                        CompanyId = @CompanyId
                                        WHERE Id = @Id";
                connection.Execute(sql, editEmployeeModel);
            }
            return RedirectToAction("Details", new {id =  editEmployeeModel.Id});
        }
        public IActionResult Delete(int id)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var employee = new DetailsEmployeeModel();
            using(var connection = new SqlConnection(connectionString))
            {
                employee = connection.QuerySingle<DetailsEmployeeModel>("SELECT * FROM Employees WHERE Id = @Id", new { Id = id});
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(DetailsEmployeeModel detailsEmployeeModel)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var employee = new EditEmployeeModel();
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Employees WHERE Id = @Id", new { Id = detailsEmployeeModel.Id});
            }
            return RedirectToAction("Index");
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
