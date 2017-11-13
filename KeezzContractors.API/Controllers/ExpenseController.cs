using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors/{contractorId}/contractorinvoices/{contractorInvoiceId}/expenses")]
    public class ExpenseController : Controller
    {
        [HttpGet("")]
        public IActionResult GetExpenses(int contractorId, int contractorInvoiceId)
        {
            var contractorInvoice = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId)
                .ContractorInvoices.FirstOrDefault(i => i.Id == contractorInvoiceId);
            if (contractorInvoice == null) return NotFound();

            return Ok(contractorInvoice.Expenses);
        }

        [HttpGet("{id}")]
        public IActionResult GetExpense(int contractorId, int contractorInvoiceId, int id)
        {
            var expense = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId)
                .ContractorInvoices.FirstOrDefault(i => i.Id == contractorInvoiceId)
                .Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null) return NotFound();

            return Ok(expense);
        }
    }
}
