using System.Collections.Generic;

namespace Biz.Model
{
	public class Pagination<T>
	{
        public int ActivePage { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int PageSize { get; set; }
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
