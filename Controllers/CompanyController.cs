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
using mymvc1.Models.Company;
using Dapper;
using System.Data.SqlClient;

namespace mymvc1.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IConfiguration _configuration;

        public CompanyController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<IActionResult> Create()
        {
            var company = new CreateCompanyModel();
            return View(company);
        }
        [HttpPost]
        public IActionResult Create(CreateCompanyModel createCompanyModel)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            int newCompanyId;
            using(var connection = new SqlConnection(connectionString))
            {
                newCompanyId = connection.ExecuteScalar<int>(@"
                    INSERT INTO Companies(Name, Address, PhoneNumber)
                    OUTPUT INSERTED.CompanyId
                    VALUES(@Name, @Address, @PhoneNumber);
                ", createCompanyModel);
            }
            if(newCompanyId > 0)
                return RedirectToAction("Details", new {id = newCompanyId});
            return View(createCompanyModel);
        }
        public IActionResult Details(int id)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var company = new CreateCompanyModel();
            using(var connection = new SqlConnection(connectionString))
            {
                company = connection.QuerySingle<CreateCompanyModel>("SELECT * FROM Companies WHERE CompanyId = @CompanyId", new { CompanyId = id});
            }
            return View(company);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}