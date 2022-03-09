using Newtonsoft.Json;
using SmartElasticSearch.Api.Models;
using SmartElasticSearch.AWSElasticSearch.Client;
using static SmartElasticSearch.Api.Models.ClientRequest;

namespace SmartElasticSearch.Api.BusinessLogic
{
    public class SmartSearchBusinessLogic
    {
        public async Task<ClientResponse> SmartSearch(RecordRequest searchRequest, string endpoint, string endpointAccess)
        {
            OpenSearchClient client = new OpenSearchClient();
            ClientRequest clientRequest = MapToClientRequest(searchRequest);
            var response = await client.SearchData(clientRequest, endpoint, endpointAccess);
            ClientResponse businessResponse = MapToResponse(response.Content);
            return businessResponse;
        }
        private ClientResponse MapToResponse(string clientInputResponse)
        {
            return JsonConvert.DeserializeObject<ClientResponse>(clientInputResponse);
        }
        private ClientRequest MapToClientRequest(RecordRequest searchRequest)
        {
            List<string> fieldList = new List<string> { "mgmt.name", "mgmt.market", "mgmt.state", "property.name", "property.market", "property.state", "property.city", "property.streetAddress", "property.formerName" };
            Term term1 = new Term() { MgmtMarket = searchRequest.market ?? String.Empty };
            Term term2 = new Term() { PropertyMarket = searchRequest.market ?? String.Empty };
            var fshouldList = new List<ClientRequest.FShould> { new FShould { term = term1 }, new FShould { term = term2} };


            return new ClientRequest()
            {
                from = 0,
                size = searchRequest.limit,
                query = new ClientRequest.Query()
                {
                    qbool = new ClientRequest.Qbool()
                    {
                        qshould = new ClientRequest.Qshould()
                        {
                            multi_match = new ClientRequest.MultiMatch()
                            {
                                query = searchRequest.searchPhrase ?? String.Empty,
                                fields = fieldList,
                                type = "best_fields"
                            }
                        },
                        filter = new ClientRequest.Filter()
                        {
                            fbool = new ClientRequest.Fbool()
                            {
                                fshould = fshouldList
                            }
                        }
                    }
                }
            };
        }
    }
}