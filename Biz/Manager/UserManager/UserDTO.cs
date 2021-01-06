using Newtonsoft.Json;
using System;

namespace Biz.Manager.UserManager
{
	[Serializable]
	public class UserDTO
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("personId")]
		public long PersonId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("dateOfBirth")]
		public DateTime? DateOfBirth { get; set; }

		[JsonProperty("roleId")]
		public long RoleId { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }
	}
}
