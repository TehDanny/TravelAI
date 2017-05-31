using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAI
{
    class Customer
    {
        public int Age { get; set; }
        public int AnnualIncome { get; set; }
        public int WorkStatus { get; set; }
        public int Destination { get; set; }
        public Customer(int age, int annualIncome, int workStatus, int destination)
        {
            this.Age = age;
            this.AnnualIncome = annualIncome;
            this.WorkStatus = workStatus;
            this.Destination = destination;
        }
    }
}
