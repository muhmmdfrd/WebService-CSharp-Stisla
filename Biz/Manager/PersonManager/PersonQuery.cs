using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.PersonManager
{
	public class PersonQuery : IDisposable
	{
		SimpleCrudEntities db;

		public PersonQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<PersonDTO> GetQuery()
		{
			return (from val in db.People
					select new PersonDTO()
					{
						Id = val.Id,
						Name = val.Name,
						DateOfBirth = val.DateOfBirth
					});
		}

		public List<PersonDTO> GetAll()
		{
			return GetQuery().ToList();
		}

		public PersonDTO GetById(long id)
		{
			var query = GetQuery().FirstOrDefault(x => x.Id == id);

			if (query == null)
				throw new Exception("data not found");

			return query;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
