using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeezzContractors.API.Entities;
using KeezzContractors.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KeezzContractors.API.Services
{
    public class KeezzContractorsRepository : IKeezzContractorsRepository
    {
        private KeezzContractorsContext _context;

        public KeezzContractorsRepository(KeezzContractorsContext context)
        {
            _context = context;
        }

        public void AddContractor(Contractor contractor)
        {
            _context.Contractors.Add(contractor);
        }

        public void AddExpense(int contractorInvoiceId, Expense expense)
        {
            var contractorInvoice = GetContractorInvoice(contractorInvoiceId);
            contractorInvoice.Expenses.Add(expense);
        }

        public void AddInvoice(int contractorId, ContractorInvoice contractorInvoice)
        {
            var contractor = GetContractor(contractorId);
            contractor.ContractorInvoices.Add(contractorInvoice);
        }

        public bool ContractorExists(int contractorId)
        {
            return _context.Contractors.Any(c => c.Id == contractorId);
        }

        public bool ContractorInvoiceExists(int contractorInvoiceId)
        {
            return _context.ContractorInvoices.Any(i => i.Id == contractorInvoiceId);
        }

        public void DeleteContractor(Contractor contractor)
        {
            _context.Remove(contractor);
        }

        public void DeleteInvoice(ContractorInvoice contractorInvoice)
        {
            _context.ContractorInvoices.Remove(contractorInvoice);
        }

        public Contractor GetContractor(int contractorId, bool includeContractorInvoices = false)
        {
            if (includeContractorInvoices)
            {
                return _context.Contractors.Include(c => c.ContractorInvoices)
                    .Where(c => c.Id == contractorId).FirstOrDefault();
            }

            return _context.Contractors.Where(c => c.Id == contractorId).FirstOrDefault();
        }

        public ContractorInvoice GetContractorInvoice(int contractorInvoiceId)
        {
            return _context.ContractorInvoices.Where(i => i.Id == contractorInvoiceId).FirstOrDefault();
        }

        public IEnumerable<ContractorInvoice> GetContractorInvoices(int contractorId)
        {
            return _context.ContractorInvoices.Where(i => i.ContractorId == contractorId)
                .OrderBy(i => i.ContractorInvDate).ToList();
        }

        public IEnumerable<Contractor> GetContractors()
        {
            return _context.Contractors.OrderBy(c => c.LastName).ToList();
        }

        public Expense GetExpense(int contractorInvoiceId, int expenseId)
        {
            return _context.Expenses
                .Where(e => e.ContractorInvoiceId == contractorInvoiceId && e.Id == expenseId).FirstOrDefault();
        }

        public IEnumerable<Expense> GetExpenses(int contractorInvoiceId)
        {
            return _context.Expenses.Where(e => e.ContractorInvoiceId == contractorInvoiceId).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
