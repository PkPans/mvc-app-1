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
            DisplayPetsModel pet1 = new DisplayPetsModel();
            pet1.PetName = "Mozart";
            pet1.PetType = "Dog";

            var pet2 = new DisplayPetsModel();
            pet2.PetName = "Pumpkin";
            pet2.PetType = "Bitch";

            List<DisplayPetsModel> list1 = new List<DisplayPetsModel>();
            list1.Add(pet1);
            list1.Add(pet2);

            return View(list1);

        }
    }
}

// create 2 pets objects, and store both peths in an array and in a list