using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Inactive { get; set; } = false;

        public ICollection<ContractorInvoiceDto> ContractorInvoices { get; set; }
            = new List<ContractorInvoiceDto>();
    }
}
