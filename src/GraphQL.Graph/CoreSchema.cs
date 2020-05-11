using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace GraphQL.Graph
{
    public class CoreSchema : Schema
    {
        public CoreSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CoreQuery>();
            Mutation = provider.GetRequiredService<CoreMutation>();
        }
    }
}
