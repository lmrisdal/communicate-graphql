using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQL.Functions.Infrastructure
{
    internal class GraphQLBadRequestException : Exception
    {
        public GraphQLBadRequestException(string message)
            : base(message)
        { }
    }
}
