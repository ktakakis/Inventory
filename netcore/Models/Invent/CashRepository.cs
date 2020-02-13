﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class CashRepository: INetcoreBasic
    {
        [StringLength(38)]
        [Display(Name = "Id CashRepository")]
        public string CashRepositoryId { get; set; }

        public CashRepository() 
        {
            this.createdAt = DateTime.UtcNow;
        }

        [Required]
        [Display(Name = "Όνομα Ταμείου")]
        public string CashRepositoryName { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        [Display(Name = "Σύνολο Εισπράξεων")]
        public decimal TotalReceipts { get; set; } 

        [Display(Name = "Σύνολο Πληρωμών")]
        public decimal TotalPayments { get; set; }

        [Display(Name = "Υπόλοιπο")]
        public decimal Balance { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Συνεργάτη")]
        public string EmployeeId { get; set; }

        [Display(Name = "Συνεργάτης")]
        public Employee Employee { get; set; }

        [Display(Name = "Εισπράξεις")]
        public List<PaymentReceive> paymentReceive { get; set; } = new List<PaymentReceive>(); 

    }
}