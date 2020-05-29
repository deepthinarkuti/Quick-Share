using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace WeShareProject
{
    public static class CosmosDatabase
    {
        public static CosmosClient Client { get; private set; }


        static CosmosDatabase()
        {
            try
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

                var endpoint = config["CosmosEndpoint"];
                var masterKey = config["CosmosMasterKey"];
                Client = new CosmosClient(endpoint, masterKey);
            }
            catch(CosmosException e)
            {
                Console.WriteLine($"Connection error with exception {e}");
            }
        }
    }
}
