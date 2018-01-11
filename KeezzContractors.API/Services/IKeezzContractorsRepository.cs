using KeezzContractors.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Services
{
    public interface IKeezzContractorsRepository
    {
        bool ContractorExists(int contractorId);
        bool ContractorInvoiceExists(int contractorInvoiceId);
        bool ContractorInvoiceExistsForContractor(int contractorId, int contractorInvoiceId);
        bool ExpenseExistsForContractorInvoice(int contractorInvoiceId, int expenseId);
        IEnumerable<Contractor>GetContractors();
        Contractor GetContractor(int contractorId, bool includeContractorInvoices = false);
        IEnumerable<ContractorInvoice> GetContractorInvoices(int contractorId);
        ContractorInvoice GetContractorInvoiceForContractor(int contractorId, int id);
        ContractorInvoice GetContractorInvoice(int contractorInvoiceId);
        IEnumerable<Expense> GetExpenses(int contractorInvoiceId);
        Expense GetExpenseForInvoice(int contractorInvoiceId, int id);
        Expense GetExpense(int expenseId);
        void AddContractor(Contractor contractor);
        void AddInvoice(int contractorId, ContractorInvoice contractorInvoice);
        void AddExpense(int contractorInvoiceId, Expense expense);
        void DeleteContractor(Contractor contractor);
        void DeleteInvoice(ContractorInvoice contractorInvoice);
        void DeleteExpense(Expense expense);
        bool Save();
    }
}
