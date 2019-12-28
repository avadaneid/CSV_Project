using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CSV.Models
{
    public interface IBankingModel
    {
        long PIN { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        int LoanAmount { get; set; }
        float InterestPercent { get; set; }
        bool IsVariable { get; set; }    
        double InterestRate { get; set; }
        double Principal { get; set; }
    }
}
