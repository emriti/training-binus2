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

namespace TrainingBinus2
{
    public static class GetItems
    {
        [FunctionName("GetItems")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "Select * from Items i"
            )] IEnumerable<ToDoItem> toDoItems,
            ILogger log)
        {
            return new OkObjectResult(toDoItems);
            //if (toDoItems == null)
            //{
            //    return new NotFoundObjectResult("No data found!");
            //} else
            //{
            //    var list = (List<ToDoItem>)toDoItems;
            //    if (list.Count > 0)
            //    {
            //        return new OkObjectResult(toDoItems);
            //    }
            //    return new NotFoundObjectResult("No data found!");
            //}
        }
    }
}
