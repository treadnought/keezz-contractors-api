using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorWithInvoicesWithoutExpensesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContractorCompany { get; set; }
        public bool Inactive { get; set; }

        public int NumberOfInvoices
        {
            get
            {
                return ContractorInvoices.Count;
            }
        }

        public ICollection<ContractorInvoiceWithoutExpensesDto> ContractorInvoices { get; set; }
            = new List<ContractorInvoiceWithoutExpensesDto>();
    }
}
