using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Models
{
    public class ContractorWithoutInvoicesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContractorCompany { get; set; }
        public bool Inactive { get; set; }
    }
}
