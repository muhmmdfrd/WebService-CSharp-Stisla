using Biz.Extension.StringExtension;
using Repository;
using System;
using System.Linq;
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

		public void Update(Person person, User user)
		{
			using (var transac = new TransactionScope())
			{
				var query = db.Users.Include("Person").AsQueryable();
				var existUser = db.Users.Find(user.Id);
				existUser.Username = user.Username;
				existUser.RoleId = user.RoleId;
				existUser.Password = string.IsNullOrEmpty(user.Password) ? existUser.Password : user.Password.Encrypt();

				var existPerson = db.People.Find(existUser.PersonId);
				existPerson.DateOfBirth = person.DateOfBirth;
				existPerson.Name = person.Name;

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
