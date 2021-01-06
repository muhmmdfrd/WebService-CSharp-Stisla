using Biz.Manager.PermissionManager;

namespace Biz.Manager.MenuManager
{
	public class MenuDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int Sequence { get; set; }
        public long GroupMenuId { get; set; }
        public string GroupMenuName { get; set; }
    }
}
