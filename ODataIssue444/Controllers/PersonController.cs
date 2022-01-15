using System;
using System.Collections.Generic;
using ODataIssue444.Data.Entities;
using Extenso.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ODataIssue444.Controllers
{
    [Route("people")]
    public class PersonController : Controller
    {
        private readonly IRepository<Person> personRepository;

        public PersonController(IRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            if (personRepository.Count() == 0)
            {
                // Populate for testing purposes

                var people = new List<Person>
                {
                    new Person { FamilyName = "Jordan", GivenNames = "Michael", DateOfBirth = new DateTime(1963, 2, 17) },
                    new Person { FamilyName = "Johnson", GivenNames = "Dwayne", DateOfBirth = new DateTime(1972, 5, 2) },
                    new Person { FamilyName = "Froning", GivenNames = "Rich", DateOfBirth = new DateTime(1987, 7, 21) }
                };

                personRepository.Insert(people);
            }

            return View();
        }
    }
}