using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

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
            insertIntoDb(customerList);
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
        void insertIntoDb(List<Person> peopleList)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("cassandra.yikes.dk").Build();
            ISession session = cluster.Connect("Exam");
            foreach(Person person in peopleList)
            {
                var ps = session.Prepare("insert into  \"People\" (id, \"Age\", \"AnnualIncome\", \"Destination\", \"WorkStatus\") values (uuid(), ?, ?, ?, ?)");
                var statement = ps.Bind(person.Age, person.AnnualIncome, person.Destination, person.WorkStatus);
                
                //session.Execute(statement);
            }
            Console.WriteLine("Done inserting!");
            Console.ReadKey(true);
        }
    }
}
