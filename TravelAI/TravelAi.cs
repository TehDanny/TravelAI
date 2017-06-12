using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NeuralNetworkLibrary;
using DataPopulator;

namespace TravelAI
{
    class TravelAI
    {
        NeuralNet net;
        CustomerData cd = new CustomerData();
        List<Customer> customerList;
        List<Customer> trainCustomerList;
        List<Customer> testCustomerList;
        int randomSeed = 3;
        int numberOfInputNodes = 6;
        int numberOfOutputNodes = 10;
        int numberOfHiddenNeurons = 12;
        double errorMargin;
        Stopwatch stopWatch;
        
        public TravelAI()
        {
            net = new NeuralNet();
            errorMargin = 0.5;
            customerList = cd.GetCustomers(20000);
            List<List<Customer>> customerDataLists = GetCustomerLists(customerList);
            trainCustomerList = customerDataLists[0];
            testCustomerList = customerDataLists[1];
            net.Initialize(randomSeed, numberOfInputNodes, numberOfHiddenNeurons, numberOfOutputNodes);
            Run();
        }
        void Run()
        {
            int customerCount = customerList.Count();
            if (customerList != null && customerList.Count >= 1)
            {
                double[][] trainDataInputArray = GetInputArray(trainCustomerList);
                double[][] trainDataOutputArray = GetOutputArray(trainCustomerList);
                double[][] testDataInputArray = GetInputArray(testCustomerList);
                double[][] testDataOutputArray = GetOutputArray(testCustomerList);
                int iterations = 0;
                int testDataInputCount = testDataInputArray.Count();
                double[][] actualTestedDataResults;
                double[][] desiredTestDataResults = new double[testDataInputCount][];
                double deltaErrorSum;
                stopWatch = new Stopwatch();
                Console.WriteLine("Started Training:");
                do
                {
                    actualTestedDataResults = new double[testDataInputCount][];
                    iterations++;
                    stopWatch.Start();
                    net.Train(trainDataInputArray, trainDataOutputArray);
                    
                    net.ApplyLearning();
                    deltaErrorSum = 0;
                    for (int i = 0; i < testDataInputCount; i++)
                    {
                        for (int j = 0; j < numberOfInputNodes; j++)
                        {
                            net.PerceptionLayer[j].Output = testDataInputArray[i][j];
                        }
                        net.Pulse();
                        actualTestedDataResults[i] = new double[numberOfOutputNodes];
                        for (int k = 0; k < numberOfOutputNodes; k++)
                        {
                            actualTestedDataResults[i][k] = net.OutputLayer[k].Output;
                            deltaErrorSum += net.OutputLayer[k].DeltaError; 
                        }
                    }
                    
                }
                while ( deltaErrorSum > 0.00001 || deltaErrorSum < -0.00001);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}m:{1:00}s:{2:00}ms",ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("Accuracy with new Testing Data: " + GetAccuracyWithNewTestingOutputData(2000) + " " + elapsedTime);
                Console.WriteLine(iterations.ToString() + " iterations required for training");
            }

            ManualTestOfNetwork(net, numberOfInputNodes, numberOfOutputNodes);
            Console.ReadKey(true);
        }

        double GetAccuracyWithNewTestingOutputData(int numberOfCustomers)
        {
            DataPopulator.DataPopulator dp = new DataPopulator.DataPopulator();
            List<Person> personList = dp.GeneratePeopleData(numberOfCustomers);
            List<Customer> customerList = new List<Customer>();
            foreach (Person person in personList)
            {
                Customer customer = new Customer(person.Age, person.AnnualIncome, person.WorkStatus, person.Destination);

                customerList.Add(customer);
            }
            double[][] outputTestingDataList = GetOutputArray(customerList);
            double[][] inputTestingDataList = GetInputArray(customerList);

            int customerCount = customerList.Count();
            double[][] actualData = new double[customerCount][];
            for (int i = 0; i < customerCount; i++)
            {
                for (int j = 0; j < numberOfInputNodes; j++)
                {
                    net.PerceptionLayer[j].Output = inputTestingDataList[i][j];
                }
                net.Pulse();
                actualData[i] = new double[numberOfOutputNodes];
                for (int k = 0; k < numberOfOutputNodes; k++)
                {
                    actualData[i][k] = net.OutputLayer[k].Output;
                }
            }

            
            double accuracy = GetAccuracy(outputTestingDataList, actualData);
            
            return accuracy;
        }
        double GetAccuracy(double[][] desiredResult, double[][] actualResults)
        {
            double right = 0;
            double wrong = 0;
            int rowCount = actualResults.Count();
            int columnCount = desiredResult.Count();
            double low = 0 + errorMargin;
            double high = 1 - errorMargin;
            for(int i=0;i< rowCount; i++)
            {
                for(int j = 0; j< numberOfOutputNodes; j++)
                {
                    if (actualResults[i][j] > high)
                        actualResults[i][j] = 1;
                    if (actualResults[i][j] < low)
                        actualResults[i][j] = 0;
                }
                if (actualResults[i].SequenceEqual(desiredResult[i]))
                    right++;
                else
                    wrong++;
            }
            return right/(right+wrong);
        }
        string ConvertArrayToString(double[] array)
        {
            string output = "";
            foreach(double point in array)
            {
                output += point.ToString("0.##") + " ";
            }
            return output;
        }
        List<List<Customer>> GetCustomerLists(List<Customer> customerList)
        {
            List<List<Customer>> lists = new List<List<Customer>>();
            List<Customer> trainingList = new List<Customer>();
            List<Customer> testingList = new List<Customer>();
            int count = customerList.Count();
            int trainLimit = count * 90 / 100;

            int i = 0;
            foreach (Customer customer in customerList)
            {
                i++;
                if (i <= trainLimit)
                    trainingList.Add(customer);
                else
                    testingList.Add(customer);
            }
            lists.Add(trainingList);
            lists.Add(testingList);
            return lists;
        }
        double[][] GetOutputArray(List<Customer> customerList)
        {
            double[][] Output = new double[customerList.Count()][];
            int i = 0;
            foreach (Customer customer in customerList)
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
            foreach (Customer customer in customerList)
            {
                NormalizedCustomer nc = new NormalizedCustomer(customer);
                Input[i] = new double[] { nc.Age, nc.AnnualIncome, nc.WorkStatusStudent, nc.WorkStatusEmployed, nc.WorkStatusUnemployed, nc.WorkStatusRetired };
                i++;
            }
            return Input;
        }
        private void ManualTestOfNetwork(INeuralNet net, int numberOfInputNodes, int numberOfOutputNodes)
        {
            while (true)
            {
                // Asking user for input
                int age, annualIncome, workStatus, destination;
                Console.WriteLine("\nManual test");
                Console.Write("Age: ");
                age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Annual income: ");
                annualIncome = Convert.ToInt32(Console.ReadLine());
                Console.Write("1: Student, 2: Employed, 3: Unemployed, 4: Retired\n" +
                    "Work status: ");
                workStatus = Convert.ToInt32(Console.ReadLine());
                Console.Write("1: Prag, 2: Budapest, 3: Berlin, 4: Stockholm, 5: Oslo, 6: London, 7: New York, 8: Grønland, 9: Bora Bora, 10: Dubai\n" +
                    "Destination:");
                destination = Convert.ToInt32(Console.ReadLine());
                Customer customer = new Customer(age, annualIncome, workStatus, destination);
                NormalizedCustomer nc = new NormalizedCustomer(customer);

                // Converting normalized customer to input and output arrays
                double[][] userInput = new double[1][];
                userInput[0] = new double[] { nc.Age, nc.AnnualIncome, nc.WorkStatusStudent, nc.WorkStatusEmployed, nc.WorkStatusUnemployed, nc.WorkStatusRetired };

                double[][] userOutput = new double[1][];
                userOutput[0] = new double[] {nc.DestinationPrag, nc.DestinationBudapest, nc.DestinationBerlin, nc.DestinationStockholm, nc.DestinationOslo, nc.DestinationLondon,
                        nc.DestinationNewYork, nc.DestinationGreenland, nc.DestinationBoraBora, nc.DestinationDubai};

                // Testing the input customer
                for (int j = 0; j < numberOfInputNodes; j++)
                {
                    net.PerceptionLayer[j].Output = userInput[0][j];
                }

                net.Pulse();

                double[][] actualTestDataResults = new double[1][];
                actualTestDataResults[0] = new double[numberOfOutputNodes];
                for (int i = 0; i < numberOfOutputNodes; i++)
                {
                    actualTestDataResults[0][i] = net.OutputLayer[i].Output;
                }

                // Checking accuracy of the test
                int actualResult = 0;
                for (int i = 0; i < numberOfOutputNodes; i++)
                {
                    if (actualTestDataResults[0][i] > 0.5)
                        actualResult = i+1;
                }

                // Conclusion
                Console.WriteLine("Expected result: {0}, Actual result: {1}", destination, actualResult);
                if (destination == actualResult)
                    Console.WriteLine("The neural network reached the expected result.");
                else if (actualResult == 0)
                    Console.WriteLine("The neural network couldn't find a destination.");
                else
                    Console.WriteLine("The neural network did not reach the expected result.");
                Console.WriteLine("Press any key to manually test again...");
                Console.ReadKey();
            }
        }




    }
}
