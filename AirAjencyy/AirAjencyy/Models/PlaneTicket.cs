using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirAjencyy.Models
{
	public class PlaneTicket
	{
		public int Id { get; set; }
		[DisplayName("نوع پرواز")]
		public byte Type  { get;set; }
		[DisplayName("تاریخ پرواز")]
		public DateTime TakeOffDate { get; set; }
		[DisplayName("تصویربلیط")]
		public string ImageUrl { get; set; }
		
		[DisplayName("کشور مبدا")]
		public int CountryOriginID { get; set; }
		public Country CountryOrigin { get; set; }
		
		[DisplayName("استان مقصد")]
		public int CityDestinationID { get; set; }
		
		public City CityOrigin { get; set; }

		[DisplayName("توضیحات کلی")]
		public string Descriptions { get; set; }
		[DisplayName("قیمت")]
		[Required(ErrorMessage = "قیمت الزامی است")]

		public long Price { get; set; }
		[DisplayName("کشور مقصد")]
		public int CountryDestinationCode { get; set; }
		[DisplayName("استان مبدا")]
		public int CityOriginCode { get; set; }

		[DisplayName("ساعت پرواز")]
		[Required(ErrorMessage ="ساعت پرواز را اعلام کنید")]
		public string TakeOffTime { get; set; }

		[DisplayName("نوع هواپیما")]
		public int AirPlaneCode { get; set; }
	}





}