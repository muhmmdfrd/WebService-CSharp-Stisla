using Biz.Extension.NullCheckerExtension;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.BookManager
{
	internal class BookDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public BookDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Books.Find(id);

				if (exist.IsNull()) throw new Exception("data not found");

				db.Books.Remove(exist);
				db.SaveChanges();

				transac.Complete();
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
