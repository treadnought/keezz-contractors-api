using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors")]
    public class ExpenseController : Controller
    {
        [HttpGet("{contractorId}/contractorinvoices/{contractorInvoiceId}/expenses")]
        public IActionResult GetExpenses(int contractorId, int contractorInvoiceId)
        {
            var contractorInvoice = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId)
                .ContractorInvoices.FirstOrDefault(i => i.Id == contractorInvoiceId);
            if (contractorInvoice == null) return NotFound();

            return Ok(contractorInvoice.Expenses);
        }
    }
}
