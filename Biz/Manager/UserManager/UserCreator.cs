using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserCreator : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public UserCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public User Save(User data)
		{
			using (var transac = new TransactionScope())
			{
				var result = db.Users.Add(data);
				db.SaveChanges();

				transac.Complete();

				return result;
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
