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
            int numberOfHiddenNeurons = 20;
            double high = .9;
            double low = .1;
            double mid = .5;
            net.Initialize(randomSeed, numberOfInputs, numberOfHiddenNeurons, numberOfOutputs);

            CustomerData cd = new CustomerData();
            List<Customer> customerList = cd.GetCustomers(10000);
            List<Customer> trainCustomerList = GetCustomerLists(customerList)[0];
            List<Customer> testCustomerList = GetCustomerLists(customerList)[1];

            int customerCount = customerList.Count();
            if(customerList != null && customerList.Count >= 1)
            {
                double[][] trainInput = GetInputArray(trainCustomerList);
                double[][] trainOutput = GetOutputArray(trainCustomerList);
                double[][] testInput = GetInputArray(testCustomerList);
                double[][] testOutput = GetOutputArray(testCustomerList);

                double ll, lh, hl, hh;
                int iterations;
                int testInputCount = testInput.Count();
                double[] testOutputResults;
                iterations = 0;
                do
                {
                    testOutputResults = new double[testInputCount];
                    iterations++;
                    //for (int i = 0; i < 10; i++)
                    //{
                        net.Train(trainInput, trainOutput);
                    //}

                    // først træner netværket og så sætter den prædefinerede værdier igennem for at se om resultatet passer og hvis ikke gør den det igen
                    net.ApplyLearning();
                    
                    for(int i = 0; i<testInput.Count(); i++)
                    {
                        for(int j = 0; j < numberOfInputs; j++)
                        {
                            net.PerceptionLayer[j].Output = testInput[i][j];
                        }
                        net.Pulse();
                        testOutputResults[i] = net.OutputLayer[0].Output;
                    }
                    Console.WriteLine(iterations.ToString());
                }
                while (TestResults(testOutputResults)); //mens outputtene er over/under en hvis værdi

                Console.WriteLine(iterations.ToString() + " iterations required for training");
            }

            //foreach(Customer c in customerList)
            //{
            //    Console.WriteLine("Age: " + c.Age +  " AnnualIncome: " + c.AnnualIncome + " WorkStatus: " + c.WorkStatus + " Destination: " + c.Destination);
            //}
            
            Console.ReadKey(true);
            
        }
        bool TestResults(double[] testOutputResults)
        {
            for(int i = 0; i < testOutputResults.Count(); i++)
            {
                //Console.WriteLine(testOutputResults[i].ToString());
                if (testOutputResults[i] > 0.25)
                {
                    
                    return false;
                }
                    
            }
            
            return true;
        }
        List<List<Customer>> GetCustomerLists(List<Customer> customerList)
        {
            List<List<Customer>> lists = new List<List<Customer>>();
            List<Customer> trainingList = new List<Customer>();
            List<Customer> testingList = new List<Customer>();
            int count = customerList.Count();
            int trainLimit = count * 90 / 100;
            
            int i = 0;
            foreach(Customer customer in customerList)
            {
                i++;
                if (i <= trainLimit)
                {
                    trainingList.Add(customer);
                }
                else
                {
                    testingList.Add(customer);
                }
            }
            lists.Add(trainingList);
            lists.Add(testingList);
            return lists;
        }
        double[][] GetOutputArray(List<Customer> customerList)
        {

            double[][] Output = new double[customerList.Count()][];
            int i = 0;
            foreach(Customer customer in customerList)
            {
                
                NormalizedCustomer nc = new NormalizedCustomer(customer);
                Output[i] = new double[] {nc.DestinationPrag, nc.DestinationBudapest, nc.DestinationBerlin, nc.DestinationStockholm, nc.DestinationOslo, nc.DestinationLondon,
                        nc.DestinationNewYork, nc.DestinationGreenland, nc.DestinationBoraBora, nc.DestinationDubai};
                i++;
            }
            return Output;
        }
        double[][] GetInputArray(List<Customer> customerList)
        {
            double[][] Input = new double[customerList.Count()][];
            int i = 0;
            foreach(Customer customer in customerList)
            {
                
                NormalizedCustomer nc = new NormalizedCustomer(customer);
                Input[i] = new double[] { nc.Age, nc.AnnualIncome, nc.WorkStatusStudent, nc.WorkStatusEmployed, nc.WorkStatusUnemployed, nc.WorkStatusRetired };
                i++;
            }
            return Input;
        }
        
    }
}
