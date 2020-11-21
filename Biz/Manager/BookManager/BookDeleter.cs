using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.BookManager
{
	internal class BookDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public BookDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Books.FirstOrDefault(x => x.Id == id);

				if (exist == null)
					throw new Exception("data not found");

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
