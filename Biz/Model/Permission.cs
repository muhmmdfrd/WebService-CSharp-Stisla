namespace Biz.Model
{
	public class PermissionDTOReadOnly
    {
        public long UserId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuSeq { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public int ParentSeq { get; set; }
        public string ModuleId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool ShowMenu { get; set; }
    }
}
