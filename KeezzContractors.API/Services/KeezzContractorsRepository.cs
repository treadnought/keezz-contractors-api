using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeezzContractors.API.Entities;
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

        public Contractor GetContractor(int contractorId, bool includeContractorInvoices)
        {
            if (includeContractorInvoices)
            {
                return _context.Contractors.Include(c => c.ContractorInvoices)
                    .Where(c => c.Id == contractorId).FirstOrDefault();
            }

            return _context.Contractors.Where(c => c.Id == contractorId).FirstOrDefault();
        }

        public ContractorInvoice GetContractorInvoice(int contractorId, int contractorInvoiceId)
        {
            return _context.ContractorInvoices
                .Where(i => i.ContractorId == contractorId && i.Id == contractorInvoiceId).FirstOrDefault();
        }

        public IEnumerable<ContractorInvoice> GetContractorInvoices(int contractorId)
        {
            return _context.ContractorInvoices.Where(i => i.ContractorId == contractorId).ToList();
        }

        public IEnumerable<Contractor> GetContractors()
        {
            return _context.Contractors.OrderBy(c => c.LastName).ToList();
        }
    }
}
