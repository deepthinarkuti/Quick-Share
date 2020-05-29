using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeShareProject.Models;

namespace WeShareProject
{
    class DocumentOperations
    {

        public static async Task<Boolean> LoginOperations(string username, string password)
        {
            
            return await QueryDocument(username,password);
        }

        public static async Task RegisterOperations(string username, string password)
        {
            await CreateDocument(username, password);
        }

        private static async Task CreateDocument(string uname, string pass)
        {
            try
            {
                var container = CosmosDatabase.Client.GetContainer("mydatabase", "Users");

                Console.WriteLine("Creating the Document:");

                var itemProperties = new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = uname,
                    Password = pass

                };

                await container.CreateItemAsync(itemProperties, new PartitionKey(uname));
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception occurred while creating document {e}");
            }
            
            Console.WriteLine("Created user");

        }

        private static async Task<Boolean> QueryDocument(string username, string password)
        {
            var container = CosmosDatabase.Client.GetContainer("mydatabase", "Users");
            var sql = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<Users>(sql);
            var users = await iterator.ReadNextAsync();

            Console.WriteLine("Querying the documents:");

            foreach (Users user in users)
            {
                Console.WriteLine($"Current document with username {user.UserName}");
                if (username == user.UserName && password == user.Password)
                {
                    return true;
                }

            }

            return false; 
        }

    }
 
}
