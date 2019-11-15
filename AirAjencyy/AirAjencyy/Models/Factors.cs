using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirAjencyy.Models
{
	public class Factors
	{
		public int Id { get; set; }
		public string CustomerName { get; set; }
		public string Mobile { get; set; }
		public string Address { get; set; }
		public DateTime CreateDate { get; set; }
		public int PlaneTicketID { get; set; }
		public PlaneTicket PlaneTicket { get; set; }
	}
}