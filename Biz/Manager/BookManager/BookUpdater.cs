using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.BookManager
{
	internal class BookUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public BookUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public Book Update(Book data)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Books.Find(data.Id);

				if (exist.IsNull()) 
					throw new Exception(MessageResponse.NotFound("Book"));

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
