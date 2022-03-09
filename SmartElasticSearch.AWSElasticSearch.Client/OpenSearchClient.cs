using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SmartElasticSearch.Api.Models;

namespace SmartElasticSearch.AWSElasticSearch.Client
{
    public class OpenSearchClient
    {
        private const string SEARCH_REDIRECT = "/mgmt,property/_search";

        public async Task<RestResponse> SearchData(ClientRequest clientRequest, string endpoint, string endpointAccess)
        {
            try
            {
                RestResponse clientResponse = new RestResponse();

                string uploadDomainUrl = endpoint + SEARCH_REDIRECT;
                RestClientOptions options = new RestClientOptions(uploadDomainUrl)
                {
                    ThrowOnAnyError = true,
                    Timeout = 10000
                };

                RestClient client = new RestClient(options);

                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("master-user", endpointAccess);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Basic c21hcnRkb21haW5hZG1pbjpTY2hpc21fMTY=");

                var body = JsonConvert.SerializeObject(clientRequest, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                body = PreprocessBody(body);
                request.AddStringBody(body, DataFormat.Json);

                return await client.PostAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new RestResponse();
            }
        }

        private string PreprocessBody(string body)
        {
            body = body.Replace("qbool", "bool");
            body = body.Replace("qshould", "should");
            body = body.Replace("fbool", "bool");
            body = body.Replace("fshould", "should");
            return body;
        }
    }
}