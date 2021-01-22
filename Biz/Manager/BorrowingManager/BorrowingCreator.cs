using Biz.Enum;
using Biz.Extension.EnumExtension;
using Biz.Extension.IntExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.BorrowingManager
{
	public class BorrowingCreator : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public BorrowingCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public Borrowing Borrow(Borrowing borrowing)
		{
			using (var transac = new TransactionScope())
			{
				var user = db.Users.Find(borrowing.UserId);
				if (user.IsNull())
					throw new Exception(MessageResponse.NotFound("User"));

				var book = db.Books.Find(borrowing.BookId);
				if (book.IsNull())
					throw new Exception(MessageResponse.NotFound("Book"));

				// add deadline
				borrowing.Deadline = borrowing.DateOfBorrowing.AddDays(3);

				ValidateQty(book, borrowing);

				book.Qty -= borrowing.Qty;
				var result = db.Borrowings.Add(borrowing);
		
				db.SaveChanges();

				transac.Complete();

				return result;
			}
		}

		public void Return(Borrowing borrowing)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Borrowings.Find(borrowing.Id);
				var now = DateTime.Now;

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("Borrowing"));

				var book = db.Books.Find(exist.BookId);

				if (book.IsNull())
					throw new Exception(MessageResponse.NotFound("Book"));

				if (borrowing.Deadline < now)
				{
					exist.IsPenalty = true;
					exist.Status = LoanStatusEnum.Late.GetValueOfEnum();
					exist.TotalPenalty = now.Subtract(exist.Deadline).Days * 1000;
				}
				else
				{
					exist.IsPenalty = false;
					exist.Status = LoanStatusEnum.OnTime.GetValueOfEnum();
					exist.TotalPenalty = 0;
				}

				book.Qty += exist.Qty;

				db.SaveChanges();

				transac.Complete();
			}
		}

		private void ValidateQty(Book book, Borrowing borrowing)
		{
			var totalBook = book.Qty;
			var totalBorrowing = borrowing.Qty;

			if (book.IsNull())
				throw new Exception(MessageResponse.NotFound("Book"));

			if (totalBook.IsLowerThan(totalBorrowing))
				throw new Exception("Borrowed qty is greater then existing qty");

			if (totalBorrowing.IsLowerThanEquals(0))
				throw new Exception("Please enter a valid qty");
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
