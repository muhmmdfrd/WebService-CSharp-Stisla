using Biz.Extension.NullCheckerExtension;
using Biz.Extension.StringExtension;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public UserUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public User Update(User data)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Users.Find(data.Id);

				if (exist.IsNull())
					throw new Exception("data not found");

				exist.Username = data.Username;
				exist.Password = data.Password.Encrypt();

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
