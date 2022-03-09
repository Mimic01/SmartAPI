using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartElasticSearch.Api.Models
{
    public class ClientRequest
    {
        public int from { get; set; }
        public int size { get; set; }
        public Query query { get; set; }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class MultiMatch
        {
            public string query { get; set; }
            public List<string> fields { get; set; }
            public string type { get; set; }
        }

        public class Term
        {
            [JsonProperty("mgmt.market")]
            public string MgmtMarket { get; set; }

            [JsonProperty("property.market")]
            public string PropertyMarket { get; set; }
        }

        public class Filter
        {
            public Fbool fbool { get; set; }
        }

        public class Query
        {
            public Qbool qbool { get; set; }
        }
        public class Qbool
        {
            public Qshould qshould { get; set; }
            public Filter filter { get; set; }
        }
        public class Fbool
        {
            public List<FShould> fshould { get; set; }
        }
        public class Qshould
        {
            public MultiMatch multi_match { get; set; }
        }
        public class FShould
        {
            public Term term { get; set; }
        }

    }
}
