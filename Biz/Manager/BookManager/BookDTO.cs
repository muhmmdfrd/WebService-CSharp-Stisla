namespace Biz.Manager.BookManager
{
	public class BookDTO
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Path { get; set; }
		public int Qty { get; set; }
		public string Description { get; set; }
	}
}
