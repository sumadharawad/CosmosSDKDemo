using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CosmosSDKDemo
{
    public static class Shared
    {
        public static readonly CosmosClient client;

        static Shared()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var endPoint = config["CosmosEndpoint"];
            var masterkey = config["CosomosMasterkey"];

            client = new CosmosClient(endPoint, masterkey);
        }
    }
}
