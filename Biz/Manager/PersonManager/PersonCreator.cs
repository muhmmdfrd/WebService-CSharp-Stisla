using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.PersonManager
{
	public class PersonCreator : IDisposable
	{
		private SimpleCrudEntities db = new SimpleCrudEntities();

		public PersonCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public Person Save(Person person)
		{
			using (var transac = new TransactionScope())
			{
				var result = db.People.Add(person);
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
