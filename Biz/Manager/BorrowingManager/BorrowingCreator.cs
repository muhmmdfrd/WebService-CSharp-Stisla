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
				var book = db.Books.Find(borrowing.BookId);

				ValidateQty(book, borrowing);

				book.Qty -= borrowing.Qty;
				var result = db.Borrowings.Add(borrowing);
		
				db.SaveChanges();

				transac.Complete();

				return result;
			}
		}

		private void ValidateQty(Book book, Borrowing borrowing)
		{
			var totalBook = book.Qty;
			var totalBorrowing = borrowing.Qty;

			if (book.IsNull())
				throw new Exception(MessageResponse.NotFound("Book"));

			if (totalBook < totalBorrowing)
				throw new Exception("Borrowed qty is greater then existing qty");

			if (totalBorrowing <= 0)
				throw new Exception("Please enter a valid qty");
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
