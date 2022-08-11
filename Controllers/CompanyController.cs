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
                    OUTPUT INSERTED.Id
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
                company = connection.QuerySingle<CreateCompanyModel>("SELECT * FROM Companies WHERE Id = @Id", new { Id = id});
            }
            return View(company);
        }
        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var companies = new List<DetailsCompanyModel>();
            using(var connection = new SqlConnection(connectionString))
            {
                companies = connection.Query<DetailsCompanyModel>("SELECT * FROM Companies").ToList();
            }
            return View(companies);
        }
        public IActionResult Edit(int id)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var company = new EditCompanyModel();
            using(var connection = new SqlConnection(connectionString))
            {
                company = connection.QuerySingle<EditCompanyModel>("SELECT * FROM Companies WHERE Id = @Id", new { Id = id});
            }
            return View(company);
        }
        [HttpPost]
        public IActionResult Edit(EditCompanyModel editCompanyModel)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            using(var connection = new SqlConnection(connectionString))
            {
                var sql = @"UPDATE Companies SET Name = @Name, 
                                        Address = @Address,
                                        PhoneNumber = @PhoneNumber
                                        WHERE Id = @Id";
                connection.Execute(sql, editCompanyModel);
            }
            return RedirectToAction("Details", new {id =  editCompanyModel.Id});
        }
        public IActionResult Delete(int id)
         {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var company = new DetailsCompanyModel();
            using(var connection = new SqlConnection(connectionString))
            {
                company = connection.QuerySingle<DetailsCompanyModel>("SELECT * FROM Companies WHERE Id = @Id", new { Id = id});
            }
            return View(company);
        }
        [HttpPost]
        public IActionResult Delete(DetailsCompanyModel detailsCompanyModel)
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            var company = new EditCompanyModel();
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Companies WHERE Id = @Id", new { Id = detailsCompanyModel.Id});
            }
            return RedirectToAction("Index");
        }
    }
}