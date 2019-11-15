using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirAjencyy.Models
{
	public class Country
	{
		public int Id { get; set; }
		[DisplayName("نام کشور")]
		[Required(ErrorMessage = "نام کشور را وارد کنید")]
		public string Name { get; set; }

		
	public ICollection<City> Cities { get; set; }

	}
}