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
    [Route("api/contractors/{contractorId}/contractorinvoices/{contractorInvoiceId}/expenses")]
    public class ExpensesController : Controller
    {
        private IKeezzContractorsRepository _repository;
        private IMailService _mailService;
        private ILogger<ExpensesController> _logger;

        public ExpensesController(IKeezzContractorsRepository repository, 
            IMailService mailService, 
            ILogger<ExpensesController> logger)
        {
            _repository = repository;
            _mailService = mailService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetExpenses(int contractorId, int contractorInvoiceId)
        {
            try
            {
                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing expenses.");
                    return NotFound();
                }

                if (!_repository.ContractorInvoiceExists(contractorInvoiceId))
                {
                    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found when accessing expenses.");
                    return NotFound();
                }

                var expenses = _repository.GetExpenses(contractorInvoiceId);

                var expensesResults = Mapper.Map<IEnumerable<ExpenseDto>>(expenses);

                return Ok(expensesResults);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting expenses for contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetExpense(int contractorId, int contractorInvoiceId, int id)
        {
            try
            {
                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing expense with id {id}.");
                    return NotFound();
                }

                if (!_repository.ContractorInvoiceExists(contractorInvoiceId))
                {
                    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found when accessing expense with id {id}.");
                    return NotFound();
                }

                var expense = _repository.GetExpense(id);

                if (expense == null)
                {
                    _logger.LogInformation($"Expense with id {id} not found.");
                    return NotFound();
                }

                var expenseResult = Mapper.Map<ExpenseDto>(expense);

                return Ok(expenseResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting expense with id {id} for contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }

            //var expense = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == contractorId)
            //    .ContractorInvoices.FirstOrDefault(i => i.Id == contractorInvoiceId)
            //    .Expenses.FirstOrDefault(e => e.Id == id);
            //if (expense == null) return NotFound();

            //return Ok(expense);
        }
    }
}
