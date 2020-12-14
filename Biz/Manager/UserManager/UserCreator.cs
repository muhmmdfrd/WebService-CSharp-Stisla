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

		public User Save(Person person, User user)
		{
			using (var transac = new TransactionScope())
			{
				db.People.Add(person);
				db.SaveChanges();

				if (string.IsNullOrEmpty(user.Password)) user.Password = Guid.NewGuid().ToString();

				user.PersonId = person.Id;
				user.Password = user.Password.Encrypt();

				var result = db.Users.Add(user);
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
