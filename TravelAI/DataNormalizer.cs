using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAI
{
    class DataNormalizer
    {
        public NormalizedCustomer GetNormalizedCustomer(Customer customer)
        {
            return new NormalizedCustomer();
        }

        public double NormalizeAge(int age) // 16-80 --> 0.0-1.0
        {
            return Normalize(16, 80, age);
        }

        public double NormalizeAnnualIncome(int annualIncome) // 0-500000 --> 0.0-1.0
        {
            return Normalize(0, 500000, annualIncome);
        }

        public void NormalizeWorkStatus(int workstatus) // change void later
        {

        }

        private double Normalize(int lowestValue, int highestValue, int value)
        {
            return (value - lowestValue) / (highestValue - lowestValue);
        }
    }
}
