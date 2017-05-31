using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
        void Run()
        {
            CustomerData cd = new CustomerData();
            List<Customer> custList = cd.GetCustomers(100);
            foreach(Customer c in custList)
            {
                Console.WriteLine("Age: " + c.Age +  " AnnualIncome: " + c.AnnualIncome + " WorkStatus: " + c.WorkStatus + " Destination: " + c.Destination);
            }
            Console.ReadKey(true);
            
        }
    }
}
