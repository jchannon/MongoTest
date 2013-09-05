using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoTest.Model;
using Nancy;
namespace MongoTest.Modules
{
    

    public class IndexModule : NancyModule
    {
        public IndexModule(MongoDatabase db)
        {
            var collection = db.GetCollection<Person>(typeof(Person).Name.ToLower() + "s");

            Get["/"] = parameters =>
                {
                    var value = new Random().Next(0, 999);
                    //SqlConnection connection = new SqlConnection("Data Source=10.0.122.14;Initial Catalog=srm4000;User ID=developers;Password=chicago");
                    //connection.Open();
                    //SqlCommand command = new SqlCommand("SELECT * FROM [TABLE] WHERE BATCHNO = " + value, connection);
                    //SqlDataReader reader = command.ExecuteReader();
                    //List<Person> people = new List<Person>();

                    //while (reader.Read())
                    //{
                    //    var person = new Person { BatchNo = (int)reader["BATCHNO"], Name = (string)reader["NAME"] };
                    //    people.Add(person);
                    //}
                    //reader.Close();
                    //connection.Close();
                    var foundit = collection.AsQueryable<Person>().Where(x => x.BatchNo == value).ToList();
                    
                    return 200;
                };
        }
    }
}