using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WeShareProject
{
    class ContainerOperations
    {

        public async static Task Operations()
        {
            await ViewContainers();
            await CreateContainer("Families",400,"/pinCode");
            await ViewContainers();
            await DeleteContainer();
        }

        private async static Task ViewContainers()
        {
            var iterator = CosmosDatabase.Client.GetDatabase("mydatabase").GetContainerQueryIterator<ContainerProperties>();

            var containers = await iterator.ReadNextAsync();

            Console.WriteLine("The containers in the database are:");
            foreach(var container in containers)
            {
                Console.WriteLine($"container:{container.Id}");
                await ViewContainer(container);

            }
        }

        private async static Task CreateContainer(string containerId, int throughput = 400, string partitionKey = "/partitionKey")
        {
            var containerProp = new ContainerProperties
            {
                Id = containerId,
                PartitionKeyPath = partitionKey
            };
            
            var database = CosmosDatabase.Client.GetDatabase("mydatabase");
            
            await database.CreateContainerAsync(containerProp, throughput);

            Console.WriteLine($"Created container is {containerId}");


        }

        private async static Task DeleteContainer()
        {
  
            await CosmosDatabase.Client.GetContainer("mydatabase","Families").DeleteContainerAsync();
            Console.WriteLine("Container Deleted");
        }

        private async static Task ViewContainer(ContainerProperties containerProp)
        {
            Console.WriteLine($"Container Id is: {containerProp.Id} ");
            Console.WriteLine($"Container last modified: {containerProp.LastModified} ");

            Console.WriteLine($"Container partition key is: {containerProp.PartitionKeyPath} ");

            var container = CosmosDatabase.Client.GetContainer("mydatabase", containerProp.Id);
            var throughput = await container.ReadThroughputAsync();

            Console.WriteLine($"Container throughput is: {throughput} ");
        }

    }
}
