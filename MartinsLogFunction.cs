using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Martins.Function
{
    public static class MartinsLogFunction
    {
        [FunctionName("MartinsLogFunction")]
        public static async Task<IActionResult> Run(
            [CosmosDB(databaseName: "JqueryMigrateLogs", collectionName: "Log",
            ConnectionStringSetting = "CosmosDbConnectionString")] IAsyncCollector<dynamic> documentsOut,
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Martin's Log HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<List<LogEntry>>(requestBody);

            foreach(var item in data)
            {
                await documentsOut.AddAsync(new
                {
                    id = System.Guid.NewGuid().ToString(),
                    file = item.file,
                    message = item.message,
                    dateTime = DateTime.Now
                });
            }

            string responseMessage = "This HTTP triggered function executed successfully.";
            return new OkObjectResult(responseMessage);
        }
    }

    public class LogEntry
    {
        public string file { get; set; }
        public string message { get; set; }
        public DateTime dateTime { get; set; }
    }
}
