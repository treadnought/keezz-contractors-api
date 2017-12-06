using KeezzContractors.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Services
{
    public interface IKeezzContractorsRepository
    {
        IEnumerable<Contractor>GetContractors();
        Contractor GetContractor(int contractorId, bool includeContractorInvoices);
        IEnumerable<ContractorInvoice> GetContractorInvoices(int contractorId);
        ContractorInvoice GetContractorInvoice(int contractorId, int contractorInvoiceId);
    }
}
