using Repository;
using System.Linq;

namespace Biz.Manager.SessionManager
{
	public static class SessionQuery
	{
		public static User GetById(long id)
		{
			using (var db = new SimpleCrudEntities())
			{
				return db.Users.Find(id);
			}
		}

		public static UserSession GetByToken(string token)
		{
			using (var db = new SimpleCrudEntities())
			{
				return db.UserSessions.AsNoTracking().FirstOrDefault(x => x.Token.Equals(token));
			}
		}

	}
}
