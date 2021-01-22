using Biz.Extension.JavascriptExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Extension.StringExtension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Extension.HeaderExtension
{
	public static class HeadersExtension
	{
		public static bool ValidateKey(this string key, JObject param)
		{
			using (var db = new SimpleCrudEntities())
			{
				key = key.FromBase64();
				var token = param["token"].ToString();

				var userLoggedIn = db.UserSessions
					.FirstOrDefault(x => 
						x.Token.Equals(token) &&
						x.Token.Equals(key));

				return userLoggedIn.IsNotNull();
			}
		}
	}
}
