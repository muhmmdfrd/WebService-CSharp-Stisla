using Biz.Extension.NullCheckerExtension;
using Biz.Extension.StringExtension;
using Biz.Model;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.UserManager
{
	public class UserFilter : TableFilter
	{
		public long Id { get; set; }
		public long RoleId { get; set; }
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
			return (from user in db.Users.AsNoTracking()
					join person in db.People
						on user.PersonId equals person.Id
					join role in db.Roles
						on user.RoleId equals role.Id
					orderby role.Name, person.Name
					select new UserDTO()
					{
						Id = user.Id,
						Username = user.Username,
						Password = null,
						PersonId = person.Id,
						Name = person.Name,
						DateOfBirth = person.DateOfBirth,
						RoleId = role.Id,
						RoleName = role.Name
					});
		}

		private IQueryable<UserDTO> GetQueryLogin()
		{
			return db.Users.Select(x => new UserDTO()
			{
				Id = x.Id,
				Username = x.Username,
				Password = x.Password,
				RoleId = x.RoleId
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
				query = query.Where(x => x.Id.Equals(filter.Id));
				filterred = query.Count();

				if (filterred.Equals(0)) 
					throw new Exception("data not found");
			}

			if (filter.RoleId > 0)
			{
				query = query.Where(x => x.RoleId == filter.RoleId);
				filterred = query.Count();

				if (filterred.Equals(0))
					throw new Exception("data not found");
			}

			query = query
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
			var resultData = GetQueryLogin().FirstOrDefault(x => x.Username.Equals(data.Username) && x.Password.Equals(encrypted));

			if (resultData.IsNull() || resultData.RoleId.Equals(6)) 
				throw new Exception("Username or Password are incorrect !");

			return new LoginInfo()
			{
				Username = resultData.Username,
				Token = Guid.NewGuid().ToString(),
				RoleId = resultData.RoleId
			};
		}

		public void Logout(string token)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.UserSessions.FirstOrDefault(x => x.Token.Equals(token));

				if (exist.IsNull())
					throw new Exception("data not found");

				db.UserSessions.Remove(exist);
				db.SaveChanges();

				transac.Complete();
			}
		}

		public class LoginInfo
		{
			public string Username { get; set; }
			public string Token { get; set; }
			public long RoleId { get; set; }
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
