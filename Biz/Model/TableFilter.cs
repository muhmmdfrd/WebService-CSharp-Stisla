namespace Biz.Model
{
	public class TableFilter
	{
        private string keyword;
        private int pageIndex;
        private int pageSize;
        private string sortName;
        private string sortDir;

        public string Keyword
        {
            get
            {
                return keyword ?? "";
            }
            set
            {
                keyword = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return pageIndex == 0 ? 1 : pageIndex;
            }
            set
            {
                pageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return pageSize == 0 ? 10 : pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        public int Skip
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }

        public int ActivePage { get; set; }

        public string SortName
        {
            get
            {
                return sortName ?? "";
            }
            set
            {
                sortName = value;
            }
        }

        public string SortDir
        {
            get
            {
                return sortDir ?? "";
            }
            set
            {
                sortDir = value;
            }
        }
    }
}
