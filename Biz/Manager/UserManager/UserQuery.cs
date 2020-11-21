using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.UserManager
{
	public class UserQuery : IDisposable
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public UserQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<UserDTO> GetQuery()
		{
			return db.Users
				.Select(x => new UserDTO() 
				{
					Id = x.Id,
					Username = x.Username,
					Password = x.Password
				});
		}

		public List<UserDTO> GetAll()
		{
			return GetQuery().ToList();
		}

		public UserDTO GetById(long id)
		{
			return GetQuery().FirstOrDefault(x => x.Id == id);
		}

		public List<UserDTO> GetByKeyword(string keyword)
		{
			return GetQuery().Where(x => x.Username.Contains(keyword)).ToList();
		}

		public bool Login(User data)
		{
			return GetQuery().Where(x => x.Username.Equals(data.Username) && x.Password.Equals(data.Password)).FirstOrDefault() != null;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
