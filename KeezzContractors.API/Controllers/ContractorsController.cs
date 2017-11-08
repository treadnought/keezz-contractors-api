using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    [Route("api/contractors")]
    public class ContractorsController : Controller
    {
        [HttpGet()]
        public IActionResult GetContractors()
        {
            return Ok(ContractorsDataStore.Current.Contractors);
        }

        [HttpGet("{id}")]
        public IActionResult GetContractor(int id)
        {
            var contractorToReturn = ContractorsDataStore.Current.Contractors.FirstOrDefault(c => c.Id == id);
            if (contractorToReturn == null) return NotFound();

            return Ok(contractorToReturn);
        }
    }
}
