using Biz.Extension.StringExtension;
using Biz.Model;
using Repository;
using System;
using System.Linq;

namespace Biz.Manager.UserManager
{
	public class UserFilter : TableFilter
	{
		public long Id { get; set; }
	}

	public class UserQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public UserQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<UserDTO> GetQuery()
		{
			return db.Users.Select(x => new UserDTO() 
			{
				Id = x.Id,
				Username = x.Username,
				Password = x.Password
			});
		}

		public Pagination<UserDTO> Get(UserFilter filter)
		{
			var query = GetQuery();
			var total = query.Count();
			var filterred = total;

			if (!string.IsNullOrEmpty(filter.Keyword))
			{
				query = query.Where(x => x.Username.Contains(filter.Keyword));
				filterred = query.Count();
			}

			if (filter.Id > 0)
			{
				query = query.Where(x => x.Id == filter.Id);
				filterred = query.Count();

				if (filterred == 0) throw new Exception("data not found");
			}

			query = query
				.OrderBy(x => x.Id)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			return new Pagination<UserDTO>()
			{
				RecordsTotal = total,
				RecordsFiltered = filterred,
				Data = query.ToList(),
				ActivePage = filter.ActivePage,
				PageSize = filter.PageSize
			};
		}

		public bool Login(User data)
		{
			return GetQuery()
				.Where(x => x.Username.Equals(data.Username) && 
					x.Password.Equals(data.Password.Encrypt()))
				.FirstOrDefault() != null;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
