using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ExpenseHeaderDto
    {
        public int Id { get; set; }
        public string ExpenseHeaderName { get; set; }
        public bool Inactive { get; set; }
    }
}
