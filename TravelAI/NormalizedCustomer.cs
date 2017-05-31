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

        public NormalizedCustomer(Customer customer)
        {
            Age = NormalizeAge(customer.Age);
            AnnualIncome = NormalizeAnnualIncome(customer.AnnualIncome);
            NormalizeWorkStatus(customer.WorkStatus);
            NormalizeDestination(customer.Destination);
            
        }
        void NormalizeWorkStatus(int workStatus)
        {
            switch (workStatus)
            {
                case 1:
                    this.WorkStatusStudent = 1;
                    break;
                case 2:
                    this.WorkStatusEmployed = 1;
                    break;
                case 3:
                    this.WorkStatusUnemployed = 1;
                    break;
                case 4:
                    this.WorkStatusRetired = 1;
                    break;
                default:
                    throw new Exception();
            }
        }
        void NormalizeDestination(int destination)
        {
            switch (destination)
            {
                case 1:
                    this.DestinationPrag = 1;
                    break;
                case 2:
                    this.DestinationBudapest = 1;
                    break;
                case 3:
                    this.DestinationBerlin = 1;
                    break;
                case 4:
                    this.DestinationStockholm = 1;
                    break;
                case 5:
                    this.DestinationOslo = 1;
                    break;
                case 6:
                    this.DestinationLondon = 1;
                    break;
                case 7:
                    this.DestinationNewYork = 1;
                    break;
                case 8:
                    this.DestinationGreenland = 1;
                    break;
                case 9:
                    this.DestinationBoraBora = 1;
                    break;
                case 10:
                    this.DestinationDubai = 1;
                    break;
                default:
                    throw new Exception();
            }
        }
        private double NormalizeAge(int age) // 16-80 --> 0.0-1.0
        {
            return NormalizeValue(16, 80, age);
        }

        private double NormalizeAnnualIncome(int annualIncome) // 0-500000 --> 0.0-1.0
        {
            return NormalizeValue(0, 500000, annualIncome);
        }

        private double NormalizeValue(int lowestValue, int highestValue, int value)
        {
            return (value - lowestValue) / (highestValue - lowestValue);
        }
    }
}
