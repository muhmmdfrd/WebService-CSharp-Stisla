using Biz.Extension.NullCheckerExtension;
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
				var existUser = db.Users.Find(id);
				var existPerson = db.People.Find(existUser.PersonId);

				if (existUser.IsNull() || existPerson.IsNull()) throw new Exception("data not found");

				db.Users.Remove(existUser);
				db.People.Remove(existPerson);
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
