namespace SmartElasticSearch.Api.Models
{
    public class RecordRequest
    {
        public string searchPhrase { get; set; }
        public string market { get; set; }
        public int limit { get; set; }
    }
}