using System;

namespace Biz.Manager.BorrowingManager
{
	public class BorrowingDTO
	{
        public long Id { get; set; }
        public DateTime DateOfBorrowing { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public int Qty { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsPenalty { get; set; }
        public int TotalPenalty { get; set; }
        public int Status { get; set; }
    }
}
