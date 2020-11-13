using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.PersonManager
{
	public class PersonDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public PersonDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.People.FirstOrDefault(x => x.Id == id);

				if (exist == null)
					throw new Exception("data not found");

				db.People.Remove(exist);
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
