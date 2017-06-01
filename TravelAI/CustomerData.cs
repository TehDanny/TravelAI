using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace TravelAI
{
    class CustomerData
    {
        
        public List<Customer> GetCustomers(int amountOfCustomers)
        {
            List<Customer> customerList = new List<Customer>();
            Cluster cluster = Cluster.Builder().AddContactPoint("cassandra.yikes.dk").Build();
            ISession session = cluster.Connect("Exam");
            RowSet rows = session.Execute("select \"Age\", \"AnnualIncome\", \"WorkStatus\", \"Destination\" from \"People\" limit " +  amountOfCustomers.ToString());
            foreach (Row row in rows)
            {
                int age = (int)row["Age"];
                int annualIncome = (int)row["AnnualIncome"];
                int workStatus = (int)row["WorkStatus"];
                int destination = (int)row["Destination"];
                Customer customer = new Customer(age, annualIncome, workStatus, destination);
                customerList.Add(customer);
            }
                
           
            return customerList;
        }

    }
}
