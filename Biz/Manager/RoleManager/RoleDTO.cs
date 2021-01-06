using Biz.Manager.PermissionManager;
using Repository;
using System.Collections.Generic;

namespace Biz.Manager.RoleManager
{
	public class RoleDTO
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public List<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();
	}
}
