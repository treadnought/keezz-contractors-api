﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Entities
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ExpenseHeaderId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double ExpenseAmount { get; set; }
        public double ForeignAmount { get; set; }
        public int CurrencyId { get; set; }
        public string ExpenseNote { get; set; }
        public int ProjectId { get; set; }
        public int KeezzInvId { get; set; }
        public bool GST { get; set; }
        public bool OnBill { get; set; }

        public ContractorInvoice ContractorInvoice { get; set; }
    }
}
