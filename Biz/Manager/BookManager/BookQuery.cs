using Biz.Model;
using Repository;
using System;
using System.Linq;

namespace Biz.Manager.BookManager
{
	public class BookFilter : TableFilter
	{
		public long Id { get; set; }
	}

	public class BookQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public BookQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<BookDTO> GetQuery()
		{
			return db.Books.Select(x => new BookDTO()
			{
				Id = x.Id,
				Title = x.Title,
				Author = x.Author,
				Path = x.Path,
				Qty = x.Qty
			});
		}

		public Pagination<BookDTO> Get(BookFilter filter)
		{
			var dto = GetQuery();
			int totalRecord = dto.Count();
			int totalFilterred = totalRecord;

			if (!string.IsNullOrWhiteSpace(filter.Keyword))
			{
				dto = dto.Where(x => x.Author.Contains(filter.Keyword.ToLower()) || x.Title.Contains(filter.Keyword.ToLower()));
				totalFilterred = dto.Count();
			}

			if (filter.Id > 0)
			{
				dto = dto.Where(x => x.Id == filter.Id);
				totalFilterred = dto.Count();

				if (totalFilterred == 0) throw new Exception(MessageResponse.NotFound("Book"));
			}

			dto = dto
				.OrderBy(x => x.Id)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			return new Pagination<BookDTO>()
			{
				RecordsTotal = totalRecord,
				RecordsFiltered = totalFilterred,
				ActivePage = filter.ActivePage,
				PageSize = filter.PageSize,
				Data = dto.ToList()
			};
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
