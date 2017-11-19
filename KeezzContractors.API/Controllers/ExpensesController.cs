using KeezzContractors.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors/{contractorId}/contractorinvoices/{contractorInvoiceId}/expenses")]
    public class ExpensesController : Controller
    {
        private LocalMailService _mailService;
        private ILogger<ExpensesController> _logger;

        public ExpensesController(LocalMailService mailService, ILogger<ExpensesController> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetExpenses(int contractorId, int contractorInvoiceId)
        {
            var contractor = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId);
            if (contractor == null)
            {
                _logger.LogInformation($"Contractor with id {contractorId} not found when accessing expenses.");
                return NotFound();
            }

            var contractorInvoice = contractor.ContractorInvoices.FirstOrDefault(i => i.Id == contractorInvoiceId);
            if (contractorInvoice == null)
            {
                _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found when accessing expenses.");
                return NotFound();
            }

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
