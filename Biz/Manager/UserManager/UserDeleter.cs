using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public UserDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Users.FirstOrDefault(x => x.Id == id);

				if (exist == null)
					throw new Exception("data not found");

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
