using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphQL.Functions.Extensions
{
    public static class PlaygroundPageModel
    {
        public static string RenderString(ExecutionContext context)
        {
            var stream = new StreamReader("playground.cshtml", Encoding.UTF8);
            var builder = new StringBuilder(stream.ReadToEnd());

            builder.Replace("@Model.GraphQLEndPoint", "/api/graphql");
            return builder.ToString();
        }

        public static Stream RenderSteam(ExecutionContext context)
        {
            string path = Path.Combine(context.FunctionAppDirectory, "playground.cshtml");
            if (!File.Exists(path))
            {
                string home = Environment.GetEnvironmentVariable("HOME");
                path = Path.Combine(home, "site", "wwwroot");
            }

            var stream = new StreamReader(path, Encoding.UTF8);
            var builder = new StringBuilder(stream.ReadToEnd());

            builder.Replace("@Model.GraphQLEndPoint", "/api/graphql");

            byte[] byteArray = Encoding.ASCII.GetBytes(builder.ToString());
            return new MemoryStream(byteArray);
        }

    }
}
