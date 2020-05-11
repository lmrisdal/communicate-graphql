using Cosmonaut.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class User
    {
        [JsonProperty("id")]
        [CosmosPartitionKey]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public Address Address { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Country { get; set; }
    }

    public class PasswordChangeRequest
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
