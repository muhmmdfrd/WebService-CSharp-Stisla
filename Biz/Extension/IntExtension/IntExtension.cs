using System;

namespace Biz.Extension.IntExtension
{
	public static class IntExtension
	{
		public static bool IsGreaterThan(this int i, int value)
		{
			return i > value;
		}

		public static long ToLong(this object value)
		{
			return Convert.ToInt64(value);
		}
	}
}
