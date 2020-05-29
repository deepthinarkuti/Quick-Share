using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace WeShareProject
{
    class DatabaseOperations
    {

        public async static Task Operations()
        {
            await ViewDatabases();
            await CreateDatabase();
            await ViewDatabases();
            await DeleteDatabase();
        }
        private async static Task ViewDatabases()
        {
            var iterator = CosmosDatabase.Client.GetDatabaseQueryIterator<DatabaseProperties>();
            var databases = await iterator.ReadNextAsync();

            Console.WriteLine($"Databases defined are:");

            foreach (var database in databases)
            {

                Console.WriteLine($"Database id : {database.Id}");
            }
        }

        private async static Task CreateDatabase()
        {

            Console.WriteLine($"Creating the Database");
            var result = await CosmosDatabase.Client.CreateDatabaseAsync("mydatabase1");
            var database = result.Resource;
            Console.WriteLine($"Database id created is {database.Id} ");
        }


        // fetching the results from the database container
        private async static Task CosmosDatabaseSetup()
        {
            var client = CosmosDatabase.Client;
            var container = client.GetContainer("mydatabase", "Users");
            var sql = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
            var page = await iterator.ReadNextAsync();

            foreach (var doc in page)
            {
                Console.WriteLine($"Record of the user with User Name:{doc.username} and User Id:{doc.id}");
            }

            Console.ReadLine();
        }

        private async static Task DeleteDatabase()
        {
            await CosmosDatabase.Client.GetDatabase("mydatabase1").DeleteAsync();

            Console.WriteLine("Deleted the database");
        }
    }
}
