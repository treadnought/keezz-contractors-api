using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorInvoiceDto
    {
        public int Id { get; set; }
        public string ContractorInvRef { get; set; }
        public DateTime ContractorInvDate { get; set; }
        public int DaysBilled { get; set; }
        public string ContractorInvNote { get; set; }
    }
}
