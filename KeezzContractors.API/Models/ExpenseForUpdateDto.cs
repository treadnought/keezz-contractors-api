using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ExpenseForUpdateDto
    {
        [Required]
        public int ExpenseHeaderId { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }
        [Required]
        public double ExpenseAmount { get; set; }

        public double ForeignAmount { get; set; }
        public int CurrencyId { get; set; }

        public string ExpenseNote { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public int KeezzInvId { get; set; }

        public bool GST { get; set; } = true;
        public bool OnBill { get; set; } = false;
    }
}
