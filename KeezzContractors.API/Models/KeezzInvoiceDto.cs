using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class KeezzInvoiceDto
    {
        public int Id { get; set; }
        public string KeezzInvoiceRef { get; set; }
        public DateTime KeezzInvoiceDate { get; set; }
        public DateTime PeriodEnding { get; set; }
        public int MyProperty { get; set; }
        public int ProjectId { get; set; }
    }
}
