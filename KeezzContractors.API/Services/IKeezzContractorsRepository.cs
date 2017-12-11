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
        IEnumerable<Contractor>GetContractors();
        Contractor GetContractor(int contractorId, bool includeContractorInvoices = false);
        IEnumerable<ContractorInvoice> GetContractorInvoices(int contractorId);
        ContractorInvoice GetContractorInvoice(int contractorInvoiceId);
        IEnumerable<Expense> GetExpenses(int contractorInvoiceId);
        Expense GetExpense(int contractorInvoiceId, int expenseId);
        void AddContractor(Contractor contractor);
        void AddInvoice(int contractorId, ContractorInvoice contractorInvoice);
        void AddExpense(int contractorInvoiceId, Expense expense);
        void DeleteContractor(Contractor contractor);
        void DeleteInvoice(ContractorInvoice contractorInvoice);
        bool Save();
    }
}
