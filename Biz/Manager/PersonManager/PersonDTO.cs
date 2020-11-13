using Newtonsoft.Json;
using System;

namespace Biz.Manager.PersonManager
{
	public class PersonDTO
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("dateOfBirth")]
		public DateTime? DateOfBirth { get; set; }
	}
}
