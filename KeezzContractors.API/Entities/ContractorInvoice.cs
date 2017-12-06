using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Entities
{
    public class ContractorInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContractorInvRef { get; set; }

        [Required]
        public DateTime ContractorInvDate { get; set; }

        [Range(0,7)]
        public int DaysBilled { get; set; }

        [MaxLength(200)]
        public string ContractorInvNote { get; set; }

        public ICollection<Expense> Expenses { get; set; }
            = new List<Expense>();

        [ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }
    }
}
