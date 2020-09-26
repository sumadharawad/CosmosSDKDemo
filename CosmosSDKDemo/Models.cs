using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosSDKDemo
{
    public class address
    {
        public string addressType { get; set; }
        public string addressLine1 { get; set; }

        public location location { get; set; }
        public string postalCode { get; set; }
        public string countryRegionName { get; set; }

    }
    public class location
    {
        public string city { get; set; }
        public string stateProvinceName { get; set; }
    }
    public class customer
    {
        public string id { get; set; }
        public string name { get; set; }

        public address address { get; set; }

    }
}
