using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorForCreationDto
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public bool Inactive { get; set; } = false;

        public ICollection<ContractorInvoiceDto> ContractorInvoices { get; set; }
            = new List<ContractorInvoiceDto>();
    }
}
