using Newtonsoft.Json;
using System;

namespace Biz.Model
{
	[Serializable]
	public class Status<T>
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("values")]
		public T Values { get; set; }
	}
}
