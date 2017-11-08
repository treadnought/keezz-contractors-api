using KeezzContractors.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API
{
    public class ContractorsDataStore
    {
        public static ContractorsDataStore Current { get; } = new ContractorsDataStore();

        public List<ContractorDto> Contractors { get; set; }

        public ContractorsDataStore()
        {
            Contractors = new List<ContractorDto>()
            {
                new ContractorDto()
                {
                    Id = 1,
                    FirstName = "Lindsay",
                    LastName = "Boyd",
                    ContractorInvoices = new List<ContractorInvoiceDto>()
                    {
                        new ContractorInvoiceDto()
                        {
                            Id = 42,
                            ContractorInvRef = "20",
                            ContractorInvDate = new DateTime(2007,6,4),
                            DaysBilled = 5,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 44,
                            ContractorInvRef = "21",
                            ContractorInvDate = new DateTime(2007,6,27),
                            DaysBilled = 4,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 45,
                            ContractorInvRef = "22",
                            ContractorInvDate = new DateTime(2007,6,28),
                            DaysBilled = 7,
                            ContractorInvNote = ""
                        }
                    }
                },
                new ContractorDto()
                {
                    Id = 5,
                    FirstName = "Geoff",
                    LastName = "Pearson",
                    ContractorInvoices = new List<ContractorInvoiceDto>()
                    {
                        new ContractorInvoiceDto()
                        {
                            Id = 114,
                            ContractorInvRef = "80617",
                            ContractorInvDate = new DateTime(2008,6,17),
                            DaysBilled = 2,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 116,
                            ContractorInvRef = "80702",
                            ContractorInvDate = new DateTime(2008,7,2),
                            DaysBilled = 6,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 127,
                            ContractorInvRef = "80815",
                            ContractorInvDate = new DateTime(2008,8,15),
                            DaysBilled = 2,
                            ContractorInvNote = ""
                        }
                    }
                },
                new ContractorDto()
                {
                    Id = 10,
                    FirstName = "Heike",
                    LastName = "Hamann",
                    ContractorInvoices = new List<ContractorInvoiceDto>()
                    {
                        new ContractorInvoiceDto()
                        {
                            Id = 211,
                            ContractorInvRef = "KZ117",
                            ContractorInvDate = new DateTime(2009,4,11),
                            DaysBilled = 6,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 214,
                            ContractorInvRef = "KZ118",
                            ContractorInvDate = new DateTime(2009,4,25),
                            DaysBilled = 2,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 219,
                            ContractorInvRef = "KZ119",
                            ContractorInvDate = new DateTime(2009,5,9),
                            DaysBilled = 1,
                            ContractorInvNote = ""
                        }
                    }
                },
                new ContractorDto()
                {
                    Id = 11,
                    FirstName = "Matthew",
                    LastName = "Johnston",
                    ContractorInvoices = new List<ContractorInvoiceDto>()
                    {
                        new ContractorInvoiceDto()
                        {
                            Id = 231,
                            ContractorInvRef = "KEE-001",
                            ContractorInvDate = new DateTime(2009,5,23),
                            DaysBilled = 7,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 235,
                            ContractorInvRef = "KEE-002",
                            ContractorInvDate = new DateTime(2009,6,6),
                            DaysBilled = 2,
                            ContractorInvNote = ""
                        },
                        new ContractorInvoiceDto()
                        {
                            Id = 246,
                            ContractorInvRef = "KEE-003",
                            ContractorInvDate = new DateTime(2009,6,20),
                            DaysBilled = 3,
                            ContractorInvNote = ""
                        }
                    }
                }
            };
        }
    }
}
