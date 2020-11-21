using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.BookManager
{
	internal class BookUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public BookUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public Book Update(Book data)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Books.FirstOrDefault(x => x.Id == data.Id);

				if (exist == null)
					throw new Exception("data not found");

				exist.Author = data.Author;
				exist.Path = data.Path;
				exist.Qty = data.Qty;
				exist.Title = data.Title;

				db.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
