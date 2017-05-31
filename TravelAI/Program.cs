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
            double high = .9;
            double low = .1;
            double mid = .5;
            net.Initialize(randomSeed, numberOfInputs, numberOfHiddenNeurons, numberOfOutputs);

            CustomerData cd = new CustomerData();
            List<Customer> customerList = cd.GetCustomers(100);
            
            int customerCount = customerList.Count();
            if(customerList != null || customerList.Count >= 1)
            {
                double[][] input = new double[customerCount][];
                double[][] output = new double[customerCount][];
                DataNormalizer dataNormalizer = new DataNormalizer();
                int i = 0;
                foreach (Customer customer in customerList)
                {
                    // Prag, Budapest, Berlin,Stockholm ,Oslo , London, New York, Grønland, Bora Bora , Dubai,
                    i++;
                    NormalizedCustomer nc= dataNormalizer.GetNormalizedCustomer(customer);
                   
                    input[i] = new double[] { nc.Age, nc.AnnualIncome, nc.WorkStatusStudent, nc.WorkStatusEmployed, nc.WorkStatusUnemployed, nc.WorkStatusRetired };
                    output[i] = new double[] {nc.DestinationPrag, nc.DestinationBudapest, nc.DestinationBerlin, nc.DestinationStockholm, nc.DestinationOslo, nc.DestinationLondon,
                        nc.DestinationNewYork, nc.DestinationGreenland, nc.DestinationBoraBora, nc.DestinationDubai};
                }
                double ll, lh, hl, hh;
                int count;

                count = 0;
                do
                {
                    count++;
                    net.Train(input, output);

                    net.ApplyLearning();

                    net.PerceptionLayer[0].Output = low;
                    net.PerceptionLayer[1].Output = low;

                    net.Pulse();

                    ll = net.OutputLayer[0].Output;

                    net.PerceptionLayer[0].Output = high;
                    net.PerceptionLayer[1].Output = low;

                    net.Pulse();

                    hl = net.OutputLayer[0].Output;

                    net.PerceptionLayer[0].Output = low;
                    net.PerceptionLayer[1].Output = high;

                    net.Pulse();

                    lh = net.OutputLayer[0].Output;

                    net.PerceptionLayer[0].Output = high;
                    net.PerceptionLayer[1].Output = high;

                    net.Pulse();

                    hh = net.OutputLayer[0].Output;
                }
                while (hh > mid || lh < mid || hl < mid || ll > mid);

                Console.WriteLine(count.ToString() + " iterations required for training");
            }

            //foreach(Customer c in customerList)
            //{
            //    Console.WriteLine("Age: " + c.Age +  " AnnualIncome: " + c.AnnualIncome + " WorkStatus: " + c.WorkStatus + " Destination: " + c.Destination);
            //}
            
            Console.ReadKey(true);
            
        }
    }
}
