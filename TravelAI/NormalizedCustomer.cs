using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAI
{
    class NormalizedCustomer
    {
        // Prag, Budapest, Berlin,Stockholm ,Oslo , London, New York, Grønland, Bora Bora , Dubai,
        public double Age { get; set; }
        public double AnnualIncome { get; set; }
        public double WorkStatusStudent { get; set; }
        public double WorkStatusUnemployed { get; set; }
        public double WorkStatusEmployed { get; set; }
        public double WorkStatusRetired { get; set; }
        public double DestinationPrag { get; set; }
        public double DestinationBudapest { get; set; }
        public double DestinationBerlin { get; set; }
        public double DestinationStockholm { get; set; }
        public double DestinationOslo { get; set; }
        public double DestinationLondon { get; set; }
        public double DestinationNewYork { get; set; }
        public double DestinationGreenland { get; set; }
        public double DestinationBoraBora { get; set; }
        public double DestinationDubai { get; set; }

        public NormalizedCustomer(double age, double annualIncome, double workStatusStudent, double workStatusUnemployed, double workStatusEmployed, double workStatusRetired)
        {
            Age = age;
            AnnualIncome = annualIncome;
            WorkStatusStudent = workStatusStudent;
            WorkStatusUnemployed = workStatusUnemployed;
            WorkStatusEmployed = workStatusEmployed;
            WorkStatusRetired = workStatusRetired;
        }
    }
}
