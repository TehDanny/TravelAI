using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPopulator
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
            List<Person> customerList = GeneratePeopleData(10000);
            printList(customerList);
        }
        List<Person> GeneratePeopleData(int numberOfRows)
        {
            List<Person> peopleList = new List<Person>();
            
            for(int i = 0; i< numberOfRows; i++)
            {
                Person person = new Person();
                peopleList.Add(person);
                
            }
            
            return peopleList;
        }
        void printList(List<Person> peopleList)
        {
            foreach(Person person in peopleList)
            {
                Console.WriteLine("Age: " + person.Age + /*" Sex: " + person.Sex + */" WorkStatus: " + person.WorkStatus +
                " Annual Income: " + person.AnnualIncome + " Destination: " + person.Destination);
            }
            Console.ReadKey(true);
        }
    }
}
