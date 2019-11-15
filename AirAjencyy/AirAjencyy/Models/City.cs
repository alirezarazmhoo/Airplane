using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirAjencyy.Models
{
	public class City
	{
		public int Id { get; set; }
		[DisplayName("نام استان")]
		[Required(ErrorMessage = "نام استان را وارد کنید")]
		public string Name { get; set; }
		[DisplayName("کشور")]
		public int CountryID { get; set; }
		public Country Country { get; set; }


		

	}
}