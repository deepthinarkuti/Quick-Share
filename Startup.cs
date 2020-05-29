using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace WeShareProject
{
    public class Startup
    {

        public CosmosClient Client { get; private set; }

        public Startup()
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

                var endpoint = config["CosmosEndpoint"];
                var masterKey = config["CosmosMasterKey"];
                Client = new CosmosClient(endpoint, masterKey);

            }
            catch (Exception e)
            {
                Console.WriteLine($"CosmosDatabase connection error with Exception {e}");
            }

        }
    }
}
