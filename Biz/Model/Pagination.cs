using Newtonsoft.Json;
using System.Collections.Generic;

namespace Biz.Model
{
	public class Pagination<T>
	{
        [JsonProperty(PropertyName = "activePage")]
        public int ActivePage { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty(PropertyName = "recordsFilterred")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "data")]
        public IEnumerable<T> Data { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "draw")]
        public string Draw { get; set; }

        public Pagination()
        {
            ActivePage = 1;
            RecordsTotal = 1;
            PageSize = 5;
            Data = null;
        }

    }
}
