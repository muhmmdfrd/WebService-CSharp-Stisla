using Biz.Extension.NullCheckerExtension;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.PersonManager
{
	public class PersonUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();
		
		public PersonUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public Person Update(Person data)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.People.FirstOrDefault(x => x.Id == data.Id);

				if (exist.IsNull())
					throw new Exception("data not found");

				exist.DateOfBirth = data.DateOfBirth;
				exist.Name = data.Name;

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
