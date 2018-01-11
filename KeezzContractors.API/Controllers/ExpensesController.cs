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

                //if (!_repository.ContractorInvoiceExists(contractorInvoiceId))
                //{
                //    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found when accessing expenses.");
                //    return NotFound();
                //}

                if (!_repository.ContractorInvoiceExistsForContractor(contractorId, contractorInvoiceId))
                {
                    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found for Contractor with id {contractorId} when accessing expenses.");
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

        [HttpGet("{id}", Name = "GetExpense")]
        public IActionResult GetExpense(int contractorId, int contractorInvoiceId, int id)
        {
            try
            {
                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when accessing expense with id {id}.");
                    return NotFound();
                }

                //if (!_repository.ContractorInvoiceExists(contractorInvoiceId))
                //{
                //    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found when accessing expense with id {id}.");
                //    return NotFound();
                //}

                if (!_repository.ContractorInvoiceExistsForContractor(contractorId, contractorInvoiceId))
                {
                    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found for Contractor with id {contractorId} when accessing expense with id {id}.");
                    return NotFound();
                }

                if (!_repository.ExpenseExistsForContractorInvoice(contractorInvoiceId, id))
                {
                    _logger.LogInformation($"Expense with id {id} not found for Contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId}.");
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
        }

        [HttpPost()]
        public IActionResult CreateExpense(int contractorId, int contractorInvoiceId, [FromBody] ExpenseForCreationDto expense)
        {
            try
            {
                if (expense == null)
                {
                    _logger.LogInformation($"Expense body for contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId} not parsed when creating contractor invoices.");
                    return BadRequest();
                }

                if (!_repository.ContractorExists(contractorId))
                {
                    _logger.LogInformation($"Contractor with id {contractorId} not found when creating expense for contractor invoice with id {contractorInvoiceId}.");
                    return NotFound();
                }

                if (!_repository.ContractorInvoiceExists(contractorInvoiceId))
                {
                    _logger.LogInformation($"Contractor invoice with id {contractorInvoiceId} not found when creating expense for contractor with id {contractorId}.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Validation failed when creating expense.");
                    return BadRequest(ModelState);
                }

                var finalExpense = Mapper.Map<Entities.Expense>(expense);

                _repository.AddExpense(contractorInvoiceId, finalExpense);

                if (!_repository.Save())
                {
                    _logger.LogInformation("Could not add expense.");
                    return StatusCode(500, "Could not add expense.");
                }

                var createdExpense = Mapper.Map<ExpenseDto>(finalExpense);

                return CreatedAtRoute("GetExpense", new
                {
                    contractorId = contractorId,
                    contractorInvoiceId = contractorInvoiceId,
                    id = createdExpense.Id
                },
                createdExpense);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while creating expense for contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId}.", ex);
                return StatusCode(500, "Exception thrown");
            }
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateExpense(int contractorId, int contractorInvoiceId, int expenseId,
        //    [FromBody] ExpenseForUpdateDto expense)
        //{
        //    try
        //    {
        //        if (expense == null)
        //        {
        //            _logger.LogInformation($"Expense body for contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId} not parsed when fully updating expense.");
        //            return BadRequest();
        //        }

        //        if (!_repository.ContractorExists(contractorId))
        //        {
        //            _logger.LogInformation($"Contractor with id {contractorId} not found when fully updating expense with id {expenseId}");
        //            return NotFound();
        //        }


        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogCritical($"Exception while fully updating expense with id {expenseId} for contractor invoice with id {contractorInvoiceId} for contractor with id {contractorId}.", ex);
        //        return StatusCode(500, "Exception thrown");
        //    }
        //}
    }
}
