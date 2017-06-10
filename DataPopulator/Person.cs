using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPopulator
{
    public class Person
    {
        static Random rand = new Random();
        public int Age { get; set; }
        public int AnnualIncome { get; set; } // in thousands
        public int WorkStatus { get; set; } // 1= Student, 2 = Employed, 3=Unemployed, 4= Retired
        //public int Sex { get; set; } // 1= male, 2= female
        public int Destination { get; set; } // 1 = cheapest and youngest, 10 oldest and most expensive
        // Prag, Budapest, Berlin,Stockholm ,Oslo , London, New York, Grønland, Bora Bora , Dubai,
        public Person()
        {
            
            
            this.Age = rand.Next(16, 81);
            //this.Sex = rand.Next(1, 3);
            if (this.Age >= 67)
            {

                this.WorkStatus = 4;
                this.AnnualIncome = rand.Next(150, 250) * 1000;
            }
            else if (this.Age >= 40)
            {
                this.WorkStatus = rand.Next(2, 4);
                if (this.WorkStatus == 2)
                    this.AnnualIncome = rand.Next(300, 500) * 1000;
                else
                    this.AnnualIncome = rand.Next(150, 250) * 1000;
            }
            else if (this.Age >= 25)
            {
                this.WorkStatus = rand.Next(1, 4);
                if (this.WorkStatus == 1)
                    this.AnnualIncome = rand.Next(120, 150) * 1000;
                else if (this.WorkStatus == 2)
                    this.AnnualIncome = rand.Next(250, 400) * 1000;
                else
                    this.AnnualIncome = rand.Next(150, 250) * 1000;
            }
            else if (this.Age >= 18) // 18-25
            {
                this.WorkStatus = rand.Next(1, 4);
                if (this.WorkStatus == 1)
                    this.AnnualIncome = rand.Next(60, 150) * 1000;
                else if (this.WorkStatus == 2)
                    this.AnnualIncome = rand.Next(200, 300) * 1000;
                else
                    this.AnnualIncome = rand.Next(150, 200) * 1000;
            }
            else // 16-18
            {
                this.WorkStatus = rand.Next(1, 4);
                if (this.WorkStatus == 1)
                    this.AnnualIncome = rand.Next(0, 20) * 1000;
                else if (this.WorkStatus == 2)
                    this.AnnualIncome = rand.Next(20, 75) * 1000;
                else
                    this.AnnualIncome = rand.Next(0, 20) * 1000;
            }
            this.Destination = CalculateDestination();
        }
        int CalculateDestination()
        {
            int destination = 1;
            Random rand = new Random();
            if(this.AnnualIncome >= 400000)
                destination += 4;
            else if(this.AnnualIncome >= 300000)
                destination += 3;
            else if(this.AnnualIncome > 200000)
                destination += 2;
            else if(this.AnnualIncome > 100000)
                destination += 1;
            
            if(this.Age >= 60)
                destination += 2;
            else if(this.Age >= 40)
                destination += 3;
            else if(this.Age >= 30)
                destination += 2;
            else if(this.Age >= 25)
                destination += 1;

            if(this.WorkStatus == 2)  //Employed
                destination += 2;
            else if(this.WorkStatus == 4) // retired
                destination += 1;
            return destination;
        }
        string translateDestination(int number)
        {
            // Prag, Budapest, Berlin,Stockholm ,Oslo , London, New York, Grønland, Bora Bora , Dubai,
            switch (number)
            {
                case 1:
                    return "Prag";
                case 2:
                    return "Budapest";
                case 3:
                    return "Berlin";
                case 4:
                    return "Stockholm";
                case 5:
                    return "Oslo";
                case 6:
                    return "London";
                case 7:
                    return "New York";
                case 8:
                    return "Greenland";
                case 9:
                    return "Bora Bora";
                case 10:
                    return "Dubai";
                default:
                    return "Invalid Destination";
            }
        }
       
    }
}
