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

		public User Update(Person person, User user)
		{
			using (var transac = new TransactionScope())
			{
				var existUser = db.Users.Find(user.Id);
				var existPerson = db.People.Find(existUser.PersonId);

				if (existUser.IsNull() || existPerson.IsNull())
					throw new Exception("data not found");

				existUser.Username = user.Username;
				existUser.Password = user.Password.Encrypt();
				existPerson.DateOfBirth = person.DateOfBirth;
				existPerson.Name = person.Name;

				db.SaveChanges();

				transac.Complete();

				return existUser;
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
