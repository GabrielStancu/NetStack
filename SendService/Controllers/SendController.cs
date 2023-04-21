using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SendService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SendController : ControllerBase
{
    private readonly IHttpClientFactory _factory;

    public SendController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    // Good practice to set the Accept request header
    public ActionResult SetHeader()
    {
        var mt = new MediaTypeWithQualityHeaderValue("application/json");
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Add(mt);

        return Ok();
    }

    // Use streams to read the response content instead of waiting for it to return completely and store it in string
    public async Task<ActionResult> ReadResponseAsStream()
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://example.com/api/resource"),
            Headers =
            {
                { "Authorization", "Bearer [place token here]" },
                { "Accept", "application/json" }
            },
            Content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json")
        };

        // the second parameter tells the client we want to read the headers as they arrive
        // and not wait until all the response arrived
        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        using var stream = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(streamReader);
        var result = new JsonSerializer().Deserialize<Object>(jsonReader);

        // do st with result

        return Ok();
    }

    // Use special defined types
    public ActionResult SendCustomContentType(string json)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://example.com/api/resource"),
            Headers =
            {
                { "Authorization", "Bearer [place token here]" },
                { "Accept", "application/json" }
            },
            // Set the content as the new defined type in the Content namespace of the project
            Content = new Content.JsonContent(json)
        };

        // Send the request

        return Ok();
    }

    // Check the response
    public async Task<ActionResult> CheckResult()
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://example.com/api/resource"),
            Headers =
            {
                { "Authorization", "Bearer [place token here]" },
                { "Accept", "application/json" }
            },
            Content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json")
        };
        var response = await client.SendAsync(request);

        // Check the status code of the response first
        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            // If error we should probably read the details from the content
            // Might use stream approach
            var details = await response.Content.ReadAsStringAsync();
            // do sth with the details (log, alert)
        }

        // Check the response content type
        if (response.Content.Headers.ContentType?.MediaType == "application/json")
        {
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Object>(json);
            // do sth with the object
        }

        return Ok();
    }

    // Send cancellation token to allow call cancellation for long calls
    public async Task<ActionResult> SendCancellationToken(CancellationToken cancellationToken = default)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://example.com/api/resource"),
            Headers =
            {
                { "Authorization", "Bearer [place token here]" },
                { "Accept", "application/json" }
            },
            Content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json")
        };
        // send the token here
        var response = await client.SendAsync(request, cancellationToken);

        return Ok();
    }

    // Compress large responses
    public async Task<ActionResult> CompressLargeResponse()
    {
        // Tell HttpClient to auto decompress responses using the 
        // particular compression method (e.g. GZip).
        var client = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip
        });
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://example.com/api/resource"),
            Headers =
            {
                { "Authorization", "Bearer [place token here]" },
                { "Accept", "application/json" }
            },
            Content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json")
        };
        // ...

        // Tell the API we only want responses in gzip format.
        request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

        return Ok();
    }
}
