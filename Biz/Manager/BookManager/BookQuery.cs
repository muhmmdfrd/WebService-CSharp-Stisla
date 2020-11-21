using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.BookManager
{
	public class BookFilter : TableFilter
	{

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
			return db.Books
				.Select(x => new BookDTO()
				{
					Id = x.Id,
					Title = x.Title,
					Author = x.Author,
					Path = x.Path,
					Qty = x.Qty
				});
		}

		public List<BookDTO> GetAll()
		{
			return GetQuery().ToList();
		}

		public Pagination<BookDTO> GetBook(BookFilter filter)
		{
			var dto = GetQuery();
			int totalRecord = dto.Count();
			int totalFilterred = totalRecord;

			if (!string.IsNullOrWhiteSpace(filter.Keyword))
			{
				dto = dto.Where(x => x.Author.Contains(filter.Keyword.ToLower()) || x.Title.Contains(filter.Keyword.ToLower()));
				totalFilterred = dto.Count();
			}

			dto = dto
				.OrderBy(x => x.Id)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			totalFilterred = dto.Count();

			return new Pagination<BookDTO>()
			{
				RecordsTotal = totalRecord,
				RecordsFiltered = totalFilterred,
				ActivePage = filter.ActivePage,
				PageSize = filter.PageSize,
				Data = dto.ToList()
			};
		}

		public BookDTO GetById(long id)
		{
			return GetQuery().FirstOrDefault(x => x.Id == id);
		}

		public List<BookDTO> GetByKeyword(string keyword)
		{
			return GetQuery().Where(x => x.Author.Contains(keyword) || x.Title.Contains(keyword)).ToList();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
