using Biz.Extension.StringExtension;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserCreator : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public UserCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public User Save(User data)
		{
			using (var transac = new TransactionScope())
			{
				if (string.IsNullOrEmpty(data.Password))
					data.Password = Guid.NewGuid().ToString();

				data.Password = data.Password.Encrypt();

				var result = db.Users.Add(data);
				db.SaveChanges();

				transac.Complete();

				return result;
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
