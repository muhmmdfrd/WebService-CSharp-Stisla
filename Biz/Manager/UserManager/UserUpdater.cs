using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public UserUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public User Update(User data)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Users.FirstOrDefault(x => x.Id == data.Id);

				if (exist == null)
					throw new Exception("data not found");

				exist.Username = data.Username;
				exist.Password = data.Password;

				db.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
