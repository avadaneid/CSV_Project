using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_CSV.Models
{
    public class BankingModel:IBankingModel
    {
        [Range(1010101000000, 7010101000000, ErrorMessage = "PIN length not valid ! PIN must have 13 characters !")]
        public long PIN { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters and numbers are not allowed !")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters and numbers are not allowed !")]
        public string Surname { get; set; }   
        [Range(500,90000,ErrorMessage = "Loan Amount must be between 500 and 90000 ! ")]
        public int LoanAmount { get; set; }

        [Range(0.001, 0.99, ErrorMessage = " Interest Range can be between 0.001 and 0.99 ")]
        public float InterestPercent { get; set; }
        public bool IsVariable { get; set; }
        public double InterestRate { get; set; }
        public double Principal { get; set; }

    }

}