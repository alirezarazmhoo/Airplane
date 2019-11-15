using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirAjencyy.Models
{
	public class AirPlane
	{
		public int Id { get; set; }
		[DisplayName("نام هواپیما")]
		[Required(ErrorMessage = "نام هواپیما را وارد کنید")]
		public string airPlaneModel { get; set; }
		[DisplayName("تصویر هواپیما")]
		public string ImageUrl { get; set; }
	}
}