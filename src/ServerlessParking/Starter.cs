
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace ServerlessParking
{
    public static class OrchestrationStarter
    {
        [FunctionName("OrchestrationStarter")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orchestration/{functionName}")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient orchestrationClient,
            string functionName,
            TraceWriter log)
        {
            dynamic functionData = await req.Content.ReadAsAsync<object>();
            string instanceId = await orchestrationClient.StartNewAsync(functionName, functionData);

            log.Info($"Started orchestration with ID = '{instanceId}'.");

            return orchestrationClient.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
