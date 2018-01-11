using AutoMapper;
using KeezzContractors.API.Models;
using KeezzContractors.API.Services;
using Microsoft.AspNetCore.JsonPatch;
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
        private IMailService _mailService;

        public ContractorsController(IKeezzContractorsRepository repository, 
            ILogger<ContractorsController> logger,
            IMailService mailService)
        {
            _repository = repository;
            _logger = logger;
            _mailService = mailService;
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
                    var contractorResult = Mapper.Map<ContractorWithInvoicesWithoutExpensesDto>(contractor);

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

        [HttpPut("{contractorId}")]
        public IActionResult UpdateContractor(int contractorId,
            [FromBody] ContractorForUpdateDto contractor)
        {
            try
            {
                if (contractor == null)
                {
                    _logger.LogInformation($"Could not serialise input when attempting to fully update contractor with id {contractorId}.");
                    return BadRequest();
                }

                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when attemptint full update.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation failed when attempting to fully update contractor with id {contractorId}.");
                    return BadRequest();
                }

                var contractorEntity = _repository.GetContractor(contractorId);

                // replace entity values with those of passed in object
                Mapper.Map(contractor, contractorEntity);

                if (!_repository.Save())
                {
                    return StatusCode(500, "Could not update contractor.");
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while updating contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpPatch("{contractorId}")]
        public IActionResult PartiallyUpdateContractor(int contractorId,
            [FromBody] JsonPatchDocument<ContractorForUpdateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    _logger.LogInformation($"Could not deserialise contractor with id {contractorId} when partially updating.");
                    return BadRequest();
                }

                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when partially updating.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model state invalid when updating contractor with id {contractorId}.");
                    return BadRequest(ModelState);
                }

                var contractorEntity = _repository.GetContractor(contractorId);

                var contractorToPatch = Mapper.Map<ContractorForUpdateDto>(contractorEntity);

                patchDoc.ApplyTo(contractorToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation error when partially updating contractor with id {contractorId}.");
                    return BadRequest(ModelState);
                }

                TryValidateModel(contractorToPatch);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation error when partially updating contractor with id {contractorId}.");
                    return BadRequest(ModelState);
                }

                // replace entity values with those of patched object
                Mapper.Map(contractorToPatch, contractorEntity);

                if (!_repository.Save())
                {
                    return StatusCode(500, "Could not update contractor.");
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while partially updating contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpDelete("{contractorId}")]
        public IActionResult DeleteContractor(int contractorId)
        {
            if (!_repository.ContractorExists(contractorId))
            {
                _logger.LogInformation($"Contractor with id {contractorId} not found when deleting contractor.");
                return NotFound();
            }

            var contractorEntity = _repository.GetContractor(contractorId);

            _repository.DeleteContractor(contractorEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "Could not delete contract.");
            }

            _mailService.Send("Delete Delete", $"Contractor with id {contractorId} was deleted.");

            return NoContent();
        }
    }
}
