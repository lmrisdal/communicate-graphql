using Cosmonaut.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace GraphQL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TodoItem
    {
        [JsonProperty("id")]
        [CosmosPartitionKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string AssignedUserId { get; set; }
    }
}
