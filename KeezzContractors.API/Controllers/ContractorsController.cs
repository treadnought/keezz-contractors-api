using KeezzContractors.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors")]
    public class ContractorsController : Controller
    {
        [HttpGet()]
        public IActionResult GetContractors()
        {
            return Ok(ContractorsDataStore.Current.Contractors);
        }

        [HttpGet("{id}", Name = "GetContractor")]
        public IActionResult GetContractor(int id)
        {
            var contractorToReturn = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == id);
            if (contractorToReturn == null) return NotFound();

            return Ok(contractorToReturn);
        }

        [HttpPost()]
        public IActionResult CreateContractor([FromBody] ContractorForCreationDto contractor)
        {
            if (contractor == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var finalContractor = new ContractorDto()
            {
                Id = 9999,
                FirstName = contractor.FirstName,
                LastName = contractor.LastName,
                Inactive = contractor.Inactive
            };

            return CreatedAtRoute("GetContractor", new { id = finalContractor.Id }, finalContractor);
        }
    }
}
