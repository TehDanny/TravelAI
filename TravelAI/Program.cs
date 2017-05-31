using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLibrary;

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
            NeuralNet net = new NeuralNet();
            int randomSeed = 1;
            int numberOfInputs = 6;
            int numberOfOutputs = 10;
            int numberOfHiddenNeurons = 12;
            net.Initialize(randomSeed, numberOfInputs, numberOfHiddenNeurons, numberOfOutputs);

            CustomerData cd = new CustomerData();
            List<Customer> customerList = cd.GetCustomers(100);
            int customerCount = customerList.Count();
            if(customerList != null || customerList.Count >= 1)
            {
               
                foreach (Customer cust in customerList)
                {
                    double[][] input = new Double[customerCount][];
                    for(int i=1; i<= customerCount; i++)
                    {
                        
                    }
                }
                do
                {

                }
                while (true);
            }
           
            foreach(Customer c in customerList)
            {
                Console.WriteLine("Age: " + c.Age +  " AnnualIncome: " + c.AnnualIncome + " WorkStatus: " + c.WorkStatus + " Destination: " + c.Destination);
            }
            Console.ReadKey(true);
            
        }
    }
}
