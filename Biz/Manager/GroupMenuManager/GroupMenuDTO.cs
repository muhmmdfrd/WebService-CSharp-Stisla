using Biz.Manager.MenuManager;
using System.Collections.Generic;

namespace Biz.Manager.GroupMenuManager
{
	public class GroupMenuDTO
	{
		public long Id { get; set; }
		public long RoleId { get; set; }
		public string Name { get; set; }
		public int Sequence { get; set; }
		public bool IsCollapse { get; set; }
		public List<MenuDTO> Menus { get; set; } = new List<MenuDTO>();
	}
}
