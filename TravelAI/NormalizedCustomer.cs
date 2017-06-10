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
        double low = 0;
        double high = 1;
        public NormalizedCustomer(Customer customer)
        {
            Age = NormalizeAge(customer.Age);
            AnnualIncome = NormalizeAnnualIncome(customer.AnnualIncome);
            NormalizeWorkStatus(customer.WorkStatus);
            NormalizeDestination(customer.Destination);
            
        }
        void NormalizeWorkStatus(int workStatus)
        {
            
            this.WorkStatusStudent = low;
            this.WorkStatusEmployed = low;
            this.WorkStatusUnemployed = low;
            this.WorkStatusRetired = low;

            switch (workStatus)
            {
                case 1:
                    this.WorkStatusStudent = high;
                    break;
                case 2:
                    this.WorkStatusEmployed = high;
                    break;
                case 3:
                    this.WorkStatusUnemployed = high;
                    break;
                case 4:
                    this.WorkStatusRetired = high;
                    break;
                default:
                    throw new Exception();
            }
        }
        void NormalizeDestination(int destination)
        {
            
            this.DestinationPrag = low;
            this.DestinationBudapest = low;
            this.DestinationBerlin = low;
            this.DestinationStockholm = low;
            this.DestinationOslo = low;
            this.DestinationLondon = low;
            this.DestinationNewYork = low;
            this.DestinationGreenland = low;
            this.DestinationBoraBora = low;
            this.DestinationDubai = low;

            switch (destination)
            {
                case 1:
                    this.DestinationPrag = high;
                    break;
                case 2:
                    this.DestinationBudapest = high;
                    break;
                case 3:
                    this.DestinationBerlin = high;
                    break;
                case 4:
                    this.DestinationStockholm = high;
                    break;
                case 5:
                    this.DestinationOslo = high;
                    break;
                case 6:
                    this.DestinationLondon = high;
                    break;
                case 7:
                    this.DestinationNewYork = high;
                    break;
                case 8:
                    this.DestinationGreenland = high;
                    break;
                case 9:
                    this.DestinationBoraBora = high;
                    break;
                case 10:
                    this.DestinationDubai = high;
                    break;
                default:
                    throw new Exception();
            }
        }
        private double NormalizeAge(int age) // 16-80 --> 0.0-1.0
        {
            double normalizeAge = NormalizeValue(16, 80, age);
            return normalizeAge;
        }

        private double NormalizeAnnualIncome(int annualIncome) // 0-500000 --> 0.0-1.0
        {
            return NormalizeValue(0, 500000, annualIncome);
        }

        private double NormalizeValue(int lowestValue, int highestValue, int value)
        {
            double normalizedValue = (Convert.ToDouble(value) - Convert.ToDouble(lowestValue)) /
                (Convert.ToDouble(highestValue) - Convert.ToDouble(lowestValue));
            return normalizedValue;
        }
    }
}
