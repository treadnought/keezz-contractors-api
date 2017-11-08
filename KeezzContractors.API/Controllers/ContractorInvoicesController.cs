using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors")]
    public class ContractorInvoicesController : Controller
    {
        [HttpGet("{contractorId}/contractorinvoices")]
        public IActionResult GetContractorInvoices(int contractorId)
        {
            var contractor = 
                ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
            if (contractor == null) return NotFound();

            return Ok(contractor.ContractorInvoices);
        }

        [HttpGet("{contractorId}/contractorinvoices/{id}")]
        public IActionResult GetContractorInv(int contractorId, int id)
        {
            var contractor =
                ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
            if (contractor == null) return NotFound();

            var contractorInvoice = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == id);
            if (contractorInvoice == null) return NotFound();

            return Ok(contractorInvoice);
        }
    }
}
