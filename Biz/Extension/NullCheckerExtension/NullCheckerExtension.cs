namespace Biz.Extension.NullCheckerExtension
{
	public static class NullCheckerExtension
	{
		public static bool IsNull(this object i)
		{
			return i == null;
		}

		public static bool IsNotNull(this object i)
		{
			return i != null;
		}
	}
}
