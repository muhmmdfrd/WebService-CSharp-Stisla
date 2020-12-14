using Biz.Extension.NullCheckerExtension;
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
			return db.Users
				.Join(db.People, user => user.PersonId, person => person.Id, (user, person) => new { User = user, Person = person })
				.Select(x => new UserDTO()
				{
					Id = x.User.Id,
					Username = x.User.Username,
					PersonId = x.Person.Id,
					Name = x.Person.Name,
					DateOfBirth = x.Person.DateOfBirth
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

		public LoginInfo Login(User data)
		{
			var encrypted = data.Password.Encrypt();
			var resultData = GetQuery().FirstOrDefault(x => x.Username.Equals(data.Username) && x.Password.Equals(encrypted));

			if (resultData.IsNull()) throw new Exception("Username or Password are incorrect !");

			return new LoginInfo()
			{
				Username = resultData.Username,
				Token = Guid.NewGuid().ToString()
			};
		}

		public class LoginInfo
		{
			public string Username { get; set; }
			public string Token { get; set; }
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
