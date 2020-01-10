using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace TrainingBinus2
{
    public static class TestError
    {
        [FunctionName("TestError")]
        public static async Task<IActionResult> TestError(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogError("Test");
            return (ActionResult)new OkObjectResult($"Hello");
        }


        [FunctionName("InsertItem")]
        public static HttpResponseMessage InsertItem(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
    [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection"
            )] out ToDoItem newData,
    ILogger log)
        {
            var content = req.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            newData = JsonConvert.DeserializeObject<ToDoItem>(jsonContent);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
