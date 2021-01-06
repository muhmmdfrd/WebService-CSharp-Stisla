using Biz.Model;
using Repository;
using System;
using System.Linq;

namespace Biz.Manager.BorrowingManager
{
	public class BorrowingFilter : TableFilter
	{
		public long Id { get; set; }
	}

	public class BorrowingQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public BorrowingQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<BorrowingDTO> GetQuery()
		{
			return db.Borrowings
				.Join(db.Books, borrowing => borrowing.BookId, book => book.Id, (borrowing, book) => new { Book = book, Borrowing = borrowing })
				.Join(db.Users, borrowing => borrowing.Borrowing.UserId, user => user.Id, (borrowing, user) => new { User = user, Borrowing = borrowing })
				.Select(x => new BorrowingDTO()
				{
					Id = x.Borrowing.Borrowing.Id,
					DateOfBorrowing = x.Borrowing.Borrowing.DateOfBorrowing,
					Deadline = x.Borrowing.Borrowing.Deadline,
					IsPenalty = x.Borrowing.Borrowing.IsPenalty,
					TotalPenalty = x.Borrowing.Borrowing.TotalPenalty,
					BookId = x.Borrowing.Book.Id,
					Author = x.Borrowing.Book.Author,
					Path = x.Borrowing.Book.Path,
					Title = x.Borrowing.Book.Title,
					UserId = x.Borrowing.Borrowing.UserId,
					Username = x.User.Username,
					Name = x.User.Person.Name,
					Qty = x.Borrowing.Borrowing.Qty
				});
		}

		public Pagination<BorrowingDTO> Get(BorrowingFilter filter)
		{
			var query = GetQuery();
			var total = query.Count();
			var filterred = total;

			if (!string.IsNullOrEmpty(filter.Keyword))
			{
				string keyword = filter.Keyword;
				query = query.Where(x => x.Name.Contains(keyword) || x.Title.Contains(keyword) || x.Username.Contains(keyword) || x.Author.Contains(keyword));
				filterred = query.Count();
			}

			if (filter.Id > 0)
			{
				query = query.Where(x => x.Id == filter.Id);
				filterred = query.Count();

				if (filterred == 0)
					throw new Exception(MessageResponse.NotFound("Book"));
			}

			query = query
				.OrderByDescending(x => x.DateOfBorrowing)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			return new Pagination<BorrowingDTO>()
			{
				ActivePage = filter.ActivePage,
				RecordsTotal = total,
				RecordsFiltered = filterred,
				PageSize = filter.PageSize,
				Data = query.ToList()
			};
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
