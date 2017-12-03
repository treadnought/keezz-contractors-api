using KeezzContractors.API.Models;
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
        private ILogger<ContractorInvoicesController> _logger;

        public ContractorInvoicesController(ILogger<ContractorInvoicesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetContractorInvoices(int contractorId)
        {
            try
            {
                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing contractor invoices.");
                    return NotFound();
                }

                return Ok(contractor.ContractorInvoices);
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
                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing contractor invoice with id {id}");
                    return NotFound();
                }

                var contractorInvoice = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == id);
                if (contractorInvoice == null) return NotFound();

                return Ok(contractorInvoice);
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

                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when creating contractor invoices.");
                    return NotFound();
                }

                if (contractor.ContractorInvoices.Any(i => i.ContractorInvRef == contractorInvoice.ContractorInvRef))
                {
                    ModelState.AddModelError("ContractorInvRef", $"Contractor Inv Ref {contractorInvoice.ContractorInvRef} for contractor {contractorId} is already in use");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation failed when creating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var finalContractorInvoice = new ContractorInvoiceDto()
                {
                    Id = 999999,
                    ContractorInvDate = contractorInvoice.ContractorInvDate,
                    ContractorInvRef = contractorInvoice.ContractorInvRef,
                    DaysBilled = contractorInvoice.DaysBilled,
                    ContractorInvNote = contractorInvoice.ContractorInvNote
                };

                contractor.ContractorInvoices.Add(finalContractorInvoice);

                return CreatedAtRoute("GetContractorInvoice", new
                {
                    contractorId = contractorId,
                    id = finalContractorInvoice.Id
                },
                finalContractorInvoice);

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
                    _logger.LogInformation($"Contractor invoice body for contractor with id {contractorId} not parsed when fully updating contractor invoice.");
                    return BadRequest();
                }

                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when fully updating contractor invoice with id {id}");
                    return NotFound();
                }

                if (contractor.ContractorInvoices.Any(i => i.ContractorInvRef == contractorInvoice.ContractorInvRef))
                {
                    ModelState.AddModelError("ContractorInvRef", $"Contractor Inv Ref {contractorInvoice.ContractorInvRef} for contractor {contractorId} is already in use");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model state invalid when updating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var contractorInvoiceFromStore = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == id);
                if (contractorInvoiceFromStore == null)
                {
                    _logger.LogInformation($"Contractor invoice with id {id} for contractor with id {contractorId} not found when fully updating.");
                    return NotFound();
                }

                contractorInvoiceFromStore.ContractorInvRef = contractorInvoice.ContractorInvRef;
                contractorInvoiceFromStore.ContractorInvDate = contractorInvoice.ContractorInvDate;
                contractorInvoiceFromStore.DaysBilled = contractorInvoice.DaysBilled;
                contractorInvoiceFromStore.ContractorInvNote = contractorInvoice.ContractorInvNote;

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

                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when partially updating contractor invoice with id {id}");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model state invalid when updating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var contractorInvoiceFromStore = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == id);
                if (contractorInvoiceFromStore == null)
                {
                    _logger.LogInformation($"Contractor invoice with id {id} for contractor with id {contractorId} not found when partially updating.");
                    return NotFound();
                }

                var contractorInvoiceToPatch =
                    new ContractorInvoiceForUpdateDto()
                    {
                        ContractorInvRef = contractorInvoiceFromStore.ContractorInvRef,
                        ContractorInvDate = contractorInvoiceFromStore.ContractorInvDate,
                        ContractorInvNote = contractorInvoiceFromStore.ContractorInvNote
                    };

                patchDoc.ApplyTo(contractorInvoiceToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation error when partially updating contractor invoice with id {id} for contractor with id {contractorId}.");
                    return BadRequest(ModelState);
                }

                contractorInvoiceFromStore.ContractorInvRef = contractorInvoiceToPatch.ContractorInvRef;
                contractorInvoiceFromStore.ContractorInvDate = contractorInvoiceToPatch.ContractorInvDate;
                contractorInvoiceFromStore.ContractorInvNote = contractorInvoiceToPatch.ContractorInvNote;

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while partially updating invoice with id {id} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }
    }
}
