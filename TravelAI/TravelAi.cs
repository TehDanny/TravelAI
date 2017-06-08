using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLibrary;

namespace TravelAI
{
    class TravelAi
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
        public TravelAi()
        {
            net = new NeuralNet();

            
            errorMargin = 0.05;
            customerList = cd.GetCustomers(10000);
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
                int index = 1; // skal ændres til  0 senere
                int testDataInputCount = testDataInputArray.Count();
                int testDataOutputCount = testDataOutputArray.Count();
                double[][] actualTestedDataResults;
                double[][] desiredTestDataResults = new double[testDataInputCount][];
                double[] desiredResult;
                double lastAccuracy = 0;
                do
                {
                    actualTestedDataResults = new double[testDataInputCount][];
                    desiredResult = new double[numberOfOutputNodes];
                    iterations++;
                    desiredResult = trainDataOutputArray[index];
                    
                    net.Train(trainDataInputArray[index], desiredResult);
                    //net.Train(trainDataInputArray, trainDataOutputArray);

                    // først træner netværket og så tester den de prædefinerede værdier igennem for at se om resultatet passer og hvis ikke gør den det igen
                    net.ApplyLearning();

                    for (int i = 0; i < testDataInputCount; i++)
                    {
                        for (int j = 0; j < numberOfInputNodes; j++)
                        {
                            net.PerceptionLayer[j].Output = testDataInputArray[i][j];
                        }
                        net.Pulse();
                        actualTestedDataResults[i] = new double[numberOfOutputNodes];
                        //desiredTestDataResults[i] = new double[numberOfOutputNodes];
                        for (int k = 0; k < numberOfOutputNodes; k++)
                        {
                            actualTestedDataResults[i][k] = net.OutputLayer[k].Output;
                            //desiredResult[k] = testDataOutputArray[i][k];
                            //expectedTestDataResults[i][k] = testDataOutputArray[i][k];
                        }
                    }
                    //Console.WriteLine(iterations.ToString());
                    double accuracy = GetAccuracy(desiredResult, actualTestedDataResults);
                    //double accuracy = GetAccuracyBatch(testDataOutputArray, actualTestedDataResults[index]);
                    if (accuracy > (1 - errorMargin) && accuracy> lastAccuracy)
                    {
                        Console.WriteLine("index: " + index.ToString() + "accuracy: " + accuracy + "prev Accuracy: " + lastAccuracy.ToString() );
                        Console.WriteLine(iterations.ToString() + " iterations required for training index: " + index);
                        index++;
                        lastAccuracy = accuracy;
                    }
                    
                }
                while (/*keepTesting(actualTestedDataResults, desiredResult, numberOfOutputNodes)*//* &&*/ (index <testDataOutputCount)); //mens outputtene er over/under en hvis værdi
                //double[] accuracyResults = GetAccuracy(testDataOutputArray, actualTestedDataResult, numberOfOutputNodes);
                //double accuracyPercentage = (accuracyResults[0] / actualTestedDataResult.Count()) * 100;
                //Console.WriteLine("Right Results: " + accuracyResults[0] + " Wrong Results " + accuracyResults[1] + " Accuracy: " + accuracyPercentage + "%");
                Console.WriteLine(iterations.ToString() + " iterations required for training");
            }

            ManualTestOfNetwork(net, numberOfInputNodes, numberOfOutputNodes);
            Console.ReadKey(true);
        }
        bool keepTesting(double[][] actualTestDataResults,double[] expectedTestDataResult, int numberOfOutputNodes)
        {
            int correctResult = 0;
            int incorrectResult = 0;
            int testOutputResultsCount = actualTestDataResults.Count();
            //int expectedTestDataResultsCount = expectedTestDataResults.Count();
            for(int i = 0; i < testOutputResultsCount; i++)
            {
               
                if (actualTestDataResults[i].SequenceEqual(expectedTestDataResult)){
                    correctResult++;
                }
                else
                    incorrectResult++;
            }
            
            if (correctResult > testOutputResultsCount * 0.9)
                return false;
            else
                return true;
        }
        //double GetAccuracyBatch(double[][] desiredResults, double[] actualResults)
        //{
        //    int countRight = 0;
        //    int countWrong = 0;
        //    int rowCount = desiredResults.Count();
        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        for (int j = 0; j < numberOfOutputNodes; j++)
        //        {
        //            if (actualResults[j] > 0.9)
        //                actualResults[j] = 1;
        //            if(actualResults[j] < 0.1)
        //                actualResults[j] = 0;
        //        }
        //        Console.WriteLine("actual: " + ConvertArrayToString(actualResults) + " desired: " + ConvertArrayToString(desiredResults[i]));
        //        if (desiredResults[i].SequenceEqual(actualResults))
        //            countRight++;
        //        else
        //            countWrong++;
        //    }
        //    return countRight/(countRight+countWrong);
        //}
        double GetAccuracy(double[] desiredResult, double[][] actualResults)
        {
            double right = 0;
            double wrong = 0;
            int rowCount = actualResults.Count();
            int columnCount = desiredResult.Count();
            double low = 0 + errorMargin;
            double high = 1 - errorMargin;
            for(int i=0;i< rowCount; i++)
            {
                for(int j = 0; j< columnCount; j++)
                {
                    if (actualResults[i][j] > high)
                        actualResults[i][j] = 1;
                    if (actualResults[i][j] < low)
                        actualResults[i][j] = 0;
                }
                //Console.WriteLine("actual: " + ConvertArrayToString(actualResults[i]) + " desired: " + ConvertArrayToString(desiredResult));
                if (actualResults[i].SequenceEqual(desiredResult))
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
                        actualResult = i;
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
