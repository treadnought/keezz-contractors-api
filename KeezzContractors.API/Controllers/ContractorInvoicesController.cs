using KeezzContractors.API.Models;
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

                if (!ModelState.IsValid) return BadRequest(ModelState);

                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null)
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when creating contractor invoices.");
                    return NotFound();
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
            catch
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
                    _logger.LogInformation($"Contractor invoice with id {id} not found when updating.");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model state invalid when updating contractor invoice.");
                    return BadRequest(ModelState);
                }

                var contractor =
                    ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
                if (contractor == null) return NotFound();

                var contractorInvoiceFromStore = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == id);
                if (contractorInvoiceFromStore == null) return NotFound();

                contractorInvoiceFromStore.ContractorInvRef = contractorInvoice.ContractorInvRef;
                contractorInvoiceFromStore.ContractorInvDate = contractorInvoice.ContractorInvDate;
                contractorInvoiceFromStore.DaysBilled = contractorInvoice.DaysBilled;
                contractorInvoiceFromStore.ContractorInvNote = contractorInvoice.ContractorInvNote;

                return NoContent();
            }
            catch
            {
                _logger.LogCritical($"Exception while updating invoice with id {id} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }
    }
}
