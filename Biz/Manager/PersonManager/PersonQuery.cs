using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.PersonManager
{
	public class PersonFilter : TableFilter
	{
		public long Id { get; set; }
	}

	public class PersonQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public PersonQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<PersonDTO> GetQuery()
		{
			return db.People.Select(val => new PersonDTO()
			{
				Id = val.Id,
				Name = val.Name,
				DateOfBirth = val.DateOfBirth
			});
		}

		public Pagination<PersonDTO> Get(PersonFilter filter)
		{
			var query = GetQuery();
			var total = query.Count();
			var filterred = total;

			if (filter.Id > 0)
			{
				query = query.Where(x => x.Id == filter.Id);
				filterred = query.Count();

				if (filterred == 0) throw new Exception("data not found");
			}

			if (!string.IsNullOrWhiteSpace(filter.Keyword))
			{
				query = query.Where(x => x.Name.Contains(filter.Keyword));
				filterred = query.Count();
			}

			query = 
				query
					.OrderBy(x => x.Id)
					.Skip(filter.Skip)
					.Take(filter.PageSize);

			return new Pagination<PersonDTO>()
			{
				ActivePage = filter.ActivePage,
				PageSize = filter.PageSize,
				RecordsTotal = total,
				RecordsFiltered = filterred,
				Data = query.ToList()
			};
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
