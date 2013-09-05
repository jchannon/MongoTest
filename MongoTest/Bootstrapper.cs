using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MongoDB.Driver;
using Nancy.TinyIoc;
using MongoTest.Model;

namespace MongoTest
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            const string connectionString = "mongodb://localhost";

            //// Get a thread-safe client object by using a connection string
            var mongoClient = new MongoClient(connectionString);

            //// Get a reference to a server object from the Mongo client object
            container.Register((c, p) => mongoClient.GetServer());

            var mongoServer = mongoClient.GetServer();

            var db = mongoServer.GetDatabase("test");

            MongoCollection collection = db.GetCollection<Person>(typeof(Person).Name.ToLower() + "s");
            collection.Drop();

            int count = 0;
            int batchNo = 0;
            var people = new List<Person>();
            for (int i = 0; i < 10000; i++)
            {
                if (count == 10)
                {
                    batchNo++;
                    count = 0;
                }

                people.Add(new Person { BatchNo = batchNo, InternalId = i, Name = "Bob" + i });
                count++;
            }
            collection.InsertBatch(people);
            collection.EnsureIndex("BatchNo");



            //SqlConnection connection =
            //    new SqlConnection("Data Source=1.1.1.1;Initial Catalog=mydb;User ID=developers;Password=developers");
            //connection.Open();
            //SqlCommand command =
            //          new SqlCommand("DELETE FROM [TABLE]", connection);
            //command.ExecuteNonQuery();

            //int count = 0;
            //int batchNo = 0;
            //var sql = "BEGIN" + Environment.NewLine;
            //for (int i = 0; i < 10000; i++)
            //{

            //    if (count == 10)
            //    {
            //        batchNo++;
            //        count = 0;
            //        sql += "END";
            //        command =
            //            new SqlCommand(sql, connection);
            //        command.ExecuteNonQuery();
            //        sql = "BEGIN" + Environment.NewLine;
            //    }

            //    sql += "INSERT INTO [TABLE] (NAME,BATCHNO) VALUES('" + ("Bob" + i) + "'," + batchNo + ")" +
            //           Environment.NewLine;

            //    count++;
            //}
            //connection.Close();

        }




        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var server = container.Resolve<MongoServer>();

            container.Register((c, p) => server.GetDatabase("test"));
        }

    }
}