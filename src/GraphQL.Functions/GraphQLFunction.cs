using System;
using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server.Internal;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using GraphQL.Functions.Infrastructure;
using System.Net.Http;
using GraphQL.Functions.Extensions;
using System.Net;
using System.Net.Http.Headers;

namespace GraphQL.Functions
{
    public class GraphQLFunction
    {
        private readonly IGraphQLExecuter<ISchema> _graphQLExecuter;

        public GraphQLFunction(IGraphQLExecuter<ISchema> graphQLExecuter)
        {
            _graphQLExecuter = graphQLExecuter ?? throw new ArgumentNullException(nameof(graphQLExecuter));
        }

        [FunctionName("graphql")]
        public async Task<IActionResult> Run(
         [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger logger)
        {
            try
            {
                ExecutionResult executionResult = await _graphQLExecuter.ExecuteAsync(req, logger);

                if (executionResult.Errors != null)
                    logger.LogError("GraphQL execution error(s): {Errors}", executionResult.Errors);

                return new GraphQLExecutionResult(executionResult);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }

        [FunctionName("playground")]
        public HttpResponseMessage RenderPlayground(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ui/playground")] HttpRequestMessage req, ExecutionContext context)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(PlaygroundPageModel.RenderSteam(context));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
