﻿using System;
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
            //Program p = new Program();
            //p.Run();
            TravelAi travelAi = new TravelAi();
            
        }
        //void Run()
        //{
            

        //    int customerCount = customerList.Count();
        //    if(customerList != null && customerList.Count >= 1)
        //    {
        //        double[][] trainDataInputArray = GetInputArray(trainCustomerList);
        //        double[][] trainDataOutputArray = GetOutputArray(trainCustomerList);
        //        double[][] testDataInputArray = GetInputArray(testCustomerList);
        //        double[][] testDataOutputArray = GetOutputArray(testCustomerList);
        //        int iterations;
        //        int testDataInputCount = testDataInputArray.Count();
        //        int testDataOutputCount = testDataOutputArray.Count();
        //        double[][] actualTestDataResults;
        //        double[][] expectedTestDataResults = new double[testDataInputCount][];
        //        iterations = 0;
        //        do
        //        {
        //            actualTestDataResults = new double[testDataOutputCount][];
        //            iterations++;
        //            net.Train(trainDataInputArray, trainDataOutputArray);

        //            // først træner netværket og så tester den de prædefinerede værdier igennem for at se om resultatet passer og hvis ikke gør den det igen
        //            net.ApplyLearning();
                    
        //            for(int i = 0; i<testDataInputArray.Count(); i++)
        //            {
        //                for(int j = 0; j < numberOfInputNodes; j++)
        //                {
        //                    net.PerceptionLayer[j].Output = testDataInputArray[i][j];
        //                }
        //                net.Pulse();
        //                actualTestDataResults[i] = new double[numberOfOutputNodes];
        //                expectedTestDataResults[i] = new double[numberOfOutputNodes];
        //                for(int k = 0;k< numberOfOutputNodes; k++)
        //                {
        //                    actualTestDataResults[i][k] = net.OutputLayer[k].Output;
        //                    expectedTestDataResults[i][k] = testDataOutputArray[i][k];
        //                }
        //            }
        //            Console.WriteLine(iterations.ToString());
        //        }
        //        while (keepTesting(actualTestDataResults, numberOfOutputNodes)); //mens outputtene er over/under en hvis værdi
        //        double[] accuracyResults = GetAccuracy(testDataOutputArray, actualTestDataResults, numberOfOutputNodes);
        //        double accuracyPercentage = (accuracyResults[0] / actualTestDataResults.Count())*100;
        //        Console.WriteLine("Right Results: " + accuracyResults[0] + " Wrong Results " + accuracyResults[1] + " Accuracy: " + accuracyPercentage + "%");
        //        Console.WriteLine(iterations.ToString() + " iterations required for training");
        //    }
            
        //    ManualTestOfNetwork(net, numberOfInputNodes, numberOfOutputNodes);
        //    Console.ReadKey(true);
        //}
        
        //bool keepTesting(double[][] actualTestDataResults, int numberOfOutputNodes)
        //{
        //    int correctResult = 0;
        //    int incorrectResult = 0;
        //    int testOutputResultsCount = actualTestDataResults.Count();
        //    for (int i = 0; i < testOutputResultsCount; i++)
        //    {
        //        int onNode = 0;
        //        int offNode = 0;
        //        //Console.WriteLine(testOutputResults[i].ToString());
        //        for (int j = 0; j < numberOfOutputNodes; j++)
        //        {
        //            if (actualTestDataResults[i][j] > 0.5)
        //                onNode++;
        //            else
        //                offNode++;
        //        }
        //        if (onNode == 1 && offNode == 9)
        //            correctResult++;
        //        else
        //            incorrectResult++;
        //    }
        //    Console.WriteLine("Correct: " + correctResult.ToString() + " Wrong: " + incorrectResult.ToString());
        //    if (correctResult > testOutputResultsCount * 0.9)
        //        return false;
        //    else
        //        return true;
        //}
        //double[] GetAccuracy(double[][] expectedResults, double[][] actualResults, int numberOfOutputNodes)
        //{
        //    int countRight = 0;
        //    int countWrong = 0;
        //    int rowCount = expectedResults.Count();
        //    for (int i = 0; i < rowCount ; i++)
        //    {
        //        for (int j = 0; j < numberOfOutputNodes; j++)
        //        {
        //            if (actualResults[i][j] > 0.5)
        //                actualResults[i][j] = 0.9;
        //            else
        //                actualResults[i][j] = 0.1;
        //        }
        //        if (expectedResults[i].SequenceEqual(actualResults[i]))
        //            countRight++;
        //        else
        //            countWrong++;
        //    }
        //    return new double[] { countRight, countWrong};
        //}
        //List<List<Customer>> GetCustomerLists(List<Customer> customerList)
        //{
        //    List<List<Customer>> lists = new List<List<Customer>>();
        //    List<Customer> trainingList = new List<Customer>();
        //    List<Customer> testingList = new List<Customer>();
        //    int count = customerList.Count();
        //    int trainLimit = count * 90 / 100;
            
        //    int i = 0;
        //    foreach(Customer customer in customerList)
        //    {
        //        i++;
        //        if (i <= trainLimit)
        //            trainingList.Add(customer);
        //        else
        //            testingList.Add(customer);
        //    }
        //    lists.Add(trainingList);
        //    lists.Add(testingList);
        //    return lists;
        //}
        //double[][] GetOutputArray(List<Customer> customerList)
        //{
        //    double[][] Output = new double[customerList.Count()][];
        //    int i = 0;
        //    foreach(Customer customer in customerList)
        //    {
        //        NormalizedCustomer nc = new NormalizedCustomer(customer);
        //        Output[i] = new double[] {nc.DestinationPrag, nc.DestinationBudapest, nc.DestinationBerlin, nc.DestinationStockholm, nc.DestinationOslo, nc.DestinationLondon,
        //                nc.DestinationNewYork, nc.DestinationGreenland, nc.DestinationBoraBora, nc.DestinationDubai};
        //        i++;
        //    }
        //    return Output;
        //}
        //double[][] GetInputArray(List<Customer> customerList)
        //{
        //    double[][] Input = new double[customerList.Count()][];
        //    int i = 0;
        //    foreach(Customer customer in customerList)
        //    {
        //        NormalizedCustomer nc = new NormalizedCustomer(customer);
        //        Input[i] = new double[] { nc.Age, nc.AnnualIncome, nc.WorkStatusStudent, nc.WorkStatusEmployed, nc.WorkStatusUnemployed, nc.WorkStatusRetired };
        //        i++;
        //    }
        //    return Input;
        //}

        //private void ManualTestOfNetwork(INeuralNet net, int numberOfInputNodes, int numberOfOutputNodes)
        //{
        //    while (true)
        //    {
        //        // Asking user for input
        //        int age, annualIncome, workStatus, destination;
        //        Console.WriteLine("\nManual test");
        //        Console.Write("Age: ");
        //        age = Convert.ToInt32(Console.ReadLine());
        //        Console.Write("Annual income: ");
        //        annualIncome = Convert.ToInt32(Console.ReadLine());
        //        Console.Write("1: Student, 2: Employed, 3: Unemployed, 4: Retired\n" +
        //            "Work status: ");
        //        workStatus = Convert.ToInt32(Console.ReadLine());
        //        Console.Write("1: Prag, 2: Budapest, 3: Berlin, 4: Stockholm, 5: Oslo, 6: London, 7: New York, 8: Grønland, 9: Bora Bora, 10: Dubai\n" +
        //            "Destination:");
        //        destination = Convert.ToInt32(Console.ReadLine());
        //        Customer customer = new Customer(age, annualIncome, workStatus, destination);
        //        NormalizedCustomer nc = new NormalizedCustomer(customer);

        //        // Converting normalized customer to input and output arrays
        //        double[][] userInput = new double[1][];
        //        userInput[0] = new double[] { nc.Age, nc.AnnualIncome, nc.WorkStatusStudent, nc.WorkStatusEmployed, nc.WorkStatusUnemployed, nc.WorkStatusRetired };

        //        double[][] userOutput = new double[1][];
        //        userOutput[0] = new double[] {nc.DestinationPrag, nc.DestinationBudapest, nc.DestinationBerlin, nc.DestinationStockholm, nc.DestinationOslo, nc.DestinationLondon,
        //                nc.DestinationNewYork, nc.DestinationGreenland, nc.DestinationBoraBora, nc.DestinationDubai};

        //        // Testing the input customer
        //        for (int j = 0; j < numberOfInputNodes; j++)
        //        {
        //            net.PerceptionLayer[j].Output = userInput[0][j];
        //        }

        //        net.Pulse();

        //        double[][] actualTestDataResults = new double[1][];
        //        actualTestDataResults[0] = new double[numberOfOutputNodes];
        //        for (int i = 0; i < numberOfOutputNodes; i++)
        //        {
        //            actualTestDataResults[0][i] = net.OutputLayer[i].Output;
        //        }

        //        // Checking accuracy of the test
        //        int actualResult = 0;
        //        for (int i = 0; i < numberOfOutputNodes; i++)
        //        {
        //            if (actualTestDataResults[0][i] > 0.5)
        //                actualResult = i;
        //        }

        //        // Conclusion
        //        Console.WriteLine("Expected result: {0}, Actual result: {1}", destination, actualResult);
        //        if (destination == actualResult)
        //            Console.WriteLine("The neural network reached the expected result.");
        //        else if(actualResult == 0)
        //            Console.WriteLine("The neural network couldn't find a destination.");
        //        else
        //            Console.WriteLine("The neural network did not reach the expected result.");
        //        Console.WriteLine("Press any key to manually test again...");
        //        Console.ReadKey();
        //    }
        //}
    }
}
