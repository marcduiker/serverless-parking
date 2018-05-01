using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ServerlessParking
{
    public static class OrchestrationStarter
    {
        [FunctionName(nameof(OrchestrationStarter))]
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
