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
       public IActionResult DisplayMultiplePets()
       {
            var pets2 = new List<DisplayPetsModel>();
            
            var pet1 = new DisplayPetsModel();
            pet1.PetName = "Jake";
            pet1.PetType = "dog";
            
            var pet2 = new DisplayPetsModel();
            pet2.PetName = "Bella";
            pet2.PetType = "cat";
            
            pets2.Add(pet1);
            pets2.Add(pet2);

            return View(pets2);
       } 
    }
}