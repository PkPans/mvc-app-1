using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mymvc1.Models;
using mymvc1.Models.MyProfile;

namespace mymvc1.Controllers
{
    public class MyProfileController : Controller
    {
        [HttpGet]// just for clarification
        public IActionResult Display()
        {
            var Person = new DisplayModel();
            Person.Name = "Gracia";
            Person.Location = "Fort Mill, SC";
            Person.Age = 34;
            Person.Profession = "student";
            Person.Hobbies = "jogging";
            Person.PhoneNumber = 9547162737;
            return View(Person);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var createModel = new CreateModel();
            return View(createModel);
        }
        [HttpPost]
        public IActionResult Create(CreateModel createModel)
        {
            return View();
        }
    }
}