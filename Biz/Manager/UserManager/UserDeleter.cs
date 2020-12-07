using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public UserDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Users.Find(id);

				if (exist == null) throw new Exception("data not found");

				db.Users.Remove(exist);
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
