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
    [Route("api/contractors/{contractorId}/contractorinvoices")]
    public class ContractorInvoicesController : Controller
    {
        private IKeezzContractorsRepository _repository;
        private IMailService _mailService;
        private ILogger<ContractorInvoicesController> _logger;

        public ContractorInvoicesController(
            IKeezzContractorsRepository repository, 
            IMailService mailService, 
            ILogger<ContractorInvoicesController> logger)
        {
            _repository = repository;
            _mailService = mailService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetContractorInvoices(int contractorId)
        {
            try
            {
                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing contractor invoices.");
                    return NotFound();
                }

                var contractorInvoices = _repository.GetContractorInvoices(contractorId);

                var contractorInvoicesResults = Mapper.Map<IEnumerable<ContractorInvoiceWithoutExpensesDto>>(contractorInvoices);

                return Ok(contractorInvoicesResults);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting invoices for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpGet("{id}", Name = "GetContractorInvoice")]
        public IActionResult GetContractorInvoice(int contractorId, int id)
        {
            try
            {
                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing contractor invoice.");
                    return NotFound();
                }

                var contractorInvoice = _repository.GetContractorInvoice(id);

                if (contractorInvoice == null)
                {
                    _logger.LogInformation($"Contractor invoice with id {id} not found.");
                    return NotFound();
                }

                var contractorInvoiceResult = Mapper.Map<ContractorInvoiceWithoutExpensesDto>(contractorInvoice);

                return Ok(contractorInvoiceResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting invoice with id {id} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpPost("")]
        public IActionResult CreateContractorInvoice(int contractorId,
            [FromBody] ContractorInvoiceForCreationDto contractorInvoice)
        {
            try
            {
                if (contractorInvoice == null)
                {
                    _logger.LogInformation($"Contractor invoice body for contractor with id {contractorId} not parsed when creating contractor invoices.");
                    return BadRequest();
                }

                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when creating contractor invoices.");
                    return NotFound();
                }

                var contractor = _repository.GetContractor(contractorId);

                if (contractor.ContractorInvoices.Any(i => i.ContractorInvRef == contractorInvoice.ContractorInvRef))
                {
                    ModelState.AddModelError("ContractorInvRef", $"Contractor Inv Ref {contractorInvoice.ContractorInvRef} for contractor {contractorId} is already in use");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation failed when creating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var finalContractorInvoice = Mapper.Map<Entities.ContractorInvoice>(contractorInvoice);

                _repository.AddInvoice(contractorId, finalContractorInvoice);

                if (!_repository.Save())
                {
                    _logger.LogInformation("Could not add invoice.");
                    return StatusCode(500, "Could not add invoice.");
                }

                var createdInvoice = Mapper.Map<ContractorInvoiceDto>(finalContractorInvoice);

                return CreatedAtRoute("GetContractorInvoice", new
                {
                    contractorId = contractorId,
                    id = createdInvoice.Id
                },
                createdInvoice);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while creating invoice for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContractorInvoice(int contractorId, int id,
            [FromBody] ContractorInvoiceForUpdateDto contractorInvoice)
        {
            try
            {
                if (contractorInvoice == null)
                {
                    _logger.LogInformation($"Contractor invoice body for contractor invoice with id {id} not parsed when fully updating contractor invoice.");
                    return BadRequest();
                }

                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when fully updating contractor invoice with id {id}");
                    return NotFound();
                }

                //if (contractor.ContractorInvoices.Any(i => i.ContractorInvRef == contractorInvoice.ContractorInvRef))
                //{
                //    ModelState.AddModelError("ContractorInvRef", $"Contractor Inv Ref {contractorInvoice.ContractorInvRef} for contractor {contractorId} is already in use");
                //}

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model state invalid when updating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var contractorInvoiceEntity = _repository.GetContractorInvoice(id);
                if (contractorInvoiceEntity == null)
                {
                    _logger.LogInformation($"Contractor invoice with id {id} for contractor with id {contractorId} not found when fully updating.");
                    return NotFound();
                }

                // replace entity values with those of passed in object
                Mapper.Map(contractorInvoice, contractorInvoiceEntity);

                if (!_repository.Save())
                {
                    return StatusCode(500, "Could not update invoice.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while updating invoice with id {id} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateContractorInvoice(int contractorId, int id,
            [FromBody] JsonPatchDocument<ContractorInvoiceForUpdateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    _logger.LogInformation($"Could not deserialise contractor invoice with id {id} for contractor {contractorId} when partially updating.");
                    return BadRequest();
                }

                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when partially updating contractor invoice with id {id}");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model state invalid when updating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var contractorInvoiceEntity = _repository.GetContractorInvoice(id);
                if (contractorInvoiceEntity == null)
                {
                    _logger.LogInformation($"Contractor invoice with id {id} not found when partially updating contractor invoice with id {id}");
                    return NotFound();
                }

                var contractorInvoiceToPatch = Mapper.Map<ContractorInvoiceForUpdateDto>(contractorInvoiceEntity);

                patchDoc.ApplyTo(contractorInvoiceToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation error when partially updating contractor invoice with id {id} for contractor with id {contractorId}.");
                    return BadRequest(ModelState);
                }

                TryValidateModel(contractorInvoiceToPatch);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation error when partially updating contractor invoice with id {id} for contractor with id {contractorId}.");
                    return BadRequest(ModelState);
                }

                // replace entity values with those of patched object
                Mapper.Map(contractorInvoiceToPatch, contractorInvoiceEntity);

                if (!_repository.Save())
                {
                    return StatusCode(500, "Could not update invoice.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while partially updating invoice with id {id} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContractorInvoice(int contractorId, int id)
        {
            if (!_repository.ContractorExists(contractorId))
            {
                _logger.LogInformation($"Contractor with id {id} not found when deleting contractor invoice.");
                return NotFound();
            }

            var contractorInvoiceEntity = _repository.GetContractorInvoice(id);
            if( contractorInvoiceEntity == null)
            {
                _logger.LogInformation($"Contractor invoice with id {id} for contractor with id {contractorId} not found when deleting.");
                return NotFound();
            }

            _repository.DeleteInvoice(contractorInvoiceEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "Could not delete invoice.");
            }

            _mailService.Send("Delete Delete", $"Contractor invoice with id {id} was deleted.");

            return NoContent();
        }
    }
}
