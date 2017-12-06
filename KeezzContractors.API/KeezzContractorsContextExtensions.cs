using KeezzContractors.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API
{
    public static class KeezzContractorsExtensions
    {
        public static void EnsureSeedDataForContext(this KeezzContractorsContext context)
        {
            if (context.Contractors.Any()) return;

            var contractors = new List<Contractor>()
            {
                new Contractor()
                {
                    LastName = "Boyd",
                    FirstName = "Lindsay",
                    Inactive = false,
                    ContractorInvoices = new List<ContractorInvoice>()
                    {
                        new ContractorInvoice()
                        {
                            ContractorInvRef = "170526",
                            ContractorInvDate = new DateTime(2017,05,26),
                            DaysBilled = 0,
                            ContractorInvNote = "",
                            Expenses = new List<Expense>()
                            {
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 05, 24),
                                    ExpenseAmount = 352.37,
                                    ForeignAmount = 371.52,
                                    CurrencyId = 1,
                                    ExpenseNote = "Commercial Hotel Hamilton (Sudima Hotel)",
                                    ProjectId = 62,
                                    KeezzInvId = 1184,
                                    GST = false,
                                    OnBill = true
                                },
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 05, 25),
                                    ExpenseAmount = 441.54,
                                    ForeignAmount = 464.4,
                                    CurrencyId = 1,
                                    ExpenseNote = "Commercial Hotel Hamilton (Sudima Roussos)",
                                    ProjectId = 62,
                                    KeezzInvId = 1184,
                                    GST = false,
                                    OnBill = true
                                },
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 05, 22),
                                    ExpenseAmount = 477.69,
                                    ForeignAmount = 505.38,
                                    CurrencyId = 1,
                                    ExpenseNote = "Quest Hamilton",
                                    ProjectId = 62,
                                    KeezzInvId = 1182,
                                    GST = false,
                                    OnBill = true
                                }
                            }
                        },
                        new ContractorInvoice()
                        {
                            ContractorInvRef = "170626",
                            ContractorInvDate = new DateTime(2017,06,26),
                            DaysBilled = 0,
                            ContractorInvNote = "",
                            Expenses = new List<Expense>()
                            {
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 05, 30),
                                    ExpenseAmount = 209.56,
                                    ForeignAmount = 217.5,
                                    CurrencyId = 1,
                                    ExpenseNote = "Hamilton City Oaks",
                                    ProjectId = 62,
                                    KeezzInvId = 1186,
                                    GST = false,
                                    OnBill = true
                                },
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 05, 30),
                                    ExpenseAmount = 440.43,
                                    ForeignAmount = 458.18,
                                    CurrencyId = 1,
                                    ExpenseNote = "Quest Hamilton",
                                    ProjectId = 62,
                                    KeezzInvId = 1184,
                                    GST = false,
                                    OnBill = true
                                },
                                new Expense()
                                {
                                    ExpenseHeaderId = 14,
                                    ExpenseDate = new DateTime(2017, 06, 05),
                                    ExpenseAmount = 1062.18,
                                    ForeignAmount = 1095.13,
                                    CurrencyId = 1,
                                    ExpenseNote = "Avis New Zealand",
                                    ProjectId = 62,
                                    KeezzInvId = 1184,
                                    GST = false,
                                    OnBill = true
                                }
                            }
                        }
                    }
                },
                new Contractor()
                {
                    LastName = "Hamann",
                    FirstName = "Heike",
                    Inactive = false,
                    ContractorInvoices = new List<ContractorInvoice>()
                    {
                        new ContractorInvoice()
                        {
                            ContractorInvRef = "KZ395",
                            ContractorInvDate = new DateTime(2017,09,10),
                            DaysBilled = 7,
                            ContractorInvNote = "",
                            Expenses = new List<Expense>()
                            {
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 09, 04),
                                    ExpenseAmount = 146.68,
                                    ForeignAmount = 162.98,
                                    CurrencyId = 1,
                                    ExpenseNote = "Accommodation 4 Sep 2017",
                                    ProjectId = 62,
                                    KeezzInvId = 1190,
                                    GST = false,
                                    OnBill = true
                                },
                                new Expense()
                                {
                                    ExpenseHeaderId = 2,
                                    ExpenseDate = new DateTime(2017, 09, 03),
                                    ExpenseAmount = 249.08,
                                    ForeignAmount = 276.75,
                                    CurrencyId = 1,
                                    ExpenseNote = "Accommodation 2-3 Sep 2017",
                                    ProjectId = 62,
                                    KeezzInvId = 1190,
                                    GST = false,
                                    OnBill = true
                                },
                                new Expense()
                                {
                                    ExpenseHeaderId = 1,
                                    ExpenseDate = new DateTime(2017, 09, 01),
                                    ExpenseAmount = 325,
                                    ExpenseNote = "Flight OOL-AKL (Air Asia), 02/09/2017",
                                    ProjectId = 62,
                                    KeezzInvId = 1189,
                                    GST = false,
                                    OnBill = true
                                }
                            }
                        }
                    }
                }
            };

            context.Contractors.AddRange(contractors);
            context.SaveChanges();
        }
    }
}
