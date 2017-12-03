using KeezzContractors.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors")]
    public class ContractorsController : Controller
    {
        private ILogger<ContractorsController> _logger;

        public ContractorsController(ILogger<ContractorsController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetContractors()
        {
            try
            {
                return Ok(ContractorsDataStore.Current.Contractors);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting contractors.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpGet("{id}", Name = "GetContractor")]
        public IActionResult GetContractor(int id)
        {
            try
            {
                var contractorToReturn = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == id);
                if (contractorToReturn == null)
                {
                    _logger.LogInformation($"Contractor with id {id} not found.");
                    return NotFound();
                }

                return Ok(contractorToReturn);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting contractor with id {id}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpPost()]
        public IActionResult CreateContractor([FromBody] ContractorForCreationDto contractor)
        {
            try
            {
                if (contractor == null)
                {
                    _logger.LogInformation("Could not serialise input when attempting to create contractor.");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Validation failed when attempting to create contractor.");
                    return BadRequest();
                }

                var finalContractor = new ContractorDto()
                {
                    Id = 9999,
                    FirstName = contractor.FirstName,
                    LastName = contractor.LastName,
                    Inactive = contractor.Inactive
                };

                ContractorsDataStore.Current.Contractors.Add(finalContractor);

                return CreatedAtRoute("GetContractor", new { id = finalContractor.Id }, finalContractor);
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Exception while creating contractor.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }
    }
}
