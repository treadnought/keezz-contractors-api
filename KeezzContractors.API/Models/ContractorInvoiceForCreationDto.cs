using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorInvoiceForCreationDto
    {
        [Required(ErrorMessage = "You must provide an invoice reference.")]
        [MaxLength(20)]
        public string ContractorInvRef { get; set; }

        [Required]
        public DateTime ContractorInvDate { get; set; }

        [Range(0,14)]
        public int DaysBilled { get; set; }

        [MaxLength(200)]
        public string ContractorInvNote { get; set; }

        public ICollection<ExpenseDto> Expenses { get; set; }
            = new List<ExpenseDto>();

    }
}
