using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorInvoiceForUpdateDto
    {
        [Required(ErrorMessage = "You must provide an invoice reference.")]
        [MaxLength(20)]
        public string ContractorInvRef { get; set; }

        public DateTime ContractorInvDate { get; set; }

        [Range(0, 14)]
        public int DaysBilled { get; set; }

        [MaxLength(200)]
        public string ContractorInvNote { get; set; } = null;
    }
}
