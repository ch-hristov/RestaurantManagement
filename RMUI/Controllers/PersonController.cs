using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonController : Controller
    {
        private readonly IPersonData _data;

        public PersonController(IPersonData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Insert Employee into database
        public async Task<IActionResult> InsertPerson(PersonDisplayModel person)
        {
            if (ModelState.IsValid)
            {
                PersonModel newPerson = new PersonModel
                {
                    EmployeeID = person.EmployeeID,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    EmailAddress = person.EmailAddress,
                    CellPhoneNumber = person.CellPhoneNumber
                };

                await _data.InsertPerson(newPerson);

                return RedirectToAction("ViewPersons");
            }

            return View();
        }


        // View all employees as as list
        public async Task<IActionResult> ViewPersons()
        {
            var results = await _data.GetAllPeople();

            List<PersonDisplayModel> allEmployees = new List<PersonDisplayModel>();

            foreach (var employee in results)
            {
                allEmployees.Add(new PersonDisplayModel
                {
                    Id = employee.Id,
                    EmployeeID = employee.EmployeeID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    EmailAddress = employee.EmailAddress,
                    CellPhoneNumber = employee.CellPhoneNumber
                });
            }

            return View(allEmployees);
        }


        // Edit employee with database Id = id
        public async Task<IActionResult> EditPerson(int id)
        {
            PersonModel foundPerson = await _data.GetPersonById(id);

            PersonDisplayModel person = new PersonDisplayModel
            {
                Id = foundPerson.Id,
                EmployeeID = foundPerson.EmployeeID,
                FirstName = foundPerson.FirstName,
                LastName = foundPerson.LastName,
                EmailAddress = foundPerson.EmailAddress,
                CellPhoneNumber = foundPerson.CellPhoneNumber
            };

            return View(person);
        }


        // Update employee info
        public async Task<IActionResult> UpdatePerson(PersonDisplayModel person)
        {
            PersonModel updatedPerson = new PersonModel
            {
                Id = person.Id,
                EmployeeID = person.EmployeeID,
                FirstName = person.FirstName,
                LastName = person.LastName,
                EmailAddress = person.EmailAddress,
                CellPhoneNumber = person.CellPhoneNumber
            };

            await _data.UpdatePerson(updatedPerson);

            return RedirectToAction("ViewPersons");
        }



        // Delete employee with database Id = id
        public async Task<IActionResult> DeletePerson(int id)
        {
            await _data.DeletePerson(id);

            return RedirectToAction("ViewPersons");
        }
    }
}