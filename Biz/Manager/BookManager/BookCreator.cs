using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.BookManager
{
	public class BookCreator : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public BookCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public Book Save(Book data)
		{
			using (var transac = new TransactionScope())
			{
				var result = db.Books.Add(data);
				db.SaveChanges();

				transac.Complete();

				return result;
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
