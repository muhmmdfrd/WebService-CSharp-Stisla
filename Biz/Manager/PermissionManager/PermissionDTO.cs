namespace Biz.Manager.PermissionManager
{
	public class PermissionDTO
	{
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ModuleId { get; set; }
        public long MenuId { get; set; }
        public bool IsCanRead { get; set; }
        public bool IsCanCreate { get; set; }
        public bool IsCanUpdate { get; set; }
        public bool IsCanDelete { get; set; }
        public bool IsShowMenu { get; set; }
        public long RoleId { get; set; }
    }
}
