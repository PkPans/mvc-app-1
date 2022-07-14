using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mymvc1.Models;
using mymvc1.Models.ViewPets;

namespace mymvc1.Controllers
{
    public class ViewPetsController : Controller
    {
       public IActionResult DisplayPets()
       {
            var Pet = new DisplayPetsModel();
            Pet.PetName = "Jake";
            Pet.PetType = "dog";
            Pet.PetColor = "white";
            Pet.PetGender = "Male";
            Pet.PetAge = 5;
            return View(Pet);
       } 
    }
}