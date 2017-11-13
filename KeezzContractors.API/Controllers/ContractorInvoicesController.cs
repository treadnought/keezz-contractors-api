using KeezzContractors.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors/{contractorId}/contractorinvoices")]
    public class ContractorInvoicesController : Controller
    {
        [HttpGet("")]
        public IActionResult GetContractorInvoices(int contractorId)
        {
            var contractor =
                ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
            if (contractor == null) return NotFound();

            return Ok(contractor.ContractorInvoices);
        }

        [HttpGet("{id}", Name = "GetContractorInvoice")]
        public IActionResult GetContractorInvoice(int contractorId, int id)
        {
            var contractor =
                ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
            if (contractor == null) return NotFound();

            var contractorInvoice = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == id);
            if (contractorInvoice == null) return NotFound();

            return Ok(contractorInvoice);
        }

        [HttpPost("")]
        public IActionResult CreateContractorInvoice(int contractorId,
            [FromBody] ContractorInvoiceForCreationDto contractorInvoice)
        {
            if (contractorInvoice == null) return BadRequest();

            var contractor =
                ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
            if (contractor == null) return NotFound();

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
    }
}
