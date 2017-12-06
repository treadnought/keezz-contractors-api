using KeezzContractors.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Controllers
{
    public class DummyController : Controller
    {
        private KeezzContractorsContext _context;

        public DummyController(KeezzContractorsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/testes")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
