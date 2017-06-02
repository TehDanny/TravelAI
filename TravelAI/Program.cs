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
            int numberOfInputNodes = 6;
            int numberOfOutputNodes = 10;
            int numberOfHiddenNeurons = 12;
            net.Initialize(randomSeed, numberOfInputNodes, numberOfHiddenNeurons, numberOfOutputNodes);

            CustomerData cd = new CustomerData();
            List<Customer> customerList = cd.GetCustomers(20000);
            List<List<Customer>> customerDataLists = GetCustomerLists(customerList);
            List<Customer> trainCustomerList = customerDataLists[0];
            List<Customer> testCustomerList = customerDataLists[1];

            int customerCount = customerList.Count();
            if(customerList != null && customerList.Count >= 1)
            {
                double[][] trainDataInputArray = GetInputArray(trainCustomerList);
                double[][] trainDataOutputArray = GetOutputArray(trainCustomerList);
                double[][] testDataInputArray = GetInputArray(testCustomerList);
                double[][] testDataOutputArray = GetOutputArray(testCustomerList);
                int iterations;
                int testDataInputCount = testDataInputArray.Count();
                int testDataOutputCount = testDataOutputArray.Count();
                double[][] actualTestDataResults;
                double[][] expectedTestDataResults = new double[testDataInputCount][];
                iterations = 0;
                do
                {
                    actualTestDataResults = new double[testDataOutputCount][];
                    iterations++;
                    //for (int i = 0; i < 10; i++)
                    //{
                        net.Train(trainDataInputArray, trainDataOutputArray);
                    //}

                    // først træner netværket og så sætter den prædefinerede værdier igennem for at se om resultatet passer og hvis ikke gør den det igen
                    net.ApplyLearning();
                    
                    for(int i = 0; i<testDataInputArray.Count(); i++)
                    {
                        for(int j = 0; j < numberOfInputNodes; j++)
                        {
                            net.PerceptionLayer[j].Output = testDataInputArray[i][j];
                        }
                        net.Pulse();
                        actualTestDataResults[i] = new double[numberOfOutputNodes];
                        expectedTestDataResults[i] = new double[numberOfOutputNodes];
                        for(int k = 0;k< numberOfOutputNodes; k++)
                        {
                            actualTestDataResults[i][k] = net.OutputLayer[k].Output;
                            expectedTestDataResults[i][k] = testDataOutputArray[i][k];
                        }
                        
                    }
                    Console.WriteLine(iterations.ToString());
                }
                while (TestResults(actualTestDataResults, numberOfOutputNodes)); //mens outputtene er over/under en hvis værdi
                double[] accuracyResults = GetAccuracy(testDataOutputArray, actualTestDataResults, numberOfOutputNodes);
                Console.WriteLine("Right Results: " + accuracyResults[0] + " Wrong Results " + accuracyResults[1]);
                Console.WriteLine(iterations.ToString() + " iterations required for training");
            }
            Console.ReadKey(true);
            
        }
        
        bool TestResults(double[][] actualTestDataResults, int numberOfOutputNodes)
        {
            int correctResults = 0;
            int wrongResults = 0;
            int testOutputResultsCount = actualTestDataResults.Count();
            for (int i = 0; i < testOutputResultsCount; i++)
            {
                //Console.WriteLine(testOutputResults[i].ToString());
                for (int j = 0; j < numberOfOutputNodes; j++)
                {
                    if (actualTestDataResults[i][j] > 0.5)
                    {
                        correctResults++;
                    }
                    else
                        wrongResults++;
                }
                
            }
            Console.WriteLine("Correct: " + correctResults.ToString() + " Wrong: " + wrongResults.ToString());
            if (correctResults > testOutputResultsCount * 0.9)
            {
                return false;
            }
            else
                return true;
        }
        double[] GetAccuracy(double[][] expectedResults, double[][] actualResults, int numberOfOutputNodes)
        {
            int countRight = 0;
            int countWrong = 0;
            int rowCount = expectedResults.Count();
            int outputCount = expectedResults[1].Count();
            for (int i = 0; i < rowCount ; i++)
            {
                for (int j = 0; j < numberOfOutputNodes; j++)
                {
                    if (expectedResults[i][j] > 0.5)
                    {
                        expectedResults[i][j] = 0.9;
                    }
                    if (expectedResults[i][j] == actualResults[i][j])
                    {
                        countRight++;
                    }
                    else
                    {
                        countWrong++;
                    }
                }
               
                    
            }
            return new double[] { countRight, countWrong};
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
