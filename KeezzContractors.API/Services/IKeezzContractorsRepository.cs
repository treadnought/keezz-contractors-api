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
        Contractor GetContractor(int contractorId, bool includeContractorInvoices);
        IEnumerable<ContractorInvoice> GetContractorInvoices(int contractorId);
        ContractorInvoice GetContractorInvoice(int contractorId, int contractorInvoiceId);
        IEnumerable<Expense> GetExpenses(int contractorInvoiceId);
        Expense GetExpense(int contractorInvoiceId, int expenseId);
    }
}
