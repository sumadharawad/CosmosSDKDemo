using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;


namespace CosmosSDKDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //QueryForDocument().Wait();

            //ViewDatabases().Wait();

            //CreateDatabase().Wait();
            //DeleteDatabase().Wait();

            // GetContainer().Wait();
            // CreateDocument().Wait();
            QueryDocument().Wait();
           
        }

        private async static Task QueryDocument()
        {
            var container = Shared.client.GetContainer("mydb", "myStore");
            var sql = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
           
            var documents = await iterator.ReadNextAsync();
            foreach (var doc in documents)
            {
                var customer = JsonConvert.DeserializeObject<customer>(doc.ToString());
                Console.WriteLine($"City {customer.address.location.city} of customer {customer.name}");
            }
            Console.ReadLine();
        }

        private async static Task CreateDocument()
        {
            var container = Shared.client.GetContainer("mydb", "myStore");
            var document = new customer { 
             id=Guid.NewGuid().ToString(),
              name="Dharwad family",
              address=new address
              {
                   addressType="Main Office",
                   addressLine1="Basapura road",
                    location=new location
                    {
                        city="Bangalore",
                         stateProvinceName="Karnataka"
                    },
                     postalCode="11229",
                      countryRegionName="India"
              }
            
            };

            await container.CreateItemAsync(document);
            Console.WriteLine($"New document created {document.id} in the container {container.Id}");
            Console.ReadLine();

        }

        private async static Task GetContainer()
        {
            var container = Shared.client.GetContainer("Families", "Families");
            var iterator = container.GetItemQueryIterator<DatabaseProperties>();
            var items =await  iterator.ReadNextAsync();
            var throughput = container.ReadThroughputAsync();
            Console.WriteLine($"Throughput id of container {container.Id} is {throughput.Id}");
            Console.WriteLine($"Container name is {container.Id}");
            Console.WriteLine("items in this container");
            foreach (var item in items)
            {
                Console.WriteLine(item.Id);
            }
            Console.ReadLine();
        }

        private async static Task DeleteDatabase()
        {
            Console.WriteLine("Deleting the newly created database");
           await Shared.client.GetDatabase("NewDb").DeleteAsync();
            Console.WriteLine("Deleted, press enter to see new list of databases");
            Console.ReadLine();
            ViewDatabases().Wait();
        }
        private async static Task CreateDatabase()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Creating the database..");
            await Shared.client.CreateDatabaseIfNotExistsAsync("NewDb");
            Console.WriteLine("Press enter to see all the databses ");
            Console.ReadLine();
            ViewDatabases().Wait();
        }
        private async static Task ViewDatabases()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("View databases");

            var iterator = Shared.client.GetDatabaseQueryIterator<DatabaseProperties>();
            var databses = await iterator.ReadNextAsync();
            foreach (var d in databses)
            {
                Console.WriteLine($"Database ID: {d.Id} Modified at: {d.LastModified}");
            }
            //View containers
           // Shared.client.GetContainer
            Console.ReadLine();
        }

      
    }
}
