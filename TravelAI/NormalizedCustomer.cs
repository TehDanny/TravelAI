using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAI
{
    class NormalizedCustomer
    {
        public double Age { get; set; }
        public double AnnualIncome { get; set; }
        public double WorkStatusStudent { get; set; }
        public double WorkStatusUnemployed { get; set; }
        public double WorkStatusEmployed { get; set; }
        public double WorkStatusRetired { get; set; }

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
