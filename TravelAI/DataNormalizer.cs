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
            NormalizedCustomer nc = new NormalizedCustomer(
                NormalizeAge(customer.Age),
                NormalizeAnnualIncome(customer.AnnualIncome),
                0, 0, 0, 0
                );

            NormalizeWorkStatus(customer, nc);

            return nc;
        }

        private void NormalizeWorkStatus(Customer customer, NormalizedCustomer nc)
        {
            switch (customer.WorkStatus)
            {
                case 1:
                    nc.WorkStatusStudent = 1;
                    break;
                case 2:
                    nc.WorkStatusEmployed = 1;
                    break;
                case 3:
                    nc.WorkStatusUnemployed = 1;
                    break;
                case 4:
                    nc.WorkStatusRetired = 1;
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
