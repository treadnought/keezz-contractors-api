using AutoMapper;
using KeezzContractors.API.Models;
using KeezzContractors.API.Services;
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
        private IKeezzContractorsRepository _repository;
        private ILogger<ContractorsController> _logger;

        public ContractorsController(IKeezzContractorsRepository repository, ILogger<ContractorsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetContractors()
        {
            try
            {
                var contractorEntities =_repository.GetContractors();

                var results = Mapper.Map<IEnumerable<ContractorWithoutInvoicesDto>>(contractorEntities);

                return Ok(results);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting contractors.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpGet("{id}", Name = "GetContractor")]
        public IActionResult GetContractor(int id, bool includeInvoices = false)
        {
            try
            {
                var contractor = _repository.GetContractor(id, includeInvoices);

                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {id} not found when getting contractor.");
                    return NotFound();
                }

                if (includeInvoices)
                {
                    var contractorResult = Mapper.Map<ContractorDto>(contractor);

                    return Ok(contractorResult);
                }

                var contractorWithoutInvoicesResult = Mapper.Map<ContractorWithoutInvoicesDto>(contractor);

                return Ok(contractorWithoutInvoicesResult);
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

                var finalContractor = Mapper.Map<Entities.Contractor>(contractor);

                _repository.AddContractor(finalContractor);

                if (!_repository.Save())
                {
                    _logger.LogInformation("Could not add contractor.");
                    return StatusCode(500, "Could not add contractor.");
                }

                var createdContractor = Mapper.Map<ContractorDto>(finalContractor);

                return CreatedAtRoute("GetContractor", new { id = createdContractor.Id }, createdContractor);
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Exception while creating contractor.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }
    }
}
