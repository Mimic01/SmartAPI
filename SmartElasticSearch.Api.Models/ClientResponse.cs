using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartElasticSearch.Api.Models
{
    public  class ClientResponse
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public Shards _shards { get; set; }
        public Hits hits { get; set; }

        public class Shards
        {
            public int total { get; set; }
            public int successful { get; set; }
            public int skipped { get; set; }
            public int failed { get; set; }

        }
        public class Total
        {
            public int value { get; set; }
            public string relation { get; set; }

        }
        public class Property
        {
            public int propertyID { get; set; }
            public string name { get; set; }
            public string formerName { get; set; }
            public string streetAddress { get; set; }
            public string city { get; set; }
            public string market { get; set; }
            public string state { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }

        }
        public class _source
        {
            public Property property { get; set; }

        }
        public class ResultHits
        {
            public string _index { get; set; }
            public string _type { get; set; }
            public string _id { get; set; }
            public double _score { get; set; }
            public _source _source { get; set; }

        }
        public class Hits
        {
            public Total total { get; set; }
            public double max_score { get; set; }
            public IList<ResultHits> hits { get; set; }

        }
    }
}
